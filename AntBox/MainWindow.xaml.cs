using AntBox.Environnement;
using AntBox.Factory;
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
            Grille.Background = new SolidColorBrush(Colors.AliceBlue);
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
            Console.WriteLine("Génération de la fourmilière");
            int nbColonne   = 3;
            int nbLigne     = 2;

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


            Console.Write(jardin.Statistiques());
        }
    }
}
