using Eventkalender.Database.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.Controller
{
    public class CronusController
    {
        private CronusMetadataDAL metadataDAL;

        public CronusController()
        {
            metadataDAL = new CronusMetadataDAL();
        }

        public List<string> GetKeys()
        {
            return metadataDAL.GetKeys();
        }

        public List<string> GetIndexes()
        {
            return metadataDAL.GetIndexes();
        }

        public List<string> GetTableConstraints()
        {
            return metadataDAL.GetTableConstraints();
        }

        public List<string> GetTables()
        {
            return metadataDAL.GetTables();
        }

        public List<string> GetColumnsForEmployeeTable()
        {
            return metadataDAL.GetColumnsForEmployeeTable();
        }
    }
}
