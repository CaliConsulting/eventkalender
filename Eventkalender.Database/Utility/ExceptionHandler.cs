using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database
{
    public class ExceptionHandler
    {
        private const int CANNOT_INSERT_NULL = 515;
        //private const int CHECK_CONSTRAINT_CONFLICT = 547;
        private const int DATA_TYPE_CONVERSION_ERROR = 8114;
        private const int LOGIN_FAILED = 4060;
        private const int NON_MATCHING_TABLE_DEFINITION = 213;
        //private const int PRIMARY_KEY_VIOLATION = 2627;
        private const int RAISE_ERROR = 50000;
        private const int TRUNCATED_DATA = 8152;
        private const int WRONG_CREDENTIALS = 18456;

        public static string GetErrorMessage(Exception ex)
        {
            bool isSql = ex is SqlException;
            if (isSql)
            {
                SqlException sqlEx = ex as SqlException;
                return GetSqlErrorMessage(sqlEx);
            }
            return GetGenericErrorMessage(ex);
        }

        private static string GetSqlErrorMessage(SqlException ex)
        {
            string message = ex.Message;
            switch (ex.ErrorCode)
            {
                case CANNOT_INSERT_NULL:
                    return MessageHelper.GetCannotInsertNull(message);
                case DATA_TYPE_CONVERSION_ERROR:
                    return MessageHelper.GetCannotInsertNull(message);
                case LOGIN_FAILED:
                    return "Inloggningen till databasen misslyckades; kontrollera användarnamn och lösenord";
                case NON_MATCHING_TABLE_DEFINITION:
                    return "Databasen accepterar inte indatan";
                case RAISE_ERROR:
                    return message;
                case TRUNCATED_DATA:
                    return "Ett indata-fält överskrider maximala tillåtna längden";
                case WRONG_CREDENTIALS:
                    return MessageHelper.GetWrongCredentials(message);
            }
            return ex.Message;
        }

        private static string GetGenericErrorMessage(Exception ex)
        {
            if (ex is NullReferenceException)
            {
                // Detta felmeddelandet måste nog göras om
                return "Fältet accepterar inte denna indata, var god kontrollera formattering på tal och text";
            }
            return ex.Message;
        }

        private static class MessageHelper
        {
            public static string GetCannotInsertNull(string message)
            {
                return "";
            }

            public static string GetDataTypeConversionError(string message)
            {
                return "";
            }

            public static string GetWrongCredentials(string message)
            {
                return "";
            }
        }

    }
}
