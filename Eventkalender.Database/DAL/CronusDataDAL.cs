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
        public List<Model.Tuple> GetSickPersonByYear(int startYear, int endYear)
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT DISTINCT[No_], [First Name], a.[Last Name], a.Address, a.[Job Title] FROM [CRONUS Sverige AB$Employee] a ");
                builder.Append("JOIN [CRONUS Sverige AB$Employee Absence] b ON a.No_ = b.[Employee No_] ");
                builder.Append("WHERE b.[Cause of Absence Code] = 'SJUK' AND [From Date] >= '@startDate' ");
                builder.Append("AND [From Date] < '@endDate'");
                string query = builder.ToString();

                DateTime startDate = new DateTime(startYear, 1, 1);
                DateTime endDate = new DateTime(endYear, 1, 1);

                SqlParameter endYearParam = (new SqlParameter("startDate", System.Data.SqlDbType.DateTime));
                SqlParameter startYearParam = (new SqlParameter("endDate", System.Data.SqlDbType.DateTime));
                endYearParam.Value = endDate;
                startYearParam.Value = startDate;

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add(endYearParam);
                command.Parameters.Add(startYearParam);
                command.Prepare();
                String s = command.CommandText;
             

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

    }
}
