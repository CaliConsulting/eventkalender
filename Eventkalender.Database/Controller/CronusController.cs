using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database
{
    public class CronusController
    {
        private CronusDataDAL dataDAL;
        private CronusMetadataDAL metadataDAL;

        public CronusController(string xmlPath)
        {
            dataDAL = new CronusDataDAL(xmlPath);
            metadataDAL = new CronusMetadataDAL(xmlPath);
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

        public DataTuple GetIllestPerson()
        {
            return dataDAL.GetIllestPerson();
        }

        public List<DataTuple> GetSickPersonByYear(int startYear, int endYear)
        {
            return dataDAL.GetSickPersonByYear(startYear, endYear);
        }
    }
}
