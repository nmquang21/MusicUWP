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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Music.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        
        public Home()
        {
            var Url = "http://localhost:8080/Quang_T1807E/public/listCT";
            this.InitializeComponent();
            //this.Image.Source = new BitmapImage(new Uri("https://cdn4.vectorstock.com/i/1000x1000/32/18/user-sign-icon-person-symbol-human-avatar-vector-12693218.jpg"));
            //var httpClient = new HttpClient();
            //Task<HttpResponseMessage> httpRequestMessage = httpClient.GetAsync(Url);
            //String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            //var currentUploadUrl = responseContent.ToString();
            //Debug.WriteLine(currentUploadUrl);
        }
    }
}
