using System.Data.Entity;
using System.Data.Entity.Infrastructure;

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