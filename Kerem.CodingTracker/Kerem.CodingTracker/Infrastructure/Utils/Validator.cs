using System.Text.RegularExpressions;
using Spectre.Console;

namespace Kerem.CodingTracker.Features.CreateCodingSession ;

    public static class Validator
    {
        public static bool ValidateDateFormat(String date)
        {
            string pattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]) ([01]\d|2[0-3]):[0-5]\d$";
            if (Regex.IsMatch(date, pattern))
            {
                return true;
            }
            return false;
        }

        public static bool Abort(string choice)
        {
            if (choice == "abort")
            {
                return true;
            }
            return false;
        }

        public static bool ValidateStartAndEndDate(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return false;
            }
            return true;
        }
        
    }