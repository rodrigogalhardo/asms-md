﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public class FieldsBy : KnownTargetValueInjection<string>
    {
        private IEnumerable<string> ignoredFields = new string[] { };
        private string format = "{0}";
        private string nullFormat;
        private string glue = ",";

        public FieldsBy SetGlue(string g)
        {
            glue = " " + g + " ";
            return this;
        }

        public FieldsBy IgnoreFields(params string[] fields)
        {
            ignoredFields = fields;
            return this;
        }

        public FieldsBy SetFormat(string f)
        {
            format = f;
            return this;
        }

        public FieldsBy SetNullFormat(string f)
        {
            nullFormat = f;
            return this;
        }

        protected override void Inject(object source, ref string target)
        {
            var sourceProps = source.GetProps();
            var s = string.Empty;
            for (var i = 0; i < sourceProps.Count; i++)
            {
                var prop = sourceProps[i];
                if (ignoredFields.Contains(prop.Name)) continue;
                if (prop.GetValue(source) == DBNull.Value && nullFormat != null)
                    s += string.Format(nullFormat, prop.Name);
                else
                    s += string.Format(format, prop.Name) + glue;
            }
            s = s.RemoveSuffix(glue);
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

        protected override void Inject(object source, ref SqlCommand cmd)
        {
            if (source == null) return;
            var sourceProps = source.GetProps();

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