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
using Windows.Media.Core;
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
            this.InitializeComponent();
        }
        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    await SetLocalMedia();
        //}

        //async private System.Threading.Tasks.Task SetLocalMedia()
        //{
        //    var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

        //    openPicker.FileTypeFilter.Add(".wmv");
        //    openPicker.FileTypeFilter.Add(".mp4");
        //    openPicker.FileTypeFilter.Add(".wma");
        //    openPicker.FileTypeFilter.Add(".mp3");

        //    var file = await openPicker.PickSingleFileAsync();

        //    // mediaPlayer is a MediaPlayerElement defined in XAML
        //    if (file != null)
        //    {
        //        mediaPlayer.Source = MediaSource.CreateFromStorageFile(file);

        //        mediaPlayer.MediaPlayer.Play();
        //    }
        //}
    }
}
