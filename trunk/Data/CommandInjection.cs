using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public class WhereInjection : KnownTargetValueInjection<string>
    {
        protected override void Inject(object source, ref string target, PropertyDescriptorCollection sourceProps)
        {
            for (var i = 0; i < sourceProps.Count; i++)
            {
                if (i != 0) target += " and ";

                var p = sourceProps[i];
                target += p.Name + "=@" + p.Name;
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
    }

    public class CommandInjection : KnownTargetValueInjection<SqlCommand>
    {
        private IEnumerable<string> ignoredFields = new string[]{};

        public CommandInjection IgnoreFields(params string[] fields)
        {
            ignoredFields = fields.AsEnumerable();
            return this;
        }

        protected override void Inject(object source, ref SqlCommand cmd, PropertyDescriptorCollection sourceProps)
        {
            for (var i = 0; i < sourceProps.Count; i++)
            {
                var prop = sourceProps[i];
                if (ignoredFields.Contains(prop.Name)) continue;

                var value = prop.GetValue(source) ?? DBNull.Value;
                cmd.Parameters.AddWithValue("@" + prop.Name, value);
            }
        }
    }
}