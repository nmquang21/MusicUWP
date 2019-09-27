﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
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
    public sealed partial class FileHandle : Page
    {
        public FileHandle()
        {
            this.InitializeComponent();
        }

        private StorageFile photo;
        private string currentUploadUrl = "https://2-dot-backup-server-003.appspot.com/_ah/upload/AMmfu6Z8KT9qIupN1ssh53xsjJBJ9wh8Odf31LZox3DmrPsQqv8-JbE8Gjci9Fx80cw6tO7v_VvpFHrQiY9nOFEk_rAj2cGRPn4hn8IhlpoizJkh1SdokeBpafnxqy2xr2YeNs2-tnHjwFWLpMTAtFc5P80jaRFNulPW9t1qE-TPoELUqGdwkNgVgoxkzv7Ev-rb2td9ZLGjOqSpLhMi7OpzpapjD1A-VI34aVbVQ_wfPSYp9HdkkIk/ALBNUaYAAAAAXY4RuwhE6PsmbH0QFGY0gWZwB-JVNTkt/";
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture 
                return;
            }
            ////Lưu ảnh
            //StorageFolder destinationFolder =
            //    await ApplicationData.Current.LocalFolder.CreateFolderAsync("ProfilePhotoFolder",
            //        CreationCollisionOption.OpenIfExists);
            //await photo.CopyAsync(destinationFolder, "ProfilePhoto.jpg", NameCollisionOption.ReplaceExisting);
            HttpUploadFile(currentUploadUrl, "myFile", "image/png");
        }


        public async void HttpUploadFile(string url, string paramName, string contentType)

            // write file.
            Stream fileStream = await this.photo.OpenStreamForReadAsync();
                //Debug.WriteLine(string.Format("File uploaded, server response is: @{0}@", reader2.ReadToEnd()));
                //string imgUrl = reader2.ReadToEnd();
                //Uri u = new Uri(reader2.ReadToEnd(), UriKind.Absolute);
                //Debug.WriteLine(u.AbsoluteUri);
                //ImageUrl.Text = u.AbsoluteUri;
                //MyAvatar.Source = new BitmapImage(u);
                //Debug.WriteLine(reader2.ReadToEnd());
                string imageUrl = reader2.ReadToEnd();
    }
}