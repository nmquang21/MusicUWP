using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Music.Entity;
using Newtonsoft.Json;
using CheckBox = Windows.UI.Xaml.Controls.CheckBox;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Music.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private const string ApiUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";
        private const string getUrlUploat = "https://2-dot-backup-server-003.appspot.com/get-upload-token";
        private int gender = -1;
        private StorageFile photo;
        private string currentUploadUrl = "";
        public Register()
        {
            this.InitializeComponent();
        }


        private void Gender_Checked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ok");
            var gender_checked = (RadioButton) sender;

            if (gender_checked != null)
            {
                switch (gender_checked.Tag)
                {
                    case "gender0":
                        gender = 0;
                        break;
                    case "gender1":
                        gender = 1;
                        break;
                }
            }
            
        }

        private void ButtonRegister_OnClick(object sender, RoutedEventArgs e)
        {
            var member = new Member()
            {
                firstName = this.FirstName.Text,
                lastName = this.LastName.Text,
                avatar = this.AvatarUrl.Text,
                phone = this.Phone.Text,
                password = this.Password.Password,
                address = this.Address.Text,
                introduction = this.Introduction.Text,
                birthday = this.Birthday.Date.ToString("yyyy-MM-dd"),
                email = this.Email.Text,
                gender = gender,
            };
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8,
                "application/json");

            Task<HttpResponseMessage> httpRequestMessage = httpClient.PostAsync(ApiUrl, content);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Response: " + responseContent);
        }

        private void ButtonReset_OnClick(object sender, RoutedEventArgs e)
        {
            this.FirstName.Text = string.Empty;
            this.LastName.Text = string.Empty;
            //this.Avatar.Text = string.Empty;
            this.Phone.Text = string.Empty;
            this.Password.Password = string.Empty;
            this.Address.Text = string.Empty;
            this.Introduction.Text = string.Empty;
            this.Email.Text = string.Empty;
        }

        public void getUploadUrl()
        {
            var httpClient = new HttpClient();
            Task<HttpResponseMessage> httpRequestMessage = httpClient.GetAsync(getUrlUploat);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            currentUploadUrl = responseContent.ToString();
            Debug.WriteLine(currentUploadUrl);
        }
        private async void  TakeAPhoto(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture 
                return;
            }

            getUploadUrl();
            HttpUploadFile(currentUploadUrl, "myFile", "image/png");
        }
        public async void HttpUploadFile(string url, string paramName, string contentType)        {            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);            wr.ContentType = "multipart/form-data; boundary=" + boundary;            wr.Method = "POST";            Stream rs = await wr.GetRequestStreamAsync();            rs.Write(boundarybytes, 0, boundarybytes.Length);            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await this.photo.OpenStreamForReadAsync();            byte[] buffer = new byte[4096];            int bytesRead = 0;            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)            {                rs.Write(buffer, 0, bytesRead);            }            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");            rs.Write(trailer, 0, trailer.Length);            WebResponse wresp = null;            try            {                wresp = await wr.GetResponseAsync();                Stream stream2 = wresp.GetResponseStream();                StreamReader reader2 = new StreamReader(stream2);
                //Debug.WriteLine(string.Format("File uploaded, server response is: @{0}@", reader2.ReadToEnd()));
                //string imgUrl = reader2.ReadToEnd();
                //Uri u = new Uri(reader2.ReadToEnd(), UriKind.Absolute);
                //Debug.WriteLine(u.AbsoluteUri);
                //ImageUrl.Text = u.AbsoluteUri;
                //MyAvatar.Source = new BitmapImage(u);
                //Debug.WriteLine(reader2.ReadToEnd());
                string imageUrl = reader2.ReadToEnd();                this.Avatar.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));                Debug.WriteLine(imageUrl);
                AvatarUrl.Text = imageUrl;
            }            catch (Exception ex)            {                Debug.WriteLine("Error uploading file", ex.StackTrace);                Debug.WriteLine("Error uploading file", ex.InnerException);                if (wresp != null)                {                    wresp = null;                }            }            finally            {                wr = null;            }        }
    }
}
