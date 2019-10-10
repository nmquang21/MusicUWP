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
using Newtonsoft.Json.Linq;

namespace Music.Service
{
    class MemberServiceImp:MemberService
    {
        
        public Member GetInformation()
        {
            string token = GetTokenFromLocalStorage();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            Task<HttpResponseMessage> httpRequestMessage = httpClient.GetAsync(ApiUrl.GET_INFORMATION_URL);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(responseContent);
            Member  member =  JsonConvert.DeserializeObject<Member>(responseContent);
            return member;
        }

       

        public string Login(MemberLogin memberLogin)
        {
            try
            {
                // lấy token từ api.
                var token = GetTokenFromApi(memberLogin);
                //lưu token ra file để dùng lại
                SaveTokenToLocalStorage(token);
                Debug.WriteLine("Login Success!!");
                return token;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public Member Register(Member member)
        {
            try
            {
                var httpClient = new HttpClient();
                HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8, "application/json");
                var httpRequestMessage = httpClient.PostAsync(ApiUrl.REGISTER_URL, content);
                var responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(responseContent);
                var resMember = JsonConvert.DeserializeObject<Member>(responseContent);
                return resMember;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }

        }

        private String GetTokenFromApi(MemberLogin memberLogin)
        {
            // thực hiện request lên api lấy token về.
            var dataContent = new StringContent(JsonConvert.SerializeObject(memberLogin), Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var responseContent = client.PostAsync(ApiUrl.LOGIN_URL, dataContent).Result.Content.ReadAsStringAsync().Result;
            var jsonJObject = JObject.Parse(responseContent);
            if (jsonJObject["token"] == null)
            {
                throw new Exception("Login failsss");
            }
            return jsonJObject["token"].ToString();
        }

        public void SaveTokenToLocalStorage(string token)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = storageFolder.CreateFileAsync("ez.txt",
                Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
            Windows.Storage.FileIO.WriteTextAsync(sampleFile, token).GetAwaiter().GetResult();
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
                Console.WriteLine(e);
                return "";
            }
            
        }
        public string GetUploadAvatarUrl()
        {
            var httpClient = new HttpClient();
            Task<HttpResponseMessage> httpRequestMessage = httpClient.GetAsync(ApiUrl.GET_URL_UPLOAD_AVATAR);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            return responseContent.ToString();
        }
        private bool ValidateMemberLogin(MemberLogin memberLogin)
        {
            return true;
        }

        public void logout()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = storageFolder.CreateFileAsync("ez.txt",
                Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
            Windows.Storage.FileIO.WriteTextAsync(sampleFile, "").GetAwaiter().GetResult();
        }
    }
}
