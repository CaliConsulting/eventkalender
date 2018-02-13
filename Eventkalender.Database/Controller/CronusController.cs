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

        public DataTuple GetIllestPerson()
        {
            return dataDAL.GetIllestPerson();
        }

        public List<DataTuple> GetIllPersonsByYear(int startYear, int endYear)
        {
            return dataDAL.GetIllPersonsByYear(startYear, endYear);
        }

        public List<DataTuple> GetEmployeeAndRelatives()
        {
            return dataDAL.GetEmployeeAndRelatives();
        }

        public List<DataTuple> GetEmployeeData()
        {
            return dataDAL.GetEmployeeData();
        }

        public List<DataTuple> GetEmployeeAbsenceData()
        {
            return dataDAL.GetEmployeeAbsenceData();
        }

        public List<DataTuple> GetEmployeeRelativeData()
        {
            return dataDAL.GetEmployeeRelativeData();
        }

        public List<DataTuple> GetEmployeeQualificationData()
        {
            return dataDAL.GetEmployeeQualificationData();
        }

        public List<DataTuple> GetEmployeePortalSetupData()
        {
            return dataDAL.GetEmployeePortalSetupData();
        }

        public List<DataTuple> GetEmployeeStatisticsGroupData()
        {
            return dataDAL.GetEmployeeStatisticsGroupData();
        }

    }
}
