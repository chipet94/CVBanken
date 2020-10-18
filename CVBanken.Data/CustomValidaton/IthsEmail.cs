using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CVBanken.Data.CustomValidaton
{
    public class IthsEmail : ValidationAttribute
    {
        public override bool IsValid(object email)
        {
            return ValidateEmail(email.ToString());
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                var ithsPrefix = "@ITHS.SE";
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    //0 = @iths.se  || 1 = @ || 2 = iths.se
                    if (match.Groups[2].ToString().ToUpper() != "ITHS.SE")
                    {
                        throw new ArgumentException();
                    }
                    return match.Groups[1].Value + match.Groups[2];
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}