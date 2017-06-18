using AntBox.Environnement;
using AntBox.Factory;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AntBox.Observateur;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Collections;

namespace AntBox
{ 
    /// <summary>
    /// Petite classe interne pour tester le layout tout
    /// en ayant quelque chose à binder
    /// </summary>
    public class MyRow
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
    }

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Button run = new Button();
        Button save = new Button();
        Button load = new Button();

        Boolean generation = false;
        Uri uriAnt = new Uri("./Resources/ant.png", UriKind.Relative);

        public static AntWeather antWeatherForecast = AntWeather.SharedAntWeather;

        public List<MyRow> ListBoxData { get; set; }

        public EnvironnementAbstrait jardin { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Fake data pour le layout
            ListBoxData = new List<MyRow>
            {
                new MyRow{Line1 = "Fourmi 1", Line2 = "Etat 1"},
                new MyRow{Line1 = "Fourmi 2", Line2 = "Etat 2"},
                new MyRow{Line1 = "Fourmi 3", Line2 = "Etat 3"}
            };

            DataContext = this;
        }

        private void runClick(object sender, RoutedEventArgs e)
        {


            Console.WriteLine("\n\n\nBOUCLE SIMULATION\n\n\n");
            Console.WriteLine(jardin.Simuler());
            genereAffichage();

        }

        private void saveClick(object sender, RoutedEventArgs e)
        {
            if (!generation) {
                string message = "Commences une partie avant de vouloir sauvegarder, sale n00b !";
                string titre = "Alerte aux gogoles";
                MessageBoxButton mbb = MessageBoxButton.OK;
                MessageBoxImage mbi = MessageBoxImage.Error;
                MessageBox.Show(message, titre, mbb, mbi);
            }
            else
            {
                if (File.Exists("save.xml"))
                {
                    string messageFile = "Ecraser l'ancienne sauvegarde ?";
                    string titreFile = "Confirmer suppression";
                    MessageBoxButton mbbFile = MessageBoxButton.YesNo;
                    MessageBoxImage mbiFile = MessageBoxImage.Question;
                    MessageBoxResult mbr = MessageBox.Show(messageFile, titreFile, mbbFile, mbiFile);
                    if(mbr == MessageBoxResult.Yes)
                    {
                        File.Delete("save.xml");
                    }
                    else
                    {
                        return;
                    }
                }
                new System.Xml.Linq.XDocument(
                    new System.Xml.Linq.XElement("grid",
                         new System.Xml.Linq.XElement("rows", Grille.RowDefinitions.Count),
                         new System.Xml.Linq.XElement("cols", Grille.ColumnDefinitions.Count)
                    )
                ).Save("save.xml");


               System.Media.SoundPlayer sp = new System.Media.SoundPlayer(AntBox.Properties.Resources.kaching);
                sp.Load();
                sp.Play();


                string message = "Sauvegarde effectuée !";
                string titre = "Sauvegarde";
                MessageBoxButton mbb = MessageBoxButton.OK;
                MessageBoxImage mbi = MessageBoxImage.None;
                MessageBox.Show(message, titre, mbb, mbi);
            }
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
            int nbLigne = 0;
            int nbColonne = 0;

            CustomDialog cd = new CustomDialog("Veuillez saisir le nombre de colonnes : ", "Veuillez saisir le nombre de lignes :", "");
            cd.ShowDialog();

            if(cd.X.Text == "" || cd.Y.Text == "") {
                return;
            }

            nbLigne = cd.getX;
            nbColonne = cd.getY;

            if (generation) {
                string message = "Une grille est déjà générée, voulez-vous en générer une nouvelle ?";
                string titre = "Avertissement";
                MessageBoxButton mbb = MessageBoxButton.YesNo;
                MessageBoxImage mbi = MessageBoxImage.Warning;
                MessageBoxResult mbr =  MessageBox.Show(message, titre, mbb, mbi);
                if(mbr == MessageBoxResult.Yes) {
                    Grille.RowDefinitions.Clear();
                    Grille.ColumnDefinitions.Clear();
                    Grille.Children.Clear();
                }
                else {
                    return;
                }
            }

            //Cette partie permet de générer dynamiquement la grille en wpf
            Console.WriteLine("Génération de la fourmilière");

            for (int i = 0; i < nbColonne; i++) {
                Grille.ColumnDefinitions.Add(new ColumnDefinition() {  });
            }

            for (int i = 0; i < nbLigne; i++) {
                Grille.RowDefinitions.Add(new RowDefinition() { });
            }


            //génération d'une fabrique / abstract factory
            FabriqueFourmiliere fabriqueAbstraiteFourmiliere = new FabriqueFourmiliere();
            jardin = fabriqueAbstraiteFourmiliere.CreerEnvironnement();                     //création du jardin à partir de la fabrique.


            //Génération du contenu du jardin (jardin fonctionne ici comme un client de la fabrique abstraite)
            jardin.Generation(nbColonne, nbLigne);

            //permet d'afficher la simulation
            genereAffichage();

            //Reporting
            Console.WriteLine(jardin.Statistiques());


            antWeatherForecast.Etat = "Il fait beau !";
            generation = true;
        }


        /**
         * 
         * 
         * 
         */
        private void genereAffichage ()
        {
 
           // Grid Grille = new Grid();

            for (int a =0; a < Grille.Children.Count; a++) {
                Grille.Children.RemoveAt(a);
            }

            foreach (ZoneAbstraite zone in jardin.ZoneList) {
                foreach (PersonnageAbstrait personnage in zone.PersonnageList) {
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                    image.Source = new BitmapImage(uriAnt);
                    Grille.Children.Add(image);
                    Grid.SetColumn(image, zone.positionX);
                    Grid.SetRow(image, zone.positionY);
                }
            }

        }

        public void drawEllipse(Grid grid, int x, int y, int colSpan = 1, int rowSpan = 1)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Red);
            ellipse.Margin = new Thickness(3);
            ellipse.Opacity = 0.5;

            grid.Children.Add(ellipse);
            Grid.SetColumn(ellipse, x);
            Grid.SetRow(ellipse, y);
            Grid.SetColumnSpan(ellipse, colSpan);
            Grid.SetRowSpan(ellipse, rowSpan);
        }

        public UIElement getGridChild(Grid grid, int x, int y)
        {
            foreach(UIElement element in grid.Children)
            {
                if (Grid.GetColumn(element) == x && Grid.GetRow(element) == y)
                {
                    return element;
                }
            }
            return null;
        }
    }
}
