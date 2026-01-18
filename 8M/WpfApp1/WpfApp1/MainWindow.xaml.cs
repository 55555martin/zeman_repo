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
using Color = System.Windows.Media.Color;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ChangeRed(byte red)
        {
            if (txtGreen is null || txtBlue is null)
            { return; }
            byte g = Convert.ToByte(txtGreen.Text);
            byte b = Convert.ToByte(txtBlue.Text);
            colorRect.Fill = new SolidColorBrush(Color.FromRgb(red, g, b));
            sliderRed.Value = red;
            txtRed.Text = Convert.ToString(red);
            RgbToHex();
        }

        public void ChangeGreen(byte green)
        {
            if (txtRed is null || txtBlue is null)
            { return; }
            byte r = Convert.ToByte(txtRed.Text);
            byte b = Convert.ToByte(txtBlue.Text);
            colorRect.Fill = new SolidColorBrush(Color.FromRgb(r, green, b));
            sliderGreen.Value = green;
            txtGreen.Text = Convert.ToString(green);
            RgbToHex();
        }

        public void ChangeBlue(byte blue)
        {
            if (txtRed is null || txtGreen is null)
            { return; }
            byte r = Convert.ToByte(txtRed.Text);
            byte g = Convert.ToByte(txtGreen.Text);
            colorRect.Fill = new SolidColorBrush(Color.FromRgb(r, g, blue));
            sliderBlue.Value = blue;
            txtBlue.Text = Convert.ToString(blue);
            RgbToHex();
        }

        private void txtRed_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = txtRed.Text;
            int result;
            if (!int.TryParse(text, out result))
            {
                MessageBox.Show("Text není číslo!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (result < 0 || result > 255)
            {
                MessageBox.Show("Číslo musí být v rozmezí 0-255!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            byte red = Convert.ToByte(result);
            ChangeRed(red);
        }

        private void txtGreen_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = txtGreen.Text;
            int result;
            if (!int.TryParse(text, out result))
            {
                MessageBox.Show("Text není číslo!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (result < 0 || result > 255)
            {
                MessageBox.Show("Číslo musí být v rozmezí 0-255!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            byte green = Convert.ToByte(result);
            ChangeGreen(green);
        }

        private void txtBlue_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = txtBlue.Text;
            int result;
            if (!int.TryParse(text, out result))
            {
                MessageBox.Show("Text není číslo!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (result < 0 || result > 255)
            {
                MessageBox.Show("Číslo musí být v rozmezí 0-255!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            byte blue = Convert.ToByte(result);
            ChangeBlue(blue);
        }

        private void sliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte red = Convert.ToByte(sliderRed.Value);
            ChangeRed(red);
        }

        private void sliderGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte green = Convert.ToByte(sliderGreen.Value);
            ChangeGreen(green);
        }

        private void sliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte blue = Convert.ToByte(sliderBlue.Value);
            ChangeBlue(blue);
        }

        public void RgbToHex()
        {
            int r = Convert.ToInt32(sliderRed.Value);
            int g = Convert.ToInt32(sliderGreen.Value);
            int b = Convert.ToInt32(sliderBlue.Value);
            HexCode.Content = $"#{r:X2}{g:X2}{b:X2}";
        }
    }
}