using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database
{
    public class SqlServerConfiguration : DbConfiguration
    {
        public SqlServerConfiguration()
        {
            SetDefaultConnectionFactory(new SqlConnectionFactory());
        }
    }
}
