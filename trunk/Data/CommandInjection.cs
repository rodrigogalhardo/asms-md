using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public class CommandInjection : KnownTargetValueInjection<SqlCommand>
    {
        protected override void Inject(object source, ref SqlCommand cmd, PropertyDescriptorCollection sourceProps)
        {
            for (var i = 0; i < sourceProps.Count; i++)
            {
                var prop = sourceProps[i];
                if (prop.Name == "Id") continue;

                var dbType = SqlDbType.NVarChar;
                if (prop.PropertyType == typeof(int)) dbType = SqlDbType.Int;
                if (prop.PropertyType == typeof(long)) dbType = SqlDbType.BigInt;

                cmd.Parameters.Add(prop.Name, dbType).Value = prop.GetValue(source);
            }

        }
    }
}