﻿using System;
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
using Music.Service;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Music.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Upload : Page
    {
        private SongService songService;
        private MemberService memberService;
        public Upload()
        {
            this.InitializeComponent();
            songService = new SongServiceImp();
            memberService = new MemberServiceImp();
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
                
                if (!memberService.GetTokenFromLocalStorage().Equals(""))
                {
                    songService.UploadSong(song);
                    Naview.MainFrame.Navigate(typeof(MySong));
                }
                else
                {
                    var respSong = songService.UploadFreeSong(song);
                    Reset();
                    this.name_validate.Visibility = Visibility.Collapsed;
                    this.link_validate.Visibility = Visibility.Collapsed;
                    this.thumbnail_validate.Visibility = Visibility.Collapsed;
                    this.singer_validate.Visibility = Visibility.Collapsed;
                    this.upload_state.Text = "Upload Song "+ respSong .name+ " Success!";
                }
            }
            else
            {
                ValidateSongUpload(errors);
            }
        }
        private void ButtonReset_OnClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            this.name.Text = string.Empty;
            this.description.Text = string.Empty;
            this.singer.Text = string.Empty;
            this.author.Text = string.Empty;
            this.thumbnail.Text = string.Empty;
            this.link.Text = string.Empty;
        }

        private void ValidateSongUpload(Dictionary<string, string> errors)
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

            if (errors.ContainsKey("singer"))
            {
                this.singer_validate.Text = errors["singer"];
                this.singer_validate.Visibility = Visibility;
            }
            else
            {
                this.singer_validate.Visibility = Visibility.Collapsed;
            }
        }
    }
}

