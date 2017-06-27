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
using System.Xml;
using Microsoft.Win32;

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

                    var personnages = new System.Xml.Linq.XElement("Personnages");
                    foreach (PersonnageAbstrait personnage in zone.PersonnageList)
                    {
                        var personnageElement = new System.Xml.Linq.XElement("Personnage");
                        personnageElement.Add(new System.Xml.Linq.XElement("Nom", personnage.Nom));
                        personnageElement.Add(new System.Xml.Linq.XElement("Etat", personnage.Etat));

                        personnages.Add(personnageElement);
                    }
                    zoneElement.Add(personnages);

                    // Ajout de la zone à la liste de zones
                    zones.Add(zoneElement);
                }

                // Ajout de la liste de zones au jardin
                jardinDocument.Add(zones);

                document.Root.Add(jardinDocument);
                //document.Save("C:\\save.xml");

                // Save dialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = " XML Files (.xml)|*.xml";

                Nullable<bool> result = saveFileDialog.ShowDialog();

                if (result == true)
                {
                    document.Save(saveFileDialog.FileName);
                }

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
                Console.WriteLine(filename);

                XmlDocument document = new XmlDocument();
                document.Load(filename);

                // Grille
                XmlNode grilleNode = document.DocumentElement.SelectSingleNode("/Document/Grille");
                int rows = int.Parse(grilleNode.SelectSingleNode("rows").InnerText);
                int cols = int.Parse(grilleNode.SelectSingleNode("cols").InnerText);
                Console.WriteLine(rows);
                Console.WriteLine(cols);

                // Début du jardin
                XmlNode jardinNode = document.DocumentElement.SelectSingleNode("/Document/jardin");
                XmlNode fabriqueAbstraiteNode = jardinNode.SelectSingleNode("fabriqueAbstraite/Titre");

                // Fabrique abstraite
                FabriqueAbstraite fabriqueAbstraiteLoad = new FabriqueFourmiliere();

                if (fabriqueAbstraiteNode.InnerText != null)
                {
                    fabriqueAbstraiteLoad.Titre = fabriqueAbstraiteNode.InnerText;
                }

                EnvironnementAbstrait jardinLoad = new Jardin(fabriqueAbstraiteLoad);

                // Coordonnées de la fourmilière
                var fourmiliereNode = jardinNode.SelectSingleNode("Fourmiliere");
                int fourmiliereX = int.Parse(fourmiliereNode.SelectSingleNode("X").InnerText);
                int fourmiliereY = int.Parse(fourmiliereNode.SelectSingleNode("Y").InnerText);
                Console.WriteLine(fourmiliereX);
                Console.WriteLine(fourmiliereY);

                // Tableau des zones
                ZoneAbstraite[,] zonesArray = new ZoneAbstraite[cols+1, rows+1];

                var zonesNode = jardinNode.SelectSingleNode("Zones");
                foreach (XmlNode zoneNode in zonesNode.ChildNodes)
                {
                    String zoneNom = zoneNode.SelectSingleNode("Nom").InnerText;
                    Console.WriteLine(zoneNom);
                    int zoneX = int.Parse(zoneNode.SelectSingleNode("X").InnerText);
                    int zoneY = int.Parse(zoneNode.SelectSingleNode("Y").InnerText);
                    ZoneAbstraite zone = new BoutDeTerrain("", zoneX, zoneY);

                    // Objets
                    var objetsNode = zoneNode.SelectSingleNode("Objets");
                    if (objetsNode.ChildNodes != null)
                    {
                        foreach (XmlNode objetNode in objetsNode)
                        {
                            String objetNom = objetNode.SelectSingleNode("Nom").InnerText;
                            int objetHpMax = int.Parse(objetNode.SelectSingleNode("HPMax").InnerText);
                            int objetHp = int.Parse(objetNode.SelectSingleNode("HP").InnerText);
                            String objetType = objetNode.SelectSingleNode("Type").InnerText;

                            ObjetAbstrait objet;

                            if (objetType.Equals("Nourriture"))
                            {
                                objet = new Nourriture();
                            }
                            else if (objetType.Equals("Oeuf"))
                            {
                                objet = new Oeuf();
                            }
                            else
                            {
                                objet = new Pheromone();
                            }
                                
                            objet.Nom = objetNom;
                            objet.HPMax = objetHpMax;
                            objet.HP = objetHp;

                            // Ajout de l'objet
                            zone.AjouterObjet(objet);
                            jardinLoad.AjouteObjet(objet);
                        }
                    }

                    // Acces
                    // Vaut mieux les recréer directement apres avoir fait toutes les zones? 
                    // Et du coup ne pas les save

                    // Personnages
                    var personnagesNode = zoneNode.SelectSingleNode("Personnages");
                    if (personnagesNode.ChildNodes != null)
                    {
                        foreach (XmlNode personnageNode in personnagesNode)
                        {
                            String personnageNom = personnageNode.SelectSingleNode("Nom").InnerText;
                            String personnageEtatType = personnageNode.SelectSingleNode("Etat").InnerText;

                            EtatPersonnageAbstrait personnageEtat;

                            if (personnageEtatType.Equals("AntBox.Etat.EtatFourmiMorte"))
                            {
                                personnageEtat = new EtatFourmiMorte();
                            } else if (personnageEtatType.Equals("AntBox.Etat.EtatFourmiAbrite"))
                            {
                                personnageEtat = new EtatFourmiAbrite();
                            } else if (personnageEtatType.Equals("AntBox.Etat.EtatFourmiFoundFood"))
                            {
                                personnageEtat = new EtatFourmiFoundFood();
                            } else if (personnageEtatType.Equals("AntBox.Etat.EtatFourmiFuirPluie"))
                            {
                                personnageEtat = new EtatFourmiFuirPluie();
                            } else
                            {
                                personnageEtat = new EtatFourmiAleatoire();
                            }

                            PersonnageAbstrait personnage = new Fourmi(personnageNom, AntWeather.SharedAntWeather, null, personnageEtat);
                            personnage.ZoneActuelle = zone;

                            zone.AjouterPersonnage(personnage);
                            jardinLoad.AjoutePersonnage(personnage);
                        }
                    }

                    // Ajout au tableau
                    zonesArray[zoneX, zoneY] = zone;

                    // Est-ce la fourmiliere?
                    if (zoneX == fourmiliereX && zoneY == fourmiliereY)
                    {
                        jardinLoad.Fourmiliere = zone;
                    }

                    jardinLoad.AjouteZoneAbstraites(zone);

                    int nombreZone = jardinLoad.ZoneList.Count;
                    ZoneAbstraite zoneDebut;
                    ZoneAbstraite zoneFin;
                    AccesAbstrait acces;

                    if ((nombreZone >=2) && (zoneX > 1))
                    {
                        zoneDebut = jardinLoad.ZoneList[nombreZone - 2];
                        zoneFin = jardinLoad.ZoneList[nombreZone - 1];
                        acces = jardinLoad.fabriqueAbstraite.CreerAcces(zoneDebut, zoneFin);

                        jardinLoad.AjouteChemins(acces);
                    }

                    if ((nombreZone - cols) >= 1)
                    {
                        zoneDebut = jardinLoad.ZoneList[nombreZone - cols - 1];
                        zoneFin = jardinLoad.ZoneList[nombreZone - 1];
                        acces = jardinLoad.fabriqueAbstraite.CreerAcces(zoneDebut, zoneFin);
                        jardinLoad.AjouteChemins(acces);
                    }
                }

                // Donner la fourmiliere comme maison de chaque personnage

                foreach (PersonnageAbstrait personnage in jardinLoad.PersonnageList)
                {
                    personnage.maison = jardinLoad.Fourmiliere;
                }

                // Creation de la grille
                if (generation)
                {
                    Grille.RowDefinitions.Clear();
                    Grille.ColumnDefinitions.Clear();
                    Grille.Children.Clear();
                }

                for (int i = 0; i < cols; i++)
                {
                    Grille.ColumnDefinitions.Add(new ColumnDefinition() { });
                }

                for (int i = 0; i < rows; i++)
                {
                    Grille.RowDefinitions.Add(new RowDefinition() { });
                }

                jardin = jardinLoad;

                Grille.Background = new SolidColorBrush(Colors.LightGreen);
                genereAffichage();
                generation = true;

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
