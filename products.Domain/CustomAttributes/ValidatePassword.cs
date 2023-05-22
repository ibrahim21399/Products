using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace products.Domain.CustomAttributes
{
    public class ValidatePassword : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var PasswordPatten = @"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,}";
                return Regex.IsMatch(value.ToString(), PasswordPatten);
            }
            else
            {
                return true;
            }
        }
    }
}
