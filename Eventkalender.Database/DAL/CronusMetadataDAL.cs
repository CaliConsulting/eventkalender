using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database
{
    public class CronusMetadataDAL
    {
        private string xmlPath;

        public CronusMetadataDAL()
        {
            xmlPath = "cronus-db.xml";
        }

        public List<string> GetKeys()
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                string query = "SELECT DISTINCT CONSTRAINT_NAME AS name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE ORDER BY name";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<string> keys = new List<string>();
                while (reader.Read())
                {
                    keys.Add(reader.IsDBNull(0) ? null : reader.GetString(0));
                }
                return keys;
            }
        }

        public List<string> GetIndexes()
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                string query = "SELECT name FROM sys.indexes ORDER BY name";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<string> indexes = new List<string>();
                while (reader.Read())
                {
                    indexes.Add(reader.IsDBNull(0) ? null : reader.GetString(0));
                }
                return indexes;
            }
        }

        public List<string> GetTableConstraints()
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                string query = "SELECT CONSTRAINT_NAME AS name FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS ORDER BY name";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<string> constraints = new List<string>();
                while (reader.Read())
                {
                    constraints.Add(reader.IsDBNull(0) ? null : reader.GetString(0));
                }
                return constraints;
            }
        }

        public List<string> GetTables()
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                string query = "SELECT name FROM sys.tables ORDER BY name";

                // Lösning 2
                // SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_NAME

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<string> tables = new List<string>();
                while (reader.Read())
                {
                    tables.Add(reader.IsDBNull(0) ? null : reader.GetString(0));
                }
                return tables;
            }
        }

        public List<string> GetColumnsForEmployeeTable()
        {
            using (SqlConnection connection = DatabaseClient.GetConnection(xmlPath))
            {
                connection.Open();

                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT c.name AS name FROM sys.columns c JOIN sys.tables t ON c.object_id = t.object_id ");
                builder.Append("WHERE t.name = 'CRONUS Sverige AB$Employee' ORDER BY name");

                string query = builder.ToString();

                // Lösning 2
                // SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = [CRONUS Sverige AB$Employee];

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<string> columns = new List<string>();
                while (reader.Read())
                {
                    columns.Add(reader.IsDBNull(0) ? null : reader.GetString(0));
                }
                return columns;
            }
        }

    }
}
