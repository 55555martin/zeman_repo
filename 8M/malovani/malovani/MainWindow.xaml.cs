using Malovani;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace malovani
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isDrawing = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_file(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = ""; // Default file name
            dialog.DefaultExt = ".png"; // Default file extension
            dialog.Filter = "Images|*.jpg;*.png;*.jpeg"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
            }
        }

        private void Save_file(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Image"; // Default file name
            dialog.DefaultExt = ".png"; // Default file extension
            dialog.Filter = "Images|*.jpg;*.png;*.jpeg"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dialog.FileName;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RGBColorPicker colorPicker = new RGBColorPicker();
            colorPicker.ShowDialog();
            if (colorPicker.Success == true)
            {
                ColorButton.Background = new SolidColorBrush(colorPicker.Color);
            }
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                // Získání aktuální pozice myši
                System.Windows.Point currentPoint = e.GetPosition(DrawingCanvas);

                double _circleRadius = BrushRadius.Value;

                // Vytvoření kruhu (Ellipse)
                Ellipse circle = new Ellipse
                {
                    Width = _circleRadius,
                    Height = _circleRadius,
                    Fill = ColorButton.Background // Barva kruhu
                };

                // Nastavení pozice kruhu
                Canvas.SetLeft(circle, currentPoint.X - _circleRadius/2);
                Canvas.SetTop(circle, currentPoint.Y - _circleRadius/2);

                // Přidání kruhu na Canvas
                DrawingCanvas.Children.Add(circle);
            }
        }

        private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = true;
        }

        private void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = false;
        }
    }
}