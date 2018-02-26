using System;

namespace Eventkalender.WS.ConsoleApp
{
    public class ValidationUtility
    {
        public static bool ValidateAlternative(int chosenAlternative, params int[] allAlternatives)
        {
            return ValidateAlternative(chosenAlternative.ToString(), Array.ConvertAll(allAlternatives, l => l.ToString()));
        }

        public static bool ValidateAlternative(string chosenAlternative, params string[] allAlternatives)
        {
            for (int i = 0; i < allAlternatives.Length; i++)
            {
                string alternative = allAlternatives[i];
                if (chosenAlternative != null && chosenAlternative.Length != 0 && chosenAlternative.ToUpper().Equals(alternative.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}