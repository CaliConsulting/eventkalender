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

        public CronusMetadataDAL(string xmlPath)
        {
            this.xmlPath = xmlPath;
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

                // Add column value
                keys.Add("1");

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

                // Add column value
                indexes.Add("1");

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

                // Add column value
                constraints.Add("1");

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

                // Add column value
                tables.Add("1");

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

                // Add column value
                columns.Add("1");

                while (reader.Read())
                {
                    columns.Add(reader.IsDBNull(0) ? null : reader.GetString(0));
                }
                return columns;
            }
        }

        public List<DataTuple> GetMetadata(string inputQuery)
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
                    DataTuple data = new DataTuple();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        data.Add(reader.GetName(i), reader.GetValue(i).ToString());
                    }
                    tuples.Add(data);
                }
                return tuples;
            }
        }

        public List<DataTuple> GetEmployeeMetadata()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS ");
            builder.Append("WHERE TABLE_NAME = 'CRONUS Sverige AB$Employee'");
            string inputQuery = builder.ToString();
            return GetMetadata(inputQuery);

        }

        public List<DataTuple> GetEmployeeAbsenceMetadata()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS ");
            builder.Append("WHERE TABLE_NAME = 'CRONUS Sverige AB$Employee Absence'");
            string inputQuery = builder.ToString();
            return GetMetadata(inputQuery);
            
        }

        public List<DataTuple> GetEmployeeRelativeMetadata()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS ");
            builder.Append("WHERE TABLE_NAME = 'CRONUS Sverige AB$Employee Relative'");
            string inputQuery = builder.ToString();
            return GetMetadata(inputQuery);

        }

        public List<DataTuple> GetEmployeePortalSetupMetadata()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS ");
            builder.Append("WHERE TABLE_NAME = 'CRONUS Sverige AB$Employee Portal Setup'");
            string inputQuery = builder.ToString();
            return GetMetadata(inputQuery);

        }

        public List<DataTuple> GetEmployeeQualificationMetadata()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS ");
            builder.Append("WHERE TABLE_NAME = 'CRONUS Sverige AB$Employee Qualification'");
            string inputQuery = builder.ToString();
            return GetMetadata(inputQuery);

        }

        public List<DataTuple> GetEmployeeStatisticsGroupMetadata()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS ");
            builder.Append("WHERE TABLE_NAME = 'CRONUS Sverige AB$Employee Statistics Group'");
            string inputQuery = builder.ToString();
            return GetMetadata(inputQuery);

        }
    }
}
