using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Navigation;
using Music.Constant;
using Music.Entity;
using Music.Service;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Music.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListSong : Page
    {
        private ObservableCollection<Song> ListSongs { get; set; }
        private SongService songService;
        public static ListView NewList;
        public ListSong()
        {
            this.InitializeComponent();
            NewList = this.NewListSong;
            songService = new SongServiceImp();
            this.ListSongs = new ObservableCollection<Song>();
            List<Song> listSong = songService.GetNewSongs();
            if (listSong != null)
            {
                Naview.NewSongs = listSong;
                this.login.Visibility = Visibility.Collapsed;
                foreach (Song item in listSong)
                {
                    this.ListSongs.Add(new Song()
                    {
                        name = item.name,
                        singer = item.singer,
                        thumbnail = item.thumbnail,
                        link = item.link
                    });
                }
                if (Naview.listPlaying.Equals("NEW_SONG"))
                {
                    NewListSong.SelectedIndex = Naview._currentIndex;
                }
            }
            else
            {
                this.login.Visibility = Visibility.Visible;
            }
        }

        private void NewSong_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var song = e.ClickedItem as Song;
            if (song != null) Debug.WriteLine(song.name);
            Naview.MyMediaPlayer.Source = new Uri(song.link);
            Naview._isPlay = true;
            Naview.listPlaying = "NEW_SONG";
            Naview._currentIndex = ListSongs.IndexOf(song);
            Naview.NamePlaying.Text = song.name;
            Naview.btnStatus.Icon = new SymbolIcon(Symbol.Pause);
            Debug.WriteLine(ListSongs.IndexOf(song));
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
           Naview.MainFrame.Navigate(typeof(Login));
        }
    }
}
