﻿using AntBox.Environnement;
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
        public List<MyRow> ListBoxData { get; set; }

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

            int nbLigne = 0;
            int nbColonne = 0;

            CustomDialog cd = new CustomDialog("Veuillez saisir le nombre de lignes : ", "Veuillez saisir le nombre de colonnes :", "");
            cd.ShowDialog();

            if(cd.X.Text == "" || cd.Y.Text == "")
            {
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
            Uri uriAnt = new Uri("./Resources/ant.png", UriKind.Relative);
            AntWeather antWeatherForecast = new AntWeather();

            for (int a = 0; a < 5; a++)
            {
                int colX = random.Next(1, nbColonne);
                int colY = random.Next(1, nbLigne);

                

                var zone = jardin.ZoneList[((colY - 1) * nbColonne + colX - 1)];
                zone.AjouterPersonnage(fabriqueAbstraiteFourmiliere.CreerPersonnage("Fourmi "+a, antWeatherForecast));

                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                image.Source = new BitmapImage(uriAnt);
                Grille.Children.Add(image);
                Grid.SetColumn(image, colX);
                Grid.SetRow(image, colY);
            }
            //FIN TODO création et positionnement d'une fourmi






           //TODO elipse
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Red);
            ellipse.Margin = new Thickness(3);
            Grille.Children.Add(ellipse);
            Grid.SetColumn(ellipse, 0);
            Grid.SetRow(ellipse, 0);

            Grid.SetColumnSpan(ellipse, nbColonne);
            Grid.SetRowSpan(ellipse, nbLigne);

            ellipse.Opacity = 0.5;
            //TODO fin elipse

            


            Console.WriteLine(jardin.Statistiques());
            Console.WriteLine(jardin.Simuler());

            antWeatherForecast.Etat = "Il fait beau !";



            generation = true;
        }
    }
}
