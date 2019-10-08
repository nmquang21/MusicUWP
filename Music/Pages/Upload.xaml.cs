using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Music.Entity;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Music.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Upload : Page
    {
        string ApiUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/post-free";
        public Upload()
        {
            this.InitializeComponent();
        }
       
        private void ButtonUpload_OnClick(object sender, RoutedEventArgs e)
        {
          
            var song = new Song()
            {
                name = this.name.Text,
                description = this.description.Text,
                singer = this.singer.Text,
                author = this.author.Text,
                thumbnail = this.thumbnail.Text,
                link = this.link.Text,
            };
            var errors = new Dictionary<string, string>();
            errors = song.ValidateData();
            if (errors.Count==0)
            {
                var httpClient = new HttpClient();
                //httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(song), Encoding.UTF8,
                    "application/json");
                Task<HttpResponseMessage> httpRequestMessage = httpClient.PostAsync(ApiUrl, content);
                String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("Response: " + responseContent);
                this.name.Text = string.Empty;
                this.description.Text = string.Empty;
                this.singer.Text = string.Empty;
                this.author.Text = string.Empty;
                this.thumbnail.Text = string.Empty;
                this.link.Text = string.Empty;
                this.name_validate.Visibility = Visibility.Collapsed;
                this.link_validate.Visibility = Visibility.Collapsed;
                this.thumbnail_validate.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (errors.ContainsKey("name"))
                {
                    this.name_validate.Text = errors["name"];
                    this.name_validate.Visibility = Visibility;
                }
                else
                {
                    this.name_validate.Visibility = Visibility.Collapsed;
                }

                if (errors.ContainsKey("thumbnail"))
                {
                    this.thumbnail_validate.Text = errors["thumbnail"];
                    this.thumbnail_validate.Visibility = Visibility;
                }
                else
                {
                    this.thumbnail_validate.Visibility = Visibility.Collapsed;
                }
                if (errors.ContainsKey("link"))
                {
                    this.link_validate.Text = errors["link"];
                    this.link_validate.Visibility = Visibility;
                }
                else
                {
                    this.link_validate.Visibility = Visibility.Collapsed;
                }
            }
           
        }
        private void ButtonReset_OnClick(object sender, RoutedEventArgs e)
        {
            this.name.Text = string.Empty;
            this.description.Text = string.Empty;
            this.singer.Text = string.Empty;
            this.author.Text = string.Empty;
            this.thumbnail.Text = string.Empty;
            this.link.Text = string.Empty;
        }
    }
}

