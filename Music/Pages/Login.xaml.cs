using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Music.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private MemberService memberService;
        public Login()
        {
            this.InitializeComponent();
            memberService = new MemberServiceImp();
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            
            var memberLogin = new MemberLogin()
            {
                email = this.Email.Text,
                password = this.Password.Password,
            };
            var errors = new Dictionary<string, string>();
            errors = memberLogin.ValidateData();
            if (errors.Count == 0)
            {
                if (memberService.Login(memberLogin) != null)
                {
                    Naview.MainFrame.Navigate(typeof(MyInformation));
                }
                else
                {
                    this.login_fail.Text = "Wrong login information!!";
                    this.login_fail.Visibility = Visibility.Visible;
                    this.validate_email.Visibility = Visibility.Collapsed;
                    this.validate_password.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                ValidateLogin(errors);
            }

        }

        private void ValidateLogin(Dictionary<string, string> errors)
        {
            if (errors.ContainsKey("email"))
            {
                this.validate_email.Text = errors["email"];
                this.validate_email.Visibility = Visibility.Visible;
            }
            else
            {
                this.validate_email.Visibility = Visibility.Collapsed;
            }
            if (errors.ContainsKey("password"))
            {
                this.validate_password.Text = errors["password"];
                this.validate_password.Visibility = Visibility.Visible;
            }
            else
            {
                this.validate_password.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonReset_OnClick(object sender, RoutedEventArgs e)
        {
            this.Email.Text = string.Empty;
            this.Password.Password = string.Empty;
        }
    }
}
