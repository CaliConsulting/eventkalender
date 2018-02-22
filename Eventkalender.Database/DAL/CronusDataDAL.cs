using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database
{
    public class CronusDataDAL
    {
        private string xmlPath;

        public CronusDataDAL(string xmlPath)
        {
            this.xmlPath = xmlPath;
        }

        public DataTuple GetIllestPerson()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 [First Name], COUNT(*) AS Sjukdagar FROM [CRONUS Sverige AB$Employee] a ");
            builder.Append("JOIN [CRONUS Sverige AB$Employee Absence] b ON a.No_ = b.[Employee No_] ");
            builder.Append("WHERE b.[Cause of Absence Code] = 'SJUK' GROUP BY[First Name] ORDER BY Sjukdagar DESC;");

            string query = builder.ToString();

            return GetData(query).First();
        }

        public List<DataTuple> GetIllPersonsByYear(int startYear, int endYear)
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT DISTINCT [No_], [First Name], a.[Last Name], a.Address, a.[Job Title] FROM [CRONUS Sverige AB$Employee] a ");
                builder.Append("JOIN [CRONUS Sverige AB$Employee Absence] b ON a.No_ = b.[Employee No_] ");
                builder.Append("WHERE b.[Cause of Absence Code] = 'SJUK' AND [From Date] BETWEEN @startDate AND @endDate");

                string query = builder.ToString();

                SqlCommand command = new SqlCommand(query, connection);

                DateTime startDate = new DateTime(startYear, 1, 1);
                DateTime endDate = new DateTime(endYear, 1, 1);

                SqlParameter startDateParam = new SqlParameter("@startDate", System.Data.SqlDbType.DateTime);
                startDateParam.Value = startDate;
                command.Parameters.Add(startDateParam);

                SqlParameter endDateParam = new SqlParameter("@endDate", System.Data.SqlDbType.DateTime);
                endDateParam.Value = endDate;
                command.Parameters.Add(endDateParam);

                command.Prepare();

                SqlDataReader reader = command.ExecuteReader();

                List<DataTuple> tuples = new List<DataTuple>();
                while (reader.Read())
                {
                    DataTuple tuple = new DataTuple();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        tuple.Add(reader.GetName(i), reader.GetValue(i).ToString());
                    }
                    tuples.Add(tuple);
                }
                return tuples;
            }
        }
        public List<DataTuple> GetEmployeeAndRelatives()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT a.[First Name], a.[Last Name], b.[Relative Code] AS Relative, b.[First Name], b.[Last Name] FROM ");
            builder.Append("[CRONUS Sverige AB$Employee] a JOIN [CRONUS Sverige AB$Employee Relative] b ON a.No_ = b.[Employee No_]");

            string query = builder.ToString();

            return GetData(query);
        }

        // Ta tre/fyra st attribut istället för allt (*)
        public List<DataTuple> GetEmployeeData()
        {
            string inputQuery = "SELECT No_, [First Name], [Last Name] FROM [CRONUS Sverige AB$Employee]";
            return GetData(inputQuery);
        }

        public List<DataTuple> GetEmployeeAbsenceData()
        {
            string inputQuery = "SELECT [Entry No_], [Employee No_], [Cause of Absence Code] FROM [CRONUS Sverige AB$Employee Absence]";
            return GetData(inputQuery);
        }

        public List<DataTuple> GetEmployeeRelativeData()
        {
            string inputQuery = "SELECT [Employee No_], [Relative Code], [First Name] FROM [CRONUS Sverige AB$Employee Relative]";
            return GetData(inputQuery);
        }

        public List<DataTuple> GetEmployeePortalSetupData()
        {
            string inputQuery = "SELECT [Primary Key], [Temp_ Key Index], [Temp_ Option Caption] FROM [CRONUS Sverige AB$Employee Portal Setup]";
            return GetData(inputQuery);
        }

        public List<DataTuple> GetEmployeeQualificationData()
        {
            string inputQuery = "SELECT [Employee No_], [Qualification Code], Description FROM [CRONUS Sverige AB$Employee Qualification]";
            return GetData(inputQuery);
        }

        public List<DataTuple> GetEmployeeStatisticsGroupData()
        {
            string inputQuery = "SELECT * FROM [CRONUS Sverige AB$Employee Statistics Group]";
            return GetData(inputQuery);
        }

        public List<DataTuple> GetData(string inputQuery)
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                string query = inputQuery;

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<DataTuple> tuples = new List<DataTuple>();
                while (reader.Read())
                {
                    DataTuple person = new DataTuple();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        person.Add(reader.GetName(i), reader.GetValue(i).ToString());
                    }
                    tuples.Add(person);
                }
                return tuples;
            }
        }

        public void AddEmployee(Employee e)
        {
            using (var context = new CronusContext(xmlPath))
            {
                context.Employee.Add(e);
                context.SaveChanges();
            }
        }

        public void DeleteEmployee(Employee e)
        {
            using (var context = new CronusContext(xmlPath))
            {
                context.Employee.Attach(e);
                context.Employee.Remove(e);
                context.SaveChanges();
            }
        }

        public Employee GetEmployee(string no)
        {
            using (var context = new CronusContext(xmlPath))
            {
                Employee dbEmployee = context.Employee.SingleOrDefault(e => e.No.Equals(no));
                return dbEmployee;
            }
        }

        public List<Employee> GetEmployees()
        {
            using (var context = new CronusContext(xmlPath))
            {
                List<Employee> dbEmployees = context.Employee.ToList();
                return dbEmployees;
            }
        }

        public void UpdateEmployee(Employee e)
        {
            using (var context = new CronusContext(xmlPath))
            {
                Employee dbEmployee = context.Employee.Find(e.No);
                if (dbEmployee == null)
                {
                    return;
                }
                context.Entry(dbEmployee).CurrentValues.SetValues(e);
                context.SaveChanges();
            }
        }
    }
}
