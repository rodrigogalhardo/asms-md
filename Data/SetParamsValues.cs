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

    public class FieldsByComma : KnownTargetValueInjection<string>
    {
        private IEnumerable<string> ignoredFields = new string[] { };
        private string format = "{0}";

        public FieldsByComma IgnoreFields(params string[] fields)
        {
            ignoredFields = fields;
            return this;
        }

        public FieldsByComma SetFormat(string f)
        {
            format = f;
            return this;
        }

        protected override void Inject(object source, ref string target, PropertyDescriptorCollection sourceProps)
        {
            var s = string.Empty;
            for (var i = 0; i < sourceProps.Count; i++)
            {
                var prop = sourceProps[i];
                if (ignoredFields.Contains(prop.Name)) continue;
                s += string.Format(format, prop.Name) + ",";
            }
            s = s.TrimEnd(new[] { ',' });
            target += s;
        }
    }
    
    public class SetParamsValues : KnownTargetValueInjection<SqlCommand>
    {
        private IEnumerable<string> ignoredFields = new string[] { };
        private string prefix = string.Empty;

        public SetParamsValues Prefix(string p)
        {
            prefix = p;
            return this;
        }

        public SetParamsValues IgnoreFields(params string[] fields)
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
                cmd.Parameters.AddWithValue("@" + prefix + prop.Name, value);
            }
        }
    }
}