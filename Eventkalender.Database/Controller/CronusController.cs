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
        
        public List<DataTuple> GetEmployeeMetadata()
        {
            return metadataDAL.GetEmployeeMetadata();
        }

        public List<DataTuple> GetEmployeeAbsenceMetadata()
        {
            return metadataDAL.GetEmployeeAbsenceMetadata();
        }

        public List<DataTuple> GetEmployeeRelativeMetadata()
        {
            return metadataDAL.GetEmployeeRelativeMetadata();
        }

        public List<DataTuple> GetEmployeeQualificationMetadata()
        {
            return metadataDAL.GetEmployeeQualificationMetadata();
        }

        public List<DataTuple> GetEmployeePortalSetupMetadata()
        {
            return metadataDAL.GetEmployeePortalSetupMetadata();
        }

        public List<DataTuple> GetEmployeeStatisticsGroupMetadata()
        {
            return metadataDAL.GetEmployeeStatisticsGroupMetadata();
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

        // HÄR BÖRJAR SPECIFIKA EMPLOYEE-OPERATIONER

        public void AddEmployee(string no, string firstName, string lastName)
        {
            Employee e = new Employee(no, firstName, lastName);
            dataDAL.AddEmployee(e);
        }

        public void DeleteEmployee(string no)
        {
            Employee e = new Employee();
            e.No = no;
            dataDAL.DeleteEmployee(e);
        }

        public Employee GetEmployee(string no)
        {
            return dataDAL.GetEmployee(no);
        }

        public List<Employee> GetEmployees()
        {
            return dataDAL.GetEmployees();
        }

        public void UpdateEmployee(string no, string firstName, string lastName)
        {
            Employee e = new Employee(no, firstName, lastName);
            dataDAL.UpdateEmployee(e);
        }

    }
}
