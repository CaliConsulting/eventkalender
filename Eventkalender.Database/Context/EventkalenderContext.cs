using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Eventkalender.Database
{
    public class EventkalenderContext : DbContext
    {
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