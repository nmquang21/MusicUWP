using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Music.Constant;
using Music.Entity;
using Newtonsoft.Json;

namespace Music.Service
{
    class SongServiceImp:SongService
    {
        public Song UploadSong(Song song)
        {
            string token = GetTokenFromLocalStorage();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(song), Encoding.UTF8,
                "application/json");
            Task<HttpResponseMessage> httpRequestMessage = httpClient.PostAsync(ApiUrl.SONG_URL, content);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Response: " + responseContent);
            return JsonConvert.DeserializeObject<Song>(responseContent);
        }

        public Song UploadFreeSong(Song song)
        {
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(song), Encoding.UTF8,
                "application/json");
            Task<HttpResponseMessage> httpRequestMessage = httpClient.PostAsync(ApiUrl.UPLOAD_FREE_SONG_URL, content);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Response: " + responseContent);
            return JsonConvert.DeserializeObject<Song>(responseContent);
        }

        public List<Song> GetNewSongs()
        {
            string token = GetTokenFromLocalStorage();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            Task<HttpResponseMessage> httpRequestMessage = httpClient.GetAsync(ApiUrl.SONG_URL);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Response: " + responseContent);
            try
            {
                return JsonConvert.DeserializeObject<List<Song>>(responseContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<Song> GetMySongs()
        {
            string token = GetTokenFromLocalStorage();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            Task<HttpResponseMessage> httpRequestMessage = httpClient.GetAsync(ApiUrl.GET_MY_SONG_URL);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Response: " + responseContent);
            try
            {
                return JsonConvert.DeserializeObject<List<Song>>(responseContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            
        }

        public List<Song> GetFreeSongs()
        {
            var httpClient = new HttpClient();
            Task<HttpResponseMessage> httpRequestMessage = httpClient.GetAsync(ApiUrl.GET_FREE_SONG_URL);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Response: " + responseContent);
            try
            {
                return JsonConvert.DeserializeObject<List<Song>>(responseContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private string GetTokenFromLocalStorage()
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                Windows.Storage.StorageFile sampleFile =
                    storageFolder.GetFileAsync("ez.txt").GetAwaiter().GetResult();
                return Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult().ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("File Not Found");
                return "";
            }
            
        }
    }
}
