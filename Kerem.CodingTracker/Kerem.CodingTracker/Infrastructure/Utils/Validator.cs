using System.Text.RegularExpressions;
using Spectre.Console;

namespace Kerem.CodingTracker.Features.CreateCodingSession ;

    public static class Validator
    {
        public static bool ValidateDateFormat(String date)
        {
            string pattern = @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}$";
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