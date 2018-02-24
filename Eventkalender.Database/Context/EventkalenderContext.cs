using Eventkalender.Database.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database
{
    public class EventkalenderContext : DbContext
    {
        public EventkalenderContext()
        {
            Configuration.ProxyCreationEnabled = false;
            //Database.ExecuteSqlCommand("DROP TABLE [PreCompiledViews]");
        }

        public EventkalenderContext(string xmlPath) : base(DatabaseClient.GetSqlServerConnectionString(xmlPath))
        {
            Configuration.ProxyCreationEnabled = false;
            //Database.ExecuteSqlCommand("DROP TABLE [PreCompiledViews]");
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());
        }

        // HACK: Enable pre-compiled views
        internal DbSet<PreCompiledView> PreCompiledViews { get; set; }

        public DbSet<Nation> Nation { get; set; }

        public DbSet<Event> Event { get; set; }

        public DbSet<Person> Person { get; set; }
    }
}
