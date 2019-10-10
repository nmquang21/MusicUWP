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
using Music.Service;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Music.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyInformation : Page
    {
        private MemberService memberService;

        public MyInformation()
        {

            this.InitializeComponent();
            memberService = new MemberServiceImp();
            var member = memberService.GetInformation();
            if (string.IsNullOrEmpty(member.id))
            {
                this.info.Visibility = Visibility.Collapsed;
            }
            else{
                Debug.WriteLine(member);

                this.PersonPicture.ProfilePicture = new BitmapImage(new Uri(member.avatar));
                this.Name.Text = "Name: " + member.lastName + " " + member.firstName;
                if (member.gender == 1)
                {
                    this.Gender.Text = "Gender: Male";
                }
                else
                {
                    this.Gender.Text = "Gender: Female";
                }

                this.email.Text = "Email: " + member.email;
                this.Address.Text = "Address: " + member.address;
                this.Birthday.Text = "Birthday: " + member.birthday;
                this.Phone.Text = "Phone: " + member.phone;
                this.introduction.Text = "Introdution: " + member.introduction;
                this.loginRequied.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonLogout_OnClick(object sender, RoutedEventArgs e)
        {
            memberService.logout();
            Naview.MainFrame.Navigate(typeof(MyInformation));
        }
        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            Naview.MainFrame.Navigate(typeof(Login));
        }
        private void ButtonRegister_OnClick(object sender, RoutedEventArgs e)
        {
            Naview.MainFrame.Navigate(typeof(Register));
        }
    }
}
