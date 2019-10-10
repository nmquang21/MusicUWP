using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Entity
{
    public class Song
    {
        public string name { get; set; }
        public string description { get; set; }
        public string singer { get; set; }
        public string author { get; set; }
        public string thumbnail { get; set; }
        public string link { get; set; }

        internal Dictionary<string, string> ValidateData()
        {
            var error = new Dictionary<string,string>();
            if (string.IsNullOrEmpty(name))
            {
                error.Add("name","Name is require!");
            }
            else if (name.Length < 3 || name.Length > 50)
            {
                error.Add("name","Name must be 3 to 50 character");
            }
            
            if (string.IsNullOrEmpty(thumbnail))
            {
                error.Add("thumbnail", "Thumbnail is require!");
            }
            
            if (string.IsNullOrEmpty(link))
            {
                error.Add("link", "Link is require!");
            }
            //else if (link.Length<4 || !link.Substring(link.Length-4).Equals(".mp3"))
            //{
            //    error.Add("link", "Link is end by .mp3!");
            //}

            if (string.IsNullOrEmpty(singer))
            {
                error.Add("singer", "Singer is require!");
            }
            return error;
        }
    }
}
