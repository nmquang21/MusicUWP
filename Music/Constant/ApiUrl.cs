using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Constant
{
    class ApiUrl
    {
        public const string API_BASE_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2";
        public const string REGISTER_URL = API_BASE_URL + "/members";
        public const string LOGIN_URL = API_BASE_URL + "/members/authentication";
        public const string GET_INFORMATION_URL = API_BASE_URL + "/members/information";
        public const string GET_FREE_SONG_URL = API_BASE_URL + "/songs/get-free-songs";
        public const string SONG_URL = API_BASE_URL + "/songs";
        public const string GET_MY_SONG_URL = API_BASE_URL + "/songs/get-mine";
        public const string GET_URL_UPLOAD_AVATAR = "https://2-dot-backup-server-003.appspot.com/get-upload-token";
    }
}
