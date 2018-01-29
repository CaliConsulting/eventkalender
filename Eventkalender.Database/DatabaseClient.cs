using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Eventkalender.Database
{
    public class DatabaseClient
    {
        public static SqlConnection GetConnection(string xmlPath)
        {
            Dictionary<string, string> values = ReadXmlFile(xmlPath);
            string dataSource = values["DataSource"];
            string database = values["Database"];
            string username = values["Username"];
            string password = values["Password"];

            String connectionString = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", dataSource, database, username, password);

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public static Dictionary<string, string> ReadXmlFile(string xmlPath)
        {
            XDocument doc = XDocument.Load(xmlPath);
            Dictionary<string, string> xmlValues = new Dictionary<string, string>();
            xmlValues.Add("DataSource", doc.Root.Element("DataSource").Value);
            xmlValues.Add("Database", doc.Root.Element("Database").Value);
            xmlValues.Add("Username", doc.Root.Element("Username").Value);
            xmlValues.Add("Password", doc.Root.Element("Password").Value);
            return xmlValues;
        }
    }
}
