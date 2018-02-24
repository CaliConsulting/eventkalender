using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database
{
    public class CronusContext : DbContext
    {
        public CronusContext()
        {
            Configuration.ProxyCreationEnabled = false;
            //Database.ExecuteSqlCommand("DROP TABLE [PreCompiledViews]");
        }

        public CronusContext(string xmlPath) : base(DatabaseClient.GetSqlServerConnectionString(xmlPath))
        {
            Configuration.ProxyCreationEnabled = false;
            //Database.ExecuteSqlCommand("DROP TABLE [PreCompiledViews]");
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Employee>().ToTable("CRONUS Sverige AB$Employee");
        }

        // HACK: Enable pre-compiled views
        internal DbSet<PreCompiledView> PreCompiledViews { get; set; }

        public DbSet<Employee> Employee { get; set; }
    }
}
