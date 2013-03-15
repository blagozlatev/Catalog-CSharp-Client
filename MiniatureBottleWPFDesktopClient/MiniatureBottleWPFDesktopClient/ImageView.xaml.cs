using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MiniatureBottleWPFDesktopClient.Nomenclatures;

namespace MiniatureBottleWPFDesktopClient
{
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : Window
    {        
        public ImageView()
        {            
            InitializeComponent();
            rotateImage.Angle = Constants.Image.NullRotation;
        }        

        private void btnZoomIn_OnClick(object sender, RoutedEventArgs e)
        {            
            scaleImage.ScaleX += Constants.Image.ScaleImageStep;
            scaleImage.ScaleY += Constants.Image.ScaleImageStep;
        }

        private void btnZoomOut_OnClick(object sender, RoutedEventArgs e)
        {            
            if (scaleImage.ScaleX >= 0.3)
            {
                scaleImage.ScaleX -= Constants.Image.ScaleImageStep;
                scaleImage.ScaleY -= Constants.Image.ScaleImageStep;
            }
        }

        private void btnFillWindow_OnClick(object sender, RoutedEventArgs e)
        {
            scaleImage.ScaleX = Constants.General.One;
            scaleImage.ScaleY = Constants.General.One;
            imgBottle.Width = scrlVwrForImage.ActualWidth - 10;
            imgBottle.Height = scrlVwrForImage.ActualHeight - 10;                    
        }

        private void btnOriginalSize_OnClick(object sender, RoutedEventArgs e)
        {            
            BitmapImage bmp = imgBottle.Source as BitmapImage;
            imgBottle.Height = bmp.Height;
            imgBottle.Width = bmp.Width;
            scaleImage.ScaleX = Constants.General.One;
            scaleImage.ScaleY = Constants.General.One;            
        }

        private void txtCustomZoom_OnEnter(object sender, KeyEventArgs e)
        {            
            if (e.Key == Key.Enter)
            {
                double customZoomPercentage = Constants.General.Zero;
                if (double.TryParse(txtCustomZoom.Text, out customZoomPercentage))
                {
                    customZoomPercentage = double.Parse(txtCustomZoom.Text);
                }
                if (customZoomPercentage >= Constants.Image.MinimumZoomPercentage
                    && customZoomPercentage <= Constants.Image.MaximumZoomPercentage)
                {
                    double zoomRatio = Constants.General.One * 
                        (customZoomPercentage / Constants.General.Hundred);
                    scaleImage.ScaleX = zoomRatio;
                    scaleImage.ScaleY = zoomRatio;                    
                }
                else
                {
                    MessageBox.Show("The value for custom zoom must be between 30 and 400!", "Error!");
                }
            }
        }

        private void btnRotateRight_OnClick(object sender, RoutedEventArgs e)
        {                        
            rotateImage.Angle += Constants.Image.AngleRotation;
            if (rotateImage.Angle == Constants.Image.FullRotation)
            {
                rotateImage.Angle = Constants.Image.NullRotation;
            }
            scaleImage.CenterX = imgBottle.ActualWidth / Constants.Image.DivisorForCenterOfImage;
            scaleImage.CenterY = imgBottle.ActualHeight / Constants.Image.DivisorForCenterOfImage;            
        }

        private void btnRotateLeft_OnClick(object sender, RoutedEventArgs e)
        {            
            rotateImage.Angle -= Constants.Image.AngleRotation;
            if (rotateImage.Angle == -Constants.Image.FullRotation)
            {
                rotateImage.Angle = Constants.Image.NullRotation;
            }
            scaleImage.CenterX = imgBottle.ActualWidth / Constants.Image.DivisorForCenterOfImage;
            scaleImage.CenterY = imgBottle.ActualHeight / Constants.Image.DivisorForCenterOfImage;            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            imgBottle.Width = scrlVwrForImage.ActualWidth - 10;
            imgBottle.Height = scrlVwrForImage.ActualHeight - 10;                  
            scaleImage.CenterX = imgBottle.ActualWidth / Constants.Image.DivisorForCenterOfImage;
            scaleImage.CenterY = imgBottle.ActualHeight / Constants.Image.DivisorForCenterOfImage;
            scaleImage.ScaleX = Constants.General.One;
            scaleImage.ScaleY = Constants.General.One;
        }
    }
}
