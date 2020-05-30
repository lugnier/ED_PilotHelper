using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using ED_PilotHelper.Models.Events;
using ED_PilotHelper.Models.Pad;
using Newtonsoft.Json;
using Utils;

namespace ED_PilotHelper.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private Boolean _IsRunning = false;
        private List<InfosZone> _zones = new List<InfosZone>();
        private Double hauteur;
        private Double longueurCellule;

        public Canvas CanvasMain;
        SolidColorBrush _mainColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFBF7F"));
        SolidColorBrush _HighlightColor = new SolidColorBrush(Colors.Orange);
        SolidColorBrush _backColor = new SolidColorBrush(Colors.White);
        private Double _fontSize = 15;


        public RelayCommand LaunchLiveCommand { get; set; }


        public MainWindowViewModel()
        {
            InitCommands();
            InitZones();

            //InitDraws();
        }

        private void InitZones()
        {

            //pads 1 à 4
            _zones.Add(new InfosZone() { NumeroZone = 1, NombrePads = 4 });
            //pads 5 à 8
            _zones.Add(new InfosZone() { NumeroZone = 2, NombrePads = 4 });
            //pads 9 à 10
            _zones.Add(new InfosZone() { NumeroZone = 3, NombrePads = 2 });
            //pads 11 à 15
            _zones.Add(new InfosZone() { NumeroZone = 4, NombrePads = 5 });
            //pads 16 à 19
            _zones.Add(new InfosZone() { NumeroZone = 5, NombrePads = 4 });
            //pads 20 à 23
            _zones.Add(new InfosZone() { NumeroZone = 6, NombrePads = 4 });
            //pads 24 à 25
            _zones.Add(new InfosZone() { NumeroZone = 7, NombrePads = 2 });
            //pads 26 à 30
            _zones.Add(new InfosZone() { NumeroZone = 8, NombrePads = 5 });
            //pads 31 à 34
            _zones.Add(new InfosZone() { NumeroZone = 9, NombrePads = 4 });
            //pads 35 à 38
            _zones.Add(new InfosZone() { NumeroZone = 10, NombrePads = 4 });
            //pads 39 à 40
            _zones.Add(new InfosZone() { NumeroZone = 11, NombrePads = 2 });
            //pads 41 à 45
            _zones.Add(new InfosZone() { NumeroZone = 12, NombrePads = 5 });
        }

        private void InitCommands()
        {
            LaunchLiveCommand = new RelayCommand(() => LaunchLiveImplementation());
        }

        public void InitDraws()
        {
            // remise à 0 du canvas
            CanvasMain.Children.Clear();
            CanvasMain.Background = new SolidColorBrush(Colors.Black);
            foreach (InfosZone infosZone in _zones)
            {
                infosZone.Pads.Clear();
            }

            // récupération de la taille de la zone d'affichage
            //hauteur = gridMain.RowDefinitions[2].ActualHeight;
            hauteur = CanvasMain.ActualHeight;

            // init des variables de dessin
            Double largeur = hauteur;

            Double rayon = largeur / 2; // rayon de la sphere qui contient tout le dessin
            Double rayonInterieur = largeur / 6; // rayon de la sphere au centre qui reste vide
            Point centre = new Point(largeur / 2, hauteur / 2); // centre du dessin

            Double angleBase = 15; // il y a 12 zones tout autour du cylindre pour accueillir les pads
            Double angle = 360 - 105; // angle en cours de calcul


            Point pointDepart;
            Point pointArrivee;
            Point pointDepartPrecedent = new Point(0, 0); // nécessaire pour le calcul des lignes de délimitation des pads
            Point pointDepartInitial = new Point(0, 0);
            Int32 numeroPad = 0;

            for (int numeroZone = 0; numeroZone < _zones.Count; numeroZone++)
            {
                longueurCellule = (rayon - rayonInterieur) / _zones[numeroZone].NombrePads; // distance entre 2 landing pad
                angle = 285 - numeroZone * 2 * angleBase;



                for (int numeroCellule = _zones[numeroZone].NombrePads; numeroCellule >= 0; numeroCellule--)
                {
                    // calcul des points de départ et d'arrivée
                    pointDepart = new Point(centre.X + ((rayon - numeroCellule * longueurCellule) * Math.Cos(ToRadians(angle))),
                        (centre.Y) + (-1 * (rayon - numeroCellule * longueurCellule) * Math.Sin(ToRadians(angle))));

                    pointArrivee = new Point(centre.X + ((rayon - numeroCellule * longueurCellule) * Math.Cos(ToRadians(angle - (2 * angleBase)))),
                        (centre.Y) + (-1 * (rayon - numeroCellule * longueurCellule) * Math.Sin(ToRadians(angle - (2 * angleBase)))));

                    // dessin de la délimitation de la zone. 
                    // à ne faire qu'une seule fois
                    if (numeroCellule == _zones[numeroZone].NombrePads)
                    {
                        // délimitation des zones. on ne le fait que la première fois
                        DrawLine(ref CanvasMain, pointDepart, new Point(centre.X + (rayon * Math.Cos(ToRadians(angle))), (centre.Y) + (-1 * rayon * Math.Sin(ToRadians(angle)))));
                    }

                    // dessin de la délimitation de la cellule
                    DrawLine(ref CanvasMain, pointDepart, pointArrivee);


                    // création des informations de la zone
                    if (numeroCellule < _zones[numeroZone].NombrePads)
                    {
                        _zones[numeroZone].Pads.Add(new InfosPad()
                        {
                            Numero = ++numeroPad,
                            Position = new Point(centre.X +
                                        ((rayonInterieur + (numeroCellule * longueurCellule) + (longueurCellule / 2) - (2 * numeroCellule)) * Math.Cos(ToRadians(angle - angleBase))),
                                        (centre.Y) + (-1 * (rayonInterieur + (numeroCellule * longueurCellule) + (longueurCellule / 2) - (2 * numeroCellule)) *
                                                      Math.Sin(ToRadians(angle - angleBase)))),
                            Taille = "S",
                            LongueurPad = longueurCellule
                        });

                        DrawText(ref CanvasMain, _zones[numeroZone].Pads[_zones[numeroZone].NombrePads - numeroCellule - 1].Numero.ToString(), _zones[numeroZone].Pads[_zones[numeroZone].NombrePads - numeroCellule - 1].Position);
                    }
                }
            }


        }

        private Int32 pos = 0;

        private void LaunchLiveImplementation()
        {
            ReadFile();
            //pos++;
            //HighlightPad(pos);
        }

        private void ReadFile()
        {
            Task.Factory.StartNew(() =>
            {


                var fileName = @"C:\Users\lugni\Saved Games\Frontier Developments\Elite Dangerous\Journal.200530095904.01.log";

                //List<String> listeFichiers = Directory
                //    .GetFiles(@"C:\Users\lugni\Saved Games\Frontier Developments\Elite Dangerous", "*.cache").ToList();

                List<String> listeFichiers = new List<string>() { fileName };

                using (FileStream fs = new FileStream
                    (listeFichiers.First(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        while (true)
                        {
                            while (!sr.EndOfStream)
                                ProcessLinr(sr.ReadLine());

                            while (sr.EndOfStream)
                                Thread.Sleep(1000);

                            ProcessLinr(sr.ReadLine());
                        }
                    }
                }
            });
        }

        private void ProcessLinr(string readLine)
        {
            Log(readLine);
            EventBase eb = JsonConvert.DeserializeObject<EventBase>(readLine);
            Console.WriteLine(eb.Timestamp);
            Console.WriteLine(eb.Event);

            if (eb.Event == "DockingGranted")
            {
                DockingGrantedEvent dg = JsonConvert.DeserializeObject<DockingGrantedEvent>(readLine);
                Console.WriteLine(dg.LandingPad);
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() => { HighlightPad((Int32)(dg.LandingPad)); }));
            }
        }

        void Log(String s)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                {
                    //Debug.WriteLine(s);
                    //listboxLog.Items.Insert(0, s);

                }));
        }

        private void HighlightPad(Int32 numPad)
        {
            InitDraws();
            InfosPad ip = GetPad(numPad);
            if (ip != null)
            {
                DrawCircle(ref CanvasMain, ip.Position, ip.LongueurPad / 3);
            }
        }

        private InfosPad GetPad(Int32 numeroPad)
        {
            foreach (InfosZone infosZone in _zones)
            {
                foreach (InfosPad infosPad in infosZone.Pads)
                {
                    if (infosPad.Numero == numeroPad)
                    {
                        return infosPad;
                    }
                }
            }

            return null;
        }

        private void DrawLine(ref Canvas canvas, Point debut, Point fin)
        {
            Line myLine = new Line();
            myLine.Stroke = _mainColor;
            myLine.X1 = debut.X;
            myLine.X2 = fin.X;
            myLine.Y1 = debut.Y;
            myLine.Y2 = fin.Y;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 3;
            canvas.Children.Add(myLine);

        }

        private void DrawCircle(ref Canvas canvas, Point centre, Double rayon)
        {
            System.Windows.Shapes.Ellipse myEllipse = new Ellipse();
            myEllipse.Height = rayon * 2;
            myEllipse.Width = rayon * 2;
            myEllipse.Fill = _HighlightColor;
            Canvas.SetLeft(myEllipse, centre.X - rayon);
            Canvas.SetTop(myEllipse, centre.Y - rayon);
            CanvasMain.Children.Add(myEllipse);
        }

        private double ToRadians(Double degree)
        {
            return degree * (Math.PI / 180);
        }

        private void DrawText(ref Canvas canvas, String message, Point centre)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = message;
            textBlock.FontSize = _fontSize;
            textBlock.Foreground = _mainColor;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(textBlock, centre.X - 10);
            Canvas.SetTop(textBlock, centre.Y - 10);
            canvas.Children.Add(textBlock);
        }
    }
}
