using System.Text.RegularExpressions;

namespace Kerem.CodingTracker.Features.CreateCodingSession ;

    public static class CreateCodingSessionValidator
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
        
    }