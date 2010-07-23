using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public static class Extensions
    {
        public static string ToSqlValue(this object v, Type t)
        {
            string s;
            if (t == typeof(string))
            {
                s = v != null ? "'" + v.ToString().Replace("'", "''") + "'" : "null";
            }
            else if (t == typeof(DateTime))
            {
                s = "'" + ((DateTime)v).ToShortDateString() + "'";
            }
            else if (t == typeof(DateTime?))
            {
                var value = (DateTime?)v;
                s = value.HasValue ? "'" + value.Value.ToShortDateString() + "'" : "null";
            }
            else if (t == typeof(bool)) s = (((bool)v) ? 1 : 0).ToString();
            else
                s = v.ToString();

            return s;

        }
    }
    public class WhereInjection : KnownTargetValueInjection<string>
    {
        protected override void Inject(object source, ref string target, PropertyDescriptorCollection sourceProps)
        {
            for (var i = 0; i < sourceProps.Count; i++)
            {
                if (i != 0) target += " and ";

                var p = sourceProps[i];
                target += p.Name + "=" + p.GetValue(source).ToSqlValue(p.PropertyType);
            }
        }
    }

    public class InsertInjection : KnownTargetValueInjection<string>
    {
        private readonly IEnumerable<string> ignoredFields = new[] { "Id" };

        protected override void Inject(object source, ref string target, PropertyDescriptorCollection sourceProps)
        {
            target = "insert " + source.GetType().Name +
                "s(" + GetNames(sourceProps, false) + ") values("
                + GetNames(sourceProps, true) + ")";
        }

        private string GetNames(PropertyDescriptorCollection props, bool v)
        {
            var s = string.Empty;
            for (var i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                if (ignoredFields.Contains(prop.Name)) continue;
                s += (v ? "@" : string.Empty) + prop.Name + ",";
            }
            s = s.TrimEnd(new[] { ',' });
            return s;
        }

        private string GetValues(object source, PropertyDescriptorCollection props)
        {
            var s = string.Empty;
            for (var i = 0; i < props.Count; i++)
            {
                var v = props[i].GetValue(source);
                var t = props[i].PropertyType;

                if (ignoredFields.Contains(props[i].Name)) continue;

                s += v.ToSqlValue(t);
                s += ",";
            }

            s = s.TrimEnd(new[] { ',' });
            return s;
        }
    }

    public class CommandInjection : KnownTargetValueInjection<SqlCommand>
    {
        protected override void Inject(object source, ref SqlCommand cmd, PropertyDescriptorCollection sourceProps)
        {
            for (var i = 0; i < sourceProps.Count; i++)
            {
                var prop = sourceProps[i];
                if (prop.Name == "Id") continue;

                //var dbType = SqlDbType.NVarChar;
                //if (prop.PropertyType == typeof(int)) dbType = SqlDbType.Int;
                //if (prop.PropertyType == typeof(long)) dbType = SqlDbType.BigInt;
                //if (prop.PropertyType == typeof(DateTime?)) dbType = SqlDbType.Date;
                //if (prop.PropertyType == typeof(DateTime)) dbType = SqlDbType.Date;
                //if (prop.PropertyType == typeof(bool)) dbType = SqlDbType.Bit;

                var value = prop.GetValue(source) ?? DBNull.Value;
                cmd.Parameters.AddWithValue("@" + prop.Name, value);
            }

        }
    }
}