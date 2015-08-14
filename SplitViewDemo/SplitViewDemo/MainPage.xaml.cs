using System;
using System.Collections.Generic;
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

//空白頁項目範本收錄在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Hamburgerbtn_Click(object sender, RoutedEventArgs e)
        {
            this.MainMenu.IsPaneOpen = !this.MainMenu.IsPaneOpen;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x = (e.AddedItems[0] as ListViewItem).Name;

            switch (x)
            {
                case "Personal":
                    this.ContentFrame.Navigate(typeof(BlankPage1));
                    break;
                case "Camera":
                    this.ContentFrame.Navigate(typeof(BlankPage2));
                    break;
                case "Settings":
                    this.ContentFrame.Navigate(typeof(BlankPage3));
                    break;
            }
        }
    }
}
