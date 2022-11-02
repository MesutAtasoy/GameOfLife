using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GameOfLife
{
    class AdWindow : Window
    {
        private readonly DispatcherTimer adTimer;
        private int imgNmb;     // the number of the image currently shown
        private string link;    // the URL where the currently shown ad leads to
        Random rnd = new Random();
        private ImageBrush myBrush = new ImageBrush();
        private Dictionary<int, BitmapImage> store = new Dictionary<int, BitmapImage>
        {

            {1, new BitmapImage(new Uri("ad1.jpg", UriKind.Relative))},
            {2, new BitmapImage(new Uri("ad2.jpg", UriKind.Relative))},
            {3, new BitmapImage(new Uri("ad3.jpg", UriKind.Relative))},
        };


        public AdWindow(Window owner)
        {
            Owner = owner;
            Width = 350;
            Height = 100;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.ToolWindow;
            Title = "Support us by clicking the ads";
            Cursor = Cursors.Hand;
            ShowActivated = false;
            MouseDown += OnClick;

            imgNmb = rnd.Next(1, 3);
            ChangeAds(this, new EventArgs());

            // Run the timer that changes the ad's image 
            adTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            adTimer.Tick += ChangeAds;
            adTimer.Start();
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(link);
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            Unsubscribe();
            base.OnClosed(e);
        }

        public void Unsubscribe()
        {
            adTimer.Tick -= ChangeAds;
            adTimer.Stop();
        }

        private void ChangeAds(object sender, EventArgs eventArgs)
        {
            var imageSource = store[imgNmb];
            myBrush.ImageSource = imageSource;
            Background = myBrush;
            link = "http://example.com";

            switch (imgNmb)
            {
                case 1:
                  
                    imgNmb++;
                    break;
                case 2:
                    imgNmb++;
                    break;
                case 3:
                    imgNmb = 1;
                    break;
            }
        }
    }
}