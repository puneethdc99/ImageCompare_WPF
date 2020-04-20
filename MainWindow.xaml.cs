using ImageMagick;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageCompare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileName1 = "";
        string fileName2 = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            fileName1 = GetFilename();
            this.image1.Source = new BitmapImage(new Uri(fileName1, UriKind.RelativeOrAbsolute)); 
        }

        private static string GetFilename()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return string.Empty;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            fileName2 = GetFilename();
            this.image2.Source = new BitmapImage(new Uri(fileName2, UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                //var diffImagePath = @"C:\software\ImageCompare\3.png";

                using (MagickImage image1 = new MagickImage(fileName1))
                using (MagickImage image2 = new MagickImage(fileName2))
                using (MagickImage diffImage = new MagickImage())
                {
                    image1.Compare(image2, ErrorMetric.Absolute, diffImage);
                    diffImage.Write(Environment.CurrentDirectory + "diff.png");
                    this.image3.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + "diff.png", UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception) { }
        }
    }
}
