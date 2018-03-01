using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
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

        public static string GetSqlServerConnectionString(string xmlPath)
        {
            Dictionary<string, string> values = ReadXmlFile(xmlPath);
            string dataSource = values["DataSource"];
            string database = values["Database"];
            string username = values["Username"];
            string password = values["Password"];

            return string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", dataSource, database, username, password);
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