using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Eventkalender.Database
{
    public class DatabaseClient
    {
        public static SqlConnection GetConnection(string xmlPath)
        {
            SqlConnection connection = new SqlConnection(GetSqlServerConnectionString(xmlPath));
            return connection;
        }

        public static string GetOdbcConnectionString(string xmlPath)
        {
            Dictionary<string, string> values = ReadXmlFile(xmlPath);
            string dataSource = values["DataSource"];
            string database = values["Database"];
            string username = values["Username"];
            string password = values["Password"];

            return String.Format("Driver={{ODBC Driver 13 for SQL Server}};server={0};database={1};uid={2};pwd={3};", dataSource, database, username, password);
        }

        public static string GetSqlServerConnectionString(string xmlPath)
        {
            Dictionary<string, string> values = ReadXmlFile(xmlPath);
            string dataSource = values["DataSource"];
            string database = values["Database"];
            string username = values["Username"];
            string password = values["Password"];

            return String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", dataSource, database, username, password);
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

        private static void WarmupEntityFramework2(object xmlPath)
        {
            string path = xmlPath as string;
            using (EventkalenderContext context = new EventkalenderContext(DatabaseClient.GetSqlServerConnectionString(path)))
            {
                context.Nation.FirstOrDefault();
            }
        }

        public static void WarmupEntityFramework(string xmlPath)
        {
            ParameterizedThreadStart pts = new ParameterizedThreadStart(WarmupEntityFramework2);
            Thread t = new Thread(pts);
            t.Start();
            Console.WriteLine("DONE");
        }

    }
}
