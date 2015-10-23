using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace QiMata.Indoor.PiApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Ellipse CenterEllipse;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGraph();
        }

        private void DrawGraph()
        {
            int linesX = 6;
            int linesY = 12;

            double startX = 0;
            double endX = this.Graph.ActualWidth;

            

            double startY = 0;
            double endY = this.Graph.ActualHeight;

            double gapY = (endY - startY) / linesY;
            //double gapX = (endX - startX) / linesX;
            double gapX = gapY;

            double xPos = startX;
            double yPos = startY;

            while (xPos < endX)
            {
                this.Graph.Children.Add(GetXLine(startY, endY, xPos));

                xPos += gapX;
            }

            while (yPos < endY)
            {
                this.Graph.Children.Add(GetYLine(startX,endX,yPos));

                yPos += gapY;
            }



            CenterEllipse = new Ellipse
            {
                Height = gapY,
                Width = gapY,
                Stroke = new SolidColorBrush(Windows.UI.Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Fill = new SolidColorBrush(Windows.UI.Colors.OrangeRed)
            };

            Canvas.SetLeft(CenterEllipse,endX / 2);
            Canvas.SetTop(CenterEllipse,endY / 2);
            this.Graph.Children.Add(CenterEllipse);
        }

        private Line GetYLine(double startX, double endX, double yPos)
        {
            return new Line
            {
                X1 = startX,
                X2 = endX,
                Y1 = yPos,
                Y2 = yPos,
                Stroke = new SolidColorBrush(Windows.UI.Colors.DarkBlue)
            };
        }

        private Line GetXLine(double startY, double endY,double xPos)
        {
            return new Line
            {
                X1 = xPos,
                X2 = xPos,
                Y1 = startY,
                Y2 = endY,
                Stroke = new SolidColorBrush(Windows.UI.Colors.DarkBlue)
            };
        }
    }
}
