using Eventkalender.Database.Context;
using Eventkalender.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.DAL
{
    public class EventkalenderDAL
    {
        private EventkalenderContext context;

        public EventkalenderDAL()
        {
            context = new EventkalenderContext(DatabaseClient.GetConnectionString("eventkalender-db.xml"));
        }

        public void AddNation(Nation n)
        {
            using (context)
            {
                context.Nations.Add(n);
                context.SaveChanges();
            }
        }

        public Nation GetNation(int id)
        {
            using (context)
            {
                return context.Nations.Find(id);
            }
        }

        public void AddEvent(Event e)
        {
            using (context)
            {
                context.Events.Add(e);
                context.SaveChanges();
            }
        }

        public Event GetEvent(int id)
        {
            using (context)
            {
                return context.Events.Find(id);
            }
        }

        //public void AddNations(List<Nation> nations)
        //{
        //    context.Nations.Add()
        //}
    }
}
