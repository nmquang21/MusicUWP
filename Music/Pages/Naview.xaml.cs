using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Music.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Music.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Naview : Page
    {
        public static List<Song> MySongs;
        public static List<Song> NewSongs;
        public static MediaElement MyMediaPlayer;
        public static Frame MainFrame;
        public static bool _isPlay = true;
        public static int _currentIndex = -1;
        public static int listPlaying = 0;  //0: My Songs
                                            //1: New Songs
        public static TextBlock NamePlaying;
        public static AppBarButton btnStatus;
        public Naview()
        {
            this.InitializeComponent();
            this.mediaPlayer.Volume = 1;
            MyMediaPlayer = this.mediaPlayer;
            MainFrame = this.ContentFrame;
            NamePlaying = this.ControlLabel;
            btnStatus = this.StatusButton;
            MySongs = new List<Song>();
            NewSongs = new List<Song>();
            //MyMediaPlayer.Source = MediaSource.CreateFromUri(new Uri("https://c1-ex-swe.nixcdn.com/NhacCuaTui982/BanDuyenRemix-DinhDungHtrolPhamThanh-5962025.mp3"));
            //https://data25.chiasenhac.com/downloads/2036/3/2035613-5a4faa89/128/Co%20Tham%20Khong%20Ve%20-%20Phat%20Ho_%20Jokes%20Bii_%20T.mp3
            //if (!GetTokenFromLocalStorage().Equals(""))
            //{
            //    this.Login.Visibility = Visibility.Collapsed;
            //    this.Register.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    this.Login.Visibility = Visibility.Visible;
            //    this.Register.Visibility = Visibility.Visible;
            //}
        }
        // Add "using" for WinUI controls.
        // using muxc = Microsoft.UI.Xaml.Controls;

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("home", typeof(Home)),
            ("apps", typeof(MainPage)),
            ("upload", typeof(Upload)),
            ("listsong", typeof(ListSong)),
            ("register", typeof(Register)),
            ("login", typeof(Login)),
            ("myInformation", typeof(MyInformation)),
            ("mysong", typeof(MySong)),
        };

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // Add handler for ContentFrame navigation.
            ContentFrame.Navigated += On_Navigated;

            // NavView doesn't load any page by default, so load home page.
            NavView.SelectedItem = NavView.MenuItems[0];
            // If navigation occurs on SelectionChanged, this isn't needed.
            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the home page.
            NavView_Navigate("home", new EntranceNavigationTransitionInfo());

            // Add keyboard accelerators for backwards navigation.
            var goBack = new KeyboardAccelerator { Key = VirtualKey.GoBack };
            goBack.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(goBack);

            // ALT routes here
            var altLeft = new KeyboardAccelerator
            {
                Key = VirtualKey.Left,
                Modifiers = VirtualKeyModifiers.Menu
            };
            altLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(altLeft);
        }
        //get token 
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
        
        private void NavView_ItemInvoked(NavigationView sender,
            NavigationViewItemInvokedEventArgs args)
        {
            
            if (args.IsSettingsInvoked == true)
            {
                Debug.WriteLine("setting");
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                Debug.WriteLine("ItemContainer");
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                if (navItemTag.Equals("login") && !GetTokenFromLocalStorage().Equals(""))
                {
                    NavView_Navigate("myInformation", args.RecommendedNavigationTransitionInfo);
                }
                else
                {
                    NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
                }
                
            }
        }

        // NavView_SelectionChanged is not used in this example, but is shown for completeness.
        // You will typically handle either ItemInvoked or SelectionChanged to perform navigation,
        // but not both.
        private void NavView_SelectionChanged(NavigationView sender,
            NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected == true)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.SelectedItemContainer != null)
            {
                var navItemTag = args.SelectedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;
            if (navItemTag == "settings")
            {
                _page = typeof(MainPage);
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }

            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                ContentFrame.Navigate(_page, null, transitionInfo);
            }
        }

        private void NavView_BackRequested(NavigationView sender,
            NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }

        private void BackInvoked(KeyboardAccelerator sender,
            KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(MainPage))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
                NavView.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                NavView.Header =
                    ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }

        

        private void StatusButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isPlay == true)
            {
                PauseSong();
            }
            else
            {
               PlaySong();
            }
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            _currentIndex++;
            if (listPlaying == 0)
            {
                if (_currentIndex >= MySongs.Count || _currentIndex < 0)
                {
                    _currentIndex = 0;
                }
            }
            else if (listPlaying == 1)
            {
                if (_currentIndex >= NewSongs.Count || _currentIndex < 0)
                {
                    _currentIndex = 0;
                }
            }
            PlayOtherSong();

        }
        private void PreviousButton_OnClick(object sender, RoutedEventArgs e)
        {
            _currentIndex--;
            if (listPlaying == 0)
            {
                if (_currentIndex < 0)
                {
                    _currentIndex = MySongs.Count - 1;
                }
                else if (_currentIndex >= MySongs.Count)
                {
                    _currentIndex = 0;
                }
            }
            else if (listPlaying == 1)
            {
                if (_currentIndex < 0)
                {
                    _currentIndex = NewSongs.Count - 1;
                }
                else if (_currentIndex >= NewSongs.Count)
                {
                    _currentIndex = 0;
                }
            }

            PlayOtherSong();
        }

        public void PlayOtherSong()
        {
            if (listPlaying == 0 && MySongs.Count>0)
            {
                MyMediaPlayer.Source = new Uri(MySongs[_currentIndex].link);
                MySong.MyList.SelectedIndex = _currentIndex;
                this.ControlLabel.Text = MySongs[_currentIndex].name;
            }
            else if (listPlaying == 1 && NewSongs.Count>0)
            {
                MyMediaPlayer.Source = new Uri(NewSongs[_currentIndex].link);
                ListSong.NewList.SelectedIndex = _currentIndex;
                this.ControlLabel.Text = NewSongs[_currentIndex].name;
            }
             
        }
        public void PlaySong()
        {
            if (_currentIndex != -1)
            {
                MyMediaPlayer.Play();
                _isPlay = true;
                this.StatusButton.Icon = new SymbolIcon(Symbol.Pause);
                if (listPlaying == 0 && MySongs.Count >0)
                {
                    this.ControlLabel.Text = MySongs[_currentIndex].name;
                }
                else if(listPlaying == 1 && NewSongs.Count>0)
                {
                    this.ControlLabel.Text = NewSongs[_currentIndex].name;
                }
            }
        }

        public void PauseSong()
        {
            MyMediaPlayer.Pause();
            _isPlay = false;
            this.StatusButton.Icon = new SymbolIcon(Symbol.Play);
            this.ControlLabel.Text = "Paused!";
        }
    }
}