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

    }
}
