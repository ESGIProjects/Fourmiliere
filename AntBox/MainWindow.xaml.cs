using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AntBox
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Button run = new Button();
        Button save = new Button();
        Button load = new Button();

        public MainWindow()
        {
            InitializeComponent();
            Board.Background = new SolidColorBrush(Colors.AliceBlue);
        }

        private void runClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Création d'une super Fourmi");
            Fourmi fourmi = new Fourmi("superFourmi", 9999);
            MessageBox.Show("Création d'une super fourmi", "SUper Fourmiiiiii");
        }

        private void saveClick(object sender, RoutedEventArgs e)
        {

        }

        private void loadClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".xml";
            ofd.Filter = " XML Files (.xml)|*.xml";

            
            Nullable<bool> result = ofd.ShowDialog();

           
            if (result == true)
            {
                string filename = ofd.FileName;
                Console.Write(filename);
            }
        }

        private void genererClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
