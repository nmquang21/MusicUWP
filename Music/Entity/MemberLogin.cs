using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Entity
{
    class MemberLogin
    {
        public string email { get; set; }
        public string password { get; set; }

        public Dictionary<string, string> ValidateData()
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(email))
            {
                errors.Add("email","Email is required!");
            }

            if (string.IsNullOrEmpty(password))
            {
                errors.Add("password", "Password is required!");
            }
            return errors;
        }
    }
}