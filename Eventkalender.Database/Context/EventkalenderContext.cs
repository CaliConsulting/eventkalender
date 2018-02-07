using Eventkalender.Database.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.Context
{
    public class EventkalenderContext : DbContext
    {
        public EventkalenderContext(string connectionString) : base(connectionString)
        {
            //this.Database.Connection.ConnectionString = DatabaseClient.GetConnectionString("eventkalender-db.xml");
        }

        public DbSet<Nation> Nations { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Person> Persons { get; set; }

        //public DbSet<Attendant> Attendants { get; set; }

    }
}
