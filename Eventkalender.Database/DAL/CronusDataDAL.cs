using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.DAL
{
    public class CronusDataDAL
    {

        private string xmlPath;

        public CronusDataDAL()
        {
            xmlPath = "cronus-db.xml";
        }
        public Model.Tuple GetIllestPerson()
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT TOP 1 [First Name], COUNT(*) AS Sjukdagar FROM [CRONUS Sverige AB$Employee] a ");
                builder.Append("JOIN [CRONUS Sverige AB$Employee Absence] b ON a.No_ = b.[Employee No_] ");
                builder.Append("WHERE b.[Cause of Absence Code] = 'SJUK' GROUP BY[First Name] ORDER BY Sjukdagar DESC;");
                string query = builder.ToString();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();


                Model.Tuple person = new Model.Tuple();
                reader.Read();

                for (int i = 0; i < reader.FieldCount; i++)
                {

                    person.Add(reader.GetName(i), reader.GetValue(i).ToString());
                       
                }



                return person;
            }
        }
        public List<Model.Tuple> GetIllPersonsByYear(int startYear, int endYear)
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
                
                List<Model.Tuple> tuples = new List<Model.Tuple>();
                while (reader.Read())
                {
                    Model.Tuple person = new Model.Tuple();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        person.Add(reader.GetName(i), reader.GetValue(i).ToString());
                    }
                    tuples.Add(person);
                }
                return tuples;
            }
        }
        public List<Model.Tuple> GetEmployeeAndRelatives()
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT a.[First Name], a.[Last Name], b.[Relative Code] AS Relative, b.[First Name], b.[Last Name] FROM ");
                builder.Append("[CRONUS Sverige AB$Employee] a JOIN [CRONUS Sverige AB$Employee Relative] b ON a.No_ = b.[Employee No_]");
      
                string query = builder.ToString();

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                List<Model.Tuple> tuples = new List<Model.Tuple>();
                while (reader.Read())
                {
                    Model.Tuple person = new Model.Tuple();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        person.Add(reader.GetName(i), reader.GetValue(i).ToString());
                    }
                    tuples.Add(person);
                }
                return tuples;
            }
        }

        public List<Model.Tuple> GetData(string inputQuery)
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                string query = inputQuery;

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<Model.Tuple> tuples = new List<Model.Tuple>();
                while (reader.Read())
                {
                    Model.Tuple person = new Model.Tuple();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        person.Add(reader.GetName(i), reader.GetValue(i).ToString());
                    }
                    tuples.Add(person);
                }
                return tuples;
            }
        }

        public List<Model.Tuple> GetEmployeeData()
        {
            string inputQuery = "SELECT * FROM [CRONUS Sverige AB$Employee]";
            return GetData(inputQuery);

        }

        public List<Model.Tuple> GetEmployeeAbsenceData()
        {
            string inputQuery = "SELECT * FROM [CRONUS Sverige AB$Employee Absence]";
            return GetData(inputQuery);

        }

        public List<Model.Tuple> GetEmployeeRelativeData()
        {
            string inputQuery = "SELECT * FROM [CRONUS Sverige AB$Employee Relative]";
            return GetData(inputQuery);

        }

        public List<Model.Tuple> GetEmployeePortalSetupData()
        {
            string inputQuery = "SELECT * FROM [CRONUS Sverige AB$Employee Portal Setup]";
            return GetData(inputQuery);

        }

        public List<Model.Tuple> GetEmployeeQualificationData()
        {
            string inputQuery = "SELECT * FROM [CRONUS Sverige AB$Employee Qualification]";
            return GetData(inputQuery);

        }

        public List<Model.Tuple> GetEmployeeStatisticsGroupData()
        {
            string inputQuery = "SELECT * FROM [CRONUS Sverige AB$Employee Statistics Group]";
            return GetData(inputQuery);

        }


    }
}
