using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Music.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyInformation : Page
    {
        private const string ApiUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/information";

        public MyInformation()
        {

            this.InitializeComponent();
            //doc token tu file
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                storageFolder.GetFileAsync("sample.txt").GetAwaiter().GetResult();
            var token = Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult().ToString();
            Debug.WriteLine(token);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            Task<HttpResponseMessage> httpRequestMessage = httpClient.GetAsync(ApiUrl);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            Member resMember = JsonConvert.DeserializeObject<Member>(responseContent);

            Debug.WriteLine(resMember);
            
            this.PersonPicture.ProfilePicture = new BitmapImage(new Uri(resMember.avatar));
            this.Name.Text = "Name: " + resMember.lastName + " " + resMember.firstName;
            if (resMember.gender == 1)
            {
                this.Gender.Text = "Gender: Male";
            }
            else
            {
                this.Gender.Text = "Gender: Female";
            }
            
            this.email.Text = "Email: " + resMember.email;
            this.Address.Text = "Address: " + resMember.address;
            this.Birthday.Text = "Birthday: " + resMember.birthday;
            this.Phone.Text = "Phone: " + resMember.phone;
            this.introduction.Text = "Introdution: " + resMember.introduction;
        }
    }
}
