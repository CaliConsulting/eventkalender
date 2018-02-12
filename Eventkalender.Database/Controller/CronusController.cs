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
        private CronusDataDAL dataDAL;

        public CronusController()
        {
            metadataDAL = new CronusMetadataDAL();
            dataDAL = new CronusDataDAL();
        }

        public List<String> GetKeys()
        {
            return metadataDAL.GetKeys();
        }

        public List<String> GetIndexes()
        {
            return metadataDAL.GetIndexes();
        }

        public List<String> GetTableConstraints()
        {
            return metadataDAL.GetTableConstraints();
        }

        public List<String> GetTables()
        {
            return metadataDAL.GetTables();
        }

        public List<String> GetColumnsForEmployeeTable()
        {
            return metadataDAL.GetColumnsForEmployeeTable();
        }

        public List<String> GetEmployeeMetaData()
        {
            return metadataDAL.GetEmployeeMetaData();
        }

        public List<String> GetEmployeeAbsenceMetaData()
        {
            return metadataDAL.GetEmployeeAbsenceMetaData();
        }

        public List<String> GetEmployeeRelativeMetaData()
        {
            return metadataDAL.GetEmployeeRelativeMetaData();
        }

        public List<String> GetEmployeeQualificationMetaData()
        {
            return metadataDAL.GetEmployeeQualificationMetaData();
        }

        public List<String> GetEmployeePortalSetupMetaData()
        {
            return metadataDAL.GetEmployeePortalSetupMetaData();
        }

        public List<String> GetEmployeeStatisticsGroupMetaData()
        {
            return metadataDAL.GetEmployeeStatisticsGroupMetaData();
        }

        //HÄR BÖRJAR DATADAL

        public Model.Tuple GetIllestPerson()
        {
            return dataDAL.GetIllestPerson();
        }

        public List<Model.Tuple> GetIllPersonsByYear(int startYear, int endYear)
        {
            return dataDAL.GetIllPersonsByYear(startYear, endYear);
        }

        public List<Model.Tuple> GetEmployeeAndRelatives()
        {
            return dataDAL.GetEmployeeAndRelatives();
        }

        public List<Model.Tuple> GetEmployeeData()
        {
            return dataDAL.GetEmployeeData();
        }

        public List<Model.Tuple> GetEmployeeAbsenceData()
        {
            return dataDAL.GetEmployeeAbsenceData();
        }

        public List<Model.Tuple> GetEmployeeRelativeData()
        {
            return dataDAL.GetEmployeeRelativeData();
        }

        public List<Model.Tuple> GetEmployeeQualificationData()
        {
            return dataDAL.GetEmployeeQualificationData();
        }

        public List<Model.Tuple> GetEmployeePortalSetupData()
        {
            return dataDAL.GetEmployeePortalSetupData();
        }

        public List<Model.Tuple> GetEmployeeStatisticsGroupData()
        {
            return dataDAL.GetEmployeeStatisticsGroupData();
        }

    }
}
