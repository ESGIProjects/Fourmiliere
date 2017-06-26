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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean generation = false;
        Boolean inProgress = false;
        Uri uriAnt = new Uri("./Resources/ant_next.png", UriKind.Relative);
        Uri uriCupcake = new Uri("./Resources/food.png", UriKind.Relative);
        Uri uriAntFood = new Uri("./Resources/ant_with_food.png", UriKind.Relative);
        Uri uriAnthill = new Uri("./Resources/anthill.png", UriKind.Relative);
        Uri uriSugar = new Uri("./Resources/sugar.png", UriKind.Relative);
        Uri deathAnt = new Uri("./Resources/death_ant.png", UriKind.Relative);

        protected int TimerPuie  { get; set; } = -2;

        public static AntWeather antWeatherForecast = AntWeather.SharedAntWeather;

        public EnvironnementAbstrait jardin { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }
        async Task PutTaskDelay()
        {
            Console.WriteLine("\n\n\nBOUCLE SIMULATION\n\n\n");
            TimerPuie--;

            if (TimerPuie == 0)
            {
                RainButton.Content = "Il pleut";
                Grille.Background = new SolidColorBrush(Colors.Blue);
                AntWeather.SharedAntWeather.Etat = AntWeather.RainIsHere;
            }
            else if (TimerPuie == -1)
            {
                RainButton.Content = "Faire pleuvoir";
                Grille.Background = new SolidColorBrush(Colors.LightGreen);
                AntWeather.SharedAntWeather.Etat = AntWeather.RainIsFinished;
            }
            else if (TimerPuie > 0)
            {
                RainButton.Content = "Il va pleuvoir dans " + TimerPuie + " tour(s)";
                Console.WriteLine("\n\n\nIl va pleuvoir dans " + TimerPuie + " tour(s) \n\n\n");
            }


            Console.WriteLine(jardin.Simuler());
            genereAffichage();
            await Task.Delay(1000);
        }

        private async void runClick(object sender, RoutedEventArgs e)
        {
            if (generation)
            {
                if (inProgress == false) { 
                    RunButton.Background = new SolidColorBrush(Colors.Green);
                    RunButton.Content = "In progress...";
                    inProgress = true;

                    while (inProgress)
                    {
                        await PutTaskDelay();
                    } 
                } else
                {
                    RunButton.Background = new SolidColorBrush(Colors.Orange);
                    RunButton.Content = "Continue";
                    inProgress = false;
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

        private void weatherClick(object sender, RoutedEventArgs e)
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

                if (TimerPuie < -1) {
                    TimerPuie = 5;
                    AntWeather.SharedAntWeather.Etat = AntWeather.RainIsComing;
                }
                else
                {
                    string message = "VEuillez patienter...";
                    string titre = "Erreur";
                    MessageBoxButton mbb = MessageBoxButton.OK;
                    MessageBox.Show(message, titre, mbb);
                }
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

                // Init du fichier xml
                var document = new System.Xml.Linq.XDocument(new System.Xml.Linq.XElement("Document"));

                // Taille de la grille
                var grilleDocument = new System.Xml.Linq.XElement("Grille");
                grilleDocument.Add(new System.Xml.Linq.XElement("cols", Grille.ColumnDefinitions.Count));
                grilleDocument.Add(new System.Xml.Linq.XElement("rows", Grille.RowDefinitions.Count));
                document.Root.Add(grilleDocument);


                // Création du jardin
                var jardinDocument = new System.Xml.Linq.XElement("jardin");

                // Fabrique abstraite
                var fabriqueAbstraiteDocument = new System.Xml.Linq.XElement("fabriqueAbstraite");
                fabriqueAbstraiteDocument.Add(new System.Xml.Linq.XElement("Titre", jardin.fabriqueAbstraite.Titre));
                jardinDocument.Add(fabriqueAbstraiteDocument);

                // Emplacement de la fourmilière
                var fourmiliereZone = new System.Xml.Linq.XElement("Fourmiliere");
                fourmiliereZone.Add(new System.Xml.Linq.XElement("X", jardin.Fourmiliere.positionX));
                fourmiliereZone.Add(new System.Xml.Linq.XElement("Y", jardin.Fourmiliere.positionY));
                jardinDocument.Add(fourmiliereZone);

                // Liste des zones
                var zones = new System.Xml.Linq.XElement("Zones");
                foreach (ZoneAbstraite zone in jardin.ZoneList)
                {
                    // Création de la zone
                    var zoneElement = new System.Xml.Linq.XElement("Zone");
                    zoneElement.Add(new System.Xml.Linq.XElement("Nom", zone.Nom));
                    zoneElement.Add(new System.Xml.Linq.XElement("X", zone.positionX));
                    zoneElement.Add(new System.Xml.Linq.XElement("Y", zone.positionY));

                    // Liste des objets de cette zone
                    var objets = new System.Xml.Linq.XElement("Objets");
                    foreach (ObjetAbstrait objet in zone.ObjetList)
                    {
                       var objetElement = new System.Xml.Linq.XElement("Objet");
                        objetElement.Add(new System.Xml.Linq.XElement("Nom", objet.Nom));
                        objetElement.Add(new System.Xml.Linq.XElement("HPMax", objet.HPMax));
                        objetElement.Add(new System.Xml.Linq.XElement("HP", objet.HP));

                        if (objet is Nourriture)
                        {
                            objetElement.Add(new System.Xml.Linq.XElement("Type", "Nourriture"));
                        }
                        else if (objet is Oeuf)
                        {
                            objetElement.Add(new System.Xml.Linq.XElement("Type", "Oeuf"));
                        }
                        else if (objet is Pheromone)
                        {
                            objetElement.Add(new System.Xml.Linq.XElement("Type", "Pheromone"));
                        }

                        objets.Add(objetElement);
                    }
                    zoneElement.Add(objets);

                    // Liste des accès de cette zone
                    var accesList = new System.Xml.Linq.XElement("AccesList");
                    foreach (AccesAbstrait acces in zone.AccesList)
                    {
                        var accesElement = new System.Xml.Linq.XElement("Acces");

                        if (acces.ZoneDebut != null)
                        {
                            var zoneDebut = new System.Xml.Linq.XElement("ZoneDebut");
                            zoneDebut.Add(new System.Xml.Linq.XElement("X", acces.ZoneDebut.positionX));
                            zoneDebut.Add(new System.Xml.Linq.XElement("Y", acces.ZoneDebut.positionY));
                            accesElement.Add(zoneDebut);
                        }

                        if (acces.ZoneFin != null)
                        {
                            var zoneFin = new System.Xml.Linq.XElement("ZoneFin");
                            zoneFin.Add(new System.Xml.Linq.XElement("X", acces.ZoneFin.positionX));
                            zoneFin.Add(new System.Xml.Linq.XElement("Y", acces.ZoneFin.positionY));
                            accesElement.Add(zoneFin);
                        }
                        accesList.Add(accesElement);
                    }
                    zoneElement.Add(accesList);

                    var personnages = new System.Xml.Linq.XElement("Personnages");
                    foreach (PersonnageAbstrait personnage in zone.PersonnageList)
                    {
                        var personnageElement = new System.Xml.Linq.XElement("Personnage");
                        personnageElement.Add(new System.Xml.Linq.XElement("Nom", personnage.Nom));
                        personnageElement.Add(new System.Xml.Linq.XElement("Observe", personnage.Observe));
                        personnageElement.Add(new System.Xml.Linq.XElement("Etat", personnage.Etat));

                        if (personnage.maison != null)
                        {
                            var maison = new System.Xml.Linq.XElement("maison");
                            maison.Add(new System.Xml.Linq.XElement("X", personnage.maison.positionX));
                            maison.Add(new System.Xml.Linq.XElement("Y", personnage.maison.positionY));
                            personnageElement.Add(maison);
                        }

                        personnages.Add(personnageElement);
                    }
                    zoneElement.Add(personnages);

                    // Ajout de la zone à la liste de zones
                    zones.Add(zoneElement);
                }

                // Ajout de la liste de zones au jardin
                jardinDocument.Add(zones);

                document.Root.Add(jardinDocument);
                document.Save("save.xml");

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

            //indication que la grille est générée
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
                    else if (personnage.Etat is EtatFourmiMorte)
                    {
                        image.Source = new BitmapImage(deathAnt);
                    } else
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
