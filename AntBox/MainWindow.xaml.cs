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
using System.Windows.Threading;
using System.Threading.Tasks;
using AntBox.Etat;

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
        Uri uriAnt = new Uri("./Resources/ant_next.png", UriKind.Relative);
        Uri uriCupcake = new Uri("./Resources/food.png", UriKind.Relative);
        Uri uriAntFood = new Uri("./Resources/ant_with_food.png", UriKind.Relative);
        Uri uriAnthill = new Uri("./Resources/anthill.png", UriKind.Relative);
        Uri uriSugar = new Uri("./Resources/sugar.png", UriKind.Relative);

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
        async Task PutTaskDelay()
        {
            Console.WriteLine("\n\n\nBOUCLE SIMULATION\n\n\n");
            Console.WriteLine(jardin.Simuler());
            genereAffichage();
            await Task.Delay(100);
        }

        private async void runClick(object sender, RoutedEventArgs e)
        {

            
            if (generation)
            {
                while (true)
                {
                    await PutTaskDelay();
                }
            }
            else
            {
                string message = "Veuillez générer une grille avant !";
                string titre = "Erreur";
                MessageBoxButton mbb = MessageBoxButton.OK;
                MessageBoxImage mbi = MessageBoxImage.Error;
                MessageBox.Show(message, titre, mbb, mbi);

            }

        }

        private void saveClick(object sender, RoutedEventArgs e)
        {
            if (!generation)
            {
                string message = "Veuillez commencer une partie avant !!";
                string titre = "Erreur";
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
                    if (mbr == MessageBoxResult.Yes)
                    {
                        File.Delete("save.xml");
                    }
                    else
                    {
                        return;
                    }
                }

                var doc = new System.Xml.Linq.XDocument(
                    new System.Xml.Linq.XElement("Grid",
                         new System.Xml.Linq.XElement("rows", Grille.RowDefinitions.Count),
                         new System.Xml.Linq.XElement("cols", Grille.ColumnDefinitions.Count)
                    )
                );

                System.Xml.Linq.XElement antlist = new System.Xml.Linq.XElement("AntList");

                foreach (ZoneAbstraite za in jardin.ZoneList)
                {
                    foreach (PersonnageAbstrait pa in za.PersonnageList)
                    {
                        antlist.Add(
                          new System.Xml.Linq.XElement("ant",
                            new System.Xml.Linq.XElement("Name", pa.Nom)));
                        //new System.Xml.Linq.XElement("Position X", za.positionX),
                        //new System.Xml.Linq.XElement("Position Y", za.positionY)));
                    }
                }
                doc.Root.Add(antlist);
                doc.Save("save.xml");

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
            Grille.Background = new SolidColorBrush(Colors.LightGreen);

            //Reporting
            Console.WriteLine(jardin.Statistiques());


            antWeatherForecast.Etat = "Il fait beau !";
            generation = true;
        }


        /**
         * Méthode permettant de raffraichir les positions des fourmis
         */
        private void genereAffichage ()
        {
            Grille.Children.Clear();

            foreach (ZoneAbstraite zone in jardin.ZoneList)
            {
                //affichage de la fourmiliere
                System.Windows.Controls.Image imageFourmiliere = new System.Windows.Controls.Image();
                imageFourmiliere.Source = new BitmapImage(uriAnthill);
                Grille.Children.Add(imageFourmiliere);
                Grid.SetColumn(imageFourmiliere, jardin.Fourmiliere.positionX - 1);
                Grid.SetRow(imageFourmiliere, jardin.Fourmiliere.positionY - 1);

               
                //affichage des objets
                foreach (ObjetAbstrait objet in zone.ObjetList) {
                    if (objet is Nourriture) { 
                        System.Windows.Controls.Image imageNourriture = new System.Windows.Controls.Image();
                        imageNourriture.Source = new BitmapImage(uriCupcake);
                        Grille.Children.Add(imageNourriture);
                        Grid.SetColumn(imageNourriture, zone.positionX - 1);
                        Grid.SetRow(imageNourriture, zone.positionY - 1);
                    } else if (objet is Pheromone)
                    {
                        System.Windows.Controls.Image imageNourriture = new System.Windows.Controls.Image();
                        imageNourriture.Source = new BitmapImage(uriSugar);

                        Console.WriteLine("Opacity : " + (Double) objet.HP / (Double) objet.HPMax);
                        imageNourriture.Opacity = ((Double) objet.HP / (Double) objet.HPMax);

                        Grille.Children.Add(imageNourriture);
                        Grid.SetColumn(imageNourriture, zone.positionX - 1);
                        Grid.SetRow(imageNourriture, zone.positionY - 1);
                    }
                }

                //affichage des personnages
                foreach (PersonnageAbstrait personnage in zone.PersonnageList)
                {
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                    if (personnage.Etat is EtatFourmiFoundFood)
                    {
                        image.Source = new BitmapImage(uriAntFood);
                    }
                    else
                    {
                        image.Source = new BitmapImage(uriAnt);
                    }

                    Grille.Children.Add(image);
                    Grid.SetColumn(image, zone.positionX - 1);
                    Grid.SetRow(image, zone.positionY - 1);
                }

            }
        }

        public Ellipse drawRainEllipse(Grid grid, int x, int y, int colSpan = 1, int rowSpan = 1)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Blue);
            ellipse.Margin = new Thickness(3);
            ellipse.Opacity = 0.5;

            grid.Children.Add(ellipse);
            Grid.SetColumn(ellipse, x);
            Grid.SetRow(ellipse, y);
            Grid.SetColumnSpan(ellipse, colSpan);
            Grid.SetRowSpan(ellipse, rowSpan);

            return ellipse;
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
