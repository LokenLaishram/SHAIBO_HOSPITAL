using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.DAL
{
   public class SqlParameterHelper
    {
        private List<SqlParameter> paraColl = null;
        public SqlParameterHelper()
        {
            paraColl = new List<SqlParameter>();
        }
        public void Add(string parameterName, SqlDbType type, object value)
        {
            this.Add(parameterName, type, value, ParameterDirection.Input);
        }
        public void Add(string parameterName, SqlDbType type, object value, ParameterDirection direction)
        {
            SqlParameter para = new SqlParameter(parameterName, type);
            para.Direction = direction;
            para.Value = value;
            paraColl.Add(para);
        }
        public void Remove(string parameterName)
        {
            if (paraColl != null && paraColl.Count > 0)
                paraColl.Remove(paraColl.Where(p => p.ParameterName == parameterName).FirstOrDefault());
        }
        public SqlParameter GetParameter(string parameterName)
        {
            if (paraColl != null && paraColl.Count > 0)
                return paraColl.Where(p => p.ParameterName == parameterName).FirstOrDefault();
            else
                return null;
        }
        public SqlParameter[] ToArray()
        {
            if (paraColl != null && paraColl.Count > 0)
                return paraColl.ToArray();
            else
                return new SqlParameter[0];
        }
    }
}
