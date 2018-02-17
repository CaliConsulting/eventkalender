using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
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
        private const int PRIMARY_KEY_VIOLATION = 2627;
        private const int RAISE_ERROR = 50000;
        private const int TRUNCATED_DATA = 8152;
        private const int WRONG_CREDENTIALS = 18456;

        public static string GetErrorMessage(Exception ex)
        {
            if (ex is DataException)
            {
                DataException dataEx = ex as DataException;
                return GetDataErrorMessage(dataEx);
            }
            else if (ex is SqlException)
            {
                SqlException sqlEx = ex as SqlException;
                return GetSqlErrorMessage(sqlEx);
            }
            return GetGenericErrorMessage(ex);
        }

        private static string GetDataErrorMessage(DataException ex)
        {
            if (ex is DbEntityValidationException)
            {
                DbEntityValidationException valEx = ex as DbEntityValidationException;
                return DataMessageHelper.GetDbEntityValidationExceptionMessage(valEx);
            }
            return GetGenericErrorMessage(ex);
        }

        private static string GetSqlErrorMessage(SqlException ex)
        {
            string message = ex.Message;
            switch (ex.ErrorCode)
            {
                case CANNOT_INSERT_NULL:
                    return MessageHelper.GetCannotInsertNullMessage(message);
                case DATA_TYPE_CONVERSION_ERROR:
                    return MessageHelper.GetDataTypeConversionErrorMessage(message);
                case LOGIN_FAILED:
                    return "Inloggningen till databasen misslyckades; kontrollera användarnamn och lösenord";
                case NON_MATCHING_TABLE_DEFINITION:
                    return "Databasen accepterar inte indatan för ett fält";
                case PRIMARY_KEY_VIOLATION:
                    return MessageHelper.GetPrimaryKeyViolationMessage(message);
                case RAISE_ERROR:
                    return message;
                case TRUNCATED_DATA:
                    return "Ett indata-fält överskrider maximala tillåtna längden";
                case WRONG_CREDENTIALS:
                    return MessageHelper.GetWrongCredentialsMessage(message);
            }
            return ex.Message;
        }

        private static string GetGenericErrorMessage(Exception ex)
        {
            if (ex is NullReferenceException)
            {
                return "Programmet stötte på null-värde som det inte kan hantera, var god kontrollera alla indata-fält.";
            }
            return ex.Message;
        }

        private static class MessageHelper
        {
            // Kanske förbättra dessa meddelande? T.ex. genom att extrahera värden från message-variabeln
            // som vi gjorde i databasprojektet.
            public static string GetCannotInsertNullMessage(string message)
            {
                return "Databasen accepterar inte null-värde, var god fyll i alla fält.";
            }

            public static string GetDataTypeConversionErrorMessage(string message)
            {
                return "Kan inte konvertera datatypen.";
            }

            public static string GetPrimaryKeyViolationMessage(string message)
            {
                return "Primärnyckeln används redan av en viss tupel, var god välj en annan.";
            }

            public static string GetWrongCredentialsMessage(string message)
            {
                return "Fel inloggningsuppgifter till databasen, var god kontrollera dessa och försök igen.";
            }
        }

        private static class DataMessageHelper
        {
            public static string GetDbEntityValidationExceptionMessage(DbEntityValidationException ex)
            {
                StringBuilder builder = new StringBuilder();
                ICollection<DbEntityValidationResult> validationResults = ex.EntityValidationErrors.ToList();
                for (int i = 0; i < validationResults.Count; i++)
                {
                    DbEntityValidationResult result = validationResults.ElementAt(i);
                    ICollection<DbValidationError> validationErrors = result.ValidationErrors;
                    for (int j = 0; j < validationErrors.Count; j++)
                    {
                        DbValidationError error = validationErrors.ElementAt(j);
                        builder.Append(error.ErrorMessage);
                        if (j != validationErrors.Count - 1)
                        {
                            builder.Append("; ");
                        }
                    }
                    if (i != validationResults.Count - 1)
                    {
                        builder.AppendLine();
                    }
                }
                return builder.ToString();
            }
        }
    }
}
