using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Music.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Music.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FreeSong : Page
    {
        private ObservableCollection<Song> FreeSongs { get; set; }
        private SongService songService;
        public static ListView FreeList;
        public FreeSong()
        {
            this.InitializeComponent();
            FreeList = this.FreeListSong;
            songService = new SongServiceImp();
            this.FreeSongs = new ObservableCollection<Song>();
            List<Song> listSong = songService.GetFreeSongs();
            Naview.FreeSongs = listSong;
            foreach (Song item in listSong)
            {
                this.FreeSongs.Add(new Song()
                {
                    name = item.name,
                    singer = item.singer,
                    thumbnail = item.thumbnail,
                    link = item.link
                });
            }
            if (Naview.listPlaying.Equals("FREE_SONG"))
            {
                this.FreeListSong.SelectedIndex = Naview._currentIndex;
            }
        }

        private void FreeListSong_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var song = e.ClickedItem as Song;
            Debug.WriteLine(song.name);
            Naview.MyMediaPlayer.Source = new Uri(song.link);
            Naview.listPlaying = "FREE_SONG";
            Naview._currentIndex = FreeSongs.IndexOf(song);
            Naview.NamePlaying.Text = song.name;
            Naview.btnStatus.Icon = new SymbolIcon(Symbol.Pause);
            Debug.WriteLine(FreeSongs.IndexOf(song));
        }
    }
}
