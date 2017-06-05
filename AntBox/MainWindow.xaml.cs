using AntBox.Environnement;
using AntBox.Factory;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AntBox.Observateur;
using System.Drawing;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

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
        Boolean generation = false;

        public MainWindow()
        {
            InitializeComponent();
            Grille.Background = new SolidColorBrush(Colors.AliceBlue);
        }

        private void runClick(object sender, RoutedEventArgs e)
        {
            AntWeather antWeatherForecast = new AntWeather();

            Console.WriteLine("Création d'une super Fourmi");
            Fourmi fourmi = new Fourmi("superFourmi", antWeatherForecast);
            MessageBox.Show("Création d'une super fourmi", "Super Fourmiiiiii");

            antWeatherForecast.Etat = "Il fait beau";

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

            if (generation) {
                string message = "Une grille est déjà générée, voulez-vous en générer une nouvelle ?";
                string titre = "Avertissement";
                MessageBoxButton mbb = MessageBoxButton.YesNo;
                MessageBoxImage mbi = MessageBoxImage.Warning;
                MessageBoxResult mbr =  MessageBox.Show(message, titre, mbb, mbi);
                if(mbr == MessageBoxResult.Yes)
                {
                    Grille.RowDefinitions.Clear();
                    Grille.ColumnDefinitions.Clear();
                    Grille.Children.Clear();
                }
                else
                {
                    return;
                }
            }

            //Cette partie permet de générer dynamiquement la grille en wpf
            Console.WriteLine("Génération de la fourmilière");
            int nbColonne   = 10;
            int nbLigne     = 5;

            for (int i = 0; i < nbColonne; i++) {
                Grille.ColumnDefinitions.Add(new ColumnDefinition() {  });
            }
            for (int i = 0; i < nbLigne; i++) {
                Grille.RowDefinitions.Add(new RowDefinition() { });
            }


            //A partir d'ici on génèrera l'environnement
            Console.WriteLine("Génération de la fourmilière");

            FabriqueFourmiliere fabriqueAbstraiteFourmiliere = new FabriqueFourmiliere();
            EnvironnementAbstrait jardin = fabriqueAbstraiteFourmiliere.CreerEnvironnement();


            //TODO TODO TODO
            //TODO ajouter le client qui va utiliser la fabriqueAbstraite et faire le code suivant
            String tempoNom = "";
            int nbCharColonne = nbColonne.ToString().Length;
            int nbCharLigne = nbLigne.ToString().Length;
            ZoneAbstraite zoneDebut;
            ZoneAbstraite zoneFin;
            AccesAbstrait acces;

            int nombreZone = 0;

            //génération du jardin
            for (int y= 1; y <= nbLigne; y++) {
                for (int x= 1; x <= nbColonne; x++)
                {
                    //création du nom de la zone
                    tempoNom = "zone " +
                        new string('0', nbCharColonne - x.ToString().Length) + x.ToString() +
                        "x"+
                        new string('0', nbCharLigne - y.ToString().Length) + y.ToString();

                    //création de la zone et ajout dans le jardin
                    jardin.AjouteZoneAbstraites(fabriqueAbstraiteFourmiliere.CreerZone(tempoNom));

                    nombreZone = jardin.ZoneList.Count;

                    //si on a 2 zones ou plus, alors alors la dernière zone à une case voisine à sa gauche
                    if (nombreZone >= 2) {
                        zoneDebut   = jardin.ZoneList[nombreZone-2];
                        zoneFin     = jardin.ZoneList[nombreZone-1];
                        acces       = fabriqueAbstraiteFourmiliere.CreerAcces(zoneDebut, zoneFin);
                        
                        jardin.AjouteChemins(acces);
                    }

                    //si en soustrayant le nombre max de colonne au nombre de case on a une valeur positive, alors la dernière case à une case voisine au dessus
                    if ((nombreZone - nbColonne) >= 1) {
                        zoneDebut = jardin.ZoneList[nombreZone - nbColonne - 1];
                        zoneFin = jardin.ZoneList[nombreZone  - 1];
                        acces = fabriqueAbstraiteFourmiliere.CreerAcces(zoneDebut, zoneFin);
                        jardin.AjouteChemins(acces);
                    }
                }
            }

            //FIN TODO FIN TODO FIN TODO
            //FIN TODO ajouter le client qui va utiliser la fabriqueAbstraite et faire le code suivant



            //TODO TODO TODO TODO
            //TODO Création et positionnement d'une fourmi
            Random random  = new Random();
            int colX =  random.Next(1, nbColonne);
            int colY = random.Next(1, nbLigne);

            AntWeather antWeatherForecast = new AntWeather();

            var zone = jardin.ZoneList[((colY-1) * nbColonne + colX - 1)];
            zone.AjouterPersonnage(fabriqueAbstraiteFourmiliere.CreerPersonnage("Fourmi 1", antWeatherForecast));

            
            //ceci est une fourmi ;)
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Red);
            ellipse.Margin = new Thickness(3);
            Grille.Children.Add(ellipse);
            Grid.SetColumn(ellipse, colX);
            Grid.SetRow(ellipse, colY);
            //FIN TODO création et positionnement d'une fourmi


            Console.Write(jardin.Statistiques());
            Console.Write("\n"+zone + " x : " + colX + " y : " + colY);
            Console.Write(jardin.Simuler());





            generation = true;
        }
    }
}
