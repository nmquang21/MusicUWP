using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Music.Entity
{
    class Member
    {
        public string id { get; set; }
        
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string avatar { get; set; }

        public string phone { get; set; }

        public string address { get; set; }

        public string introduction { get; set; }

        public int gender { get; set; }

        public string birthday { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public Dictionary<string, string> ValidateData()
        {
            Dictionary<string, string> errors= new Dictionary<string, string>();
            if (string.IsNullOrEmpty(firstName))
            {
                errors.Add("firstname","First name can not be empty!");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                errors.Add("lastname", "Last name can not be empty!");
            }
            if (string.IsNullOrEmpty(address))
            {
                errors.Add("address", "Address can not be empty!");
            }
            if (string.IsNullOrEmpty(phone))
            {
                errors.Add("phone", "Phone can not be empty!");
            }
            if (string.IsNullOrEmpty(avatar))
            {
                errors.Add("avatar", "Avatar can not be empty!");
            }
            if (string.IsNullOrEmpty(email))
            {
                errors.Add("email", "Email can not be empty!");
            }
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (!match.Success)
                {
                    errors.Add("email", "Email is invalid!");
                }
            }
            if (string.IsNullOrEmpty(password))
            {
                errors.Add("password", "Password can not be empty!");
            }
            return errors;
        }
    }
}

