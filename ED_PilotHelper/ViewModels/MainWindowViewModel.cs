using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ED_PilotHelper.Helpers;
using ED_PilotHelper.Helpers.EDDB;
using ED_PilotHelper.Models.Events;
using ED_PilotHelper.Models.Pad;
using Newtonsoft.Json;
using Utils;
using Path = System.IO.Path;

namespace ED_PilotHelper.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {

        public String LaunchScanLabel => _IsLive ? "Stop Live" : "Start Live";
        internal String EventLabel => "Events";
        //public String NextSystemLabel => "Next System";
        internal String ScoopableLabel => "Scoopable";

        private readonly String _scoopableStars = "KGBFOAM";

        private String _NextSystemLabel = "Next System";
        public String NextSystemLabel
        {
            get => _NextSystemLabel;
            set => RaisePropertyChanged(ref _NextSystemLabel, value);
        }



        private Boolean _IsRunning = false;
        private List<InfosZone> _zones = new List<InfosZone>();
        private Double hauteur;
        private Double longueurCellule;

        public Canvas CanvasMain;
        SolidColorBrush _mainColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFBF7F"));
        SolidColorBrush _HighlightColor = new SolidColorBrush(Colors.Orange);
        SolidColorBrush _backColor = new SolidColorBrush(Colors.White);
        private Double _fontSize = 15;

        private SolidColorBrush BackStageColor => new SolidColorBrush(Colors.Orange);
        private SolidColorBrush BackBorderColor => new SolidColorBrush(Colors.Orange);

        private static Int32 _lastCheckHour = 0;

        private Boolean _IsLive = false;

        public Boolean IsLive
        {
            get { return _IsLive; }
            set
            {
                RaisePropertyChanged(ref _IsLive, value);
                RaisePropertyChanged(nameof(LaunchScanLabel));
            }
        }

        private String _LastEventLabel;
        public String LastEventLabel
        {
            get => _LastEventLabel;
            set => RaisePropertyChanged(ref _LastEventLabel, value);
        }


        private ObservableCollection<EdEventBase> _Events = new ObservableCollection<EdEventBase>();
        public ObservableCollection<EdEventBase> Events
        {
            get => _Events;
            set => RaisePropertyChanged(ref _Events, value);
        }

        private ImageSource _FuelScoopImageSource;
        public ImageSource FuelScoopImageSource
        {
            get => _FuelScoopImageSource;
            set => RaisePropertyChanged(ref _FuelScoopImageSource, value);
        }


        private ImageSource _ImageStation;
        public ImageSource ImageStation
        {
            get => _ImageStation;
            set => RaisePropertyChanged(ref _ImageStation, value);
        }

        private Visibility _ImageVisibility = Visibility.Collapsed;
        public Visibility ImageVisibility
        {
            get => _ImageVisibility;
            set => RaisePropertyChanged(ref _ImageVisibility, value);
        }

        private Size _LandingPadRenderSize;
        public Size LandingPadRenderSize
        {
            get => _LandingPadRenderSize;
            set => RaisePropertyChanged(ref _LandingPadRenderSize, value);
        }

        private ObservableCollection<String> _NavRoutes = new ObservableCollection<string>();
        public ObservableCollection<String> NavRoutes
        {
            get => _NavRoutes;
            set => RaisePropertyChanged(ref _NavRoutes, value);
        }

        private Visibility _LandingPadVisibility = Visibility.Hidden;
        public Visibility LandingPadVisibility
        {
            get => _LandingPadVisibility;
            set => RaisePropertyChanged(ref _LandingPadVisibility, value);
        }







        public RelayCommand LaunchLiveCommand { get; set; }
        public RelayCommand LandingpadSizeChangedCommand { get; set; }


        public MainWindowViewModel()
        {
            InitCommands();
            InitZones();

            FuelScoopImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Assets/FuelScoop_gray_193x193.png", UriKind.Absolute));

            //Events.CollectionChanged += (s, e) =>
            //{
            //    //Debugger.Break();
            //    RaisePropertyChanged(nameof(Events));
            //};
        }



        /// <summary>
        /// init des zones d'une station (nombre de pad par section)
        /// </summary>
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

            LandingpadSizeChangedCommand = new RelayCommand(x => InitDraws());
        }

        /// <summary>
        /// dessine un plan des pads d'une station
        /// </summary>
        public void InitDraws(Boolean clearCanvasOnly = false)
        {
            // remise à 0 du canvas
            CanvasMain.Children.Clear();
            if (clearCanvasOnly)
            {
                return;
            }

            CanvasMain.Background = new SolidColorBrush(Colors.Black);
            foreach (InfosZone infosZone in _zones)
            {
                infosZone.Pads.Clear();
            }

            // récupération de la taille de la zone d'affichage
            //hauteur = gridMain.RowDefinitions[2].ActualHeight;
            hauteur = Math.Min(CanvasMain.ActualHeight, CanvasMain.ActualWidth);

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

        /// <summary>
        ///  launch file watching
        /// </summary>
        private void LaunchLiveImplementation()
        {
            if (IsLive)
            {
                IsLive = false;
            }
            else
            {
                IsLive = true;
                ReadFile();
            }
        }

        /// <summary>
        /// scan the log file to get new events
        /// </summary>
        private void ReadFile()
        {
            Task.Factory.StartNew(() =>
            {
                DirectoryInfo di = new DirectoryInfo(DataHelper.GetString(Helpers.OptionsKeys.LogFile.ToString()));

                if (di == null)
                {
                    throw new FileNotFoundException("Log files not founds");
                }

                FileInfo file = di.GetFiles("*.log").OrderByDescending(x => x.LastWriteTime).FirstOrDefault();

                if (file == null)
                {
                    throw new FileNotFoundException("Log files not founds");
                }

                var fileName = file.FullName;

                //List<String> listeFichiers = Directory
                //    .GetFiles(@"C:\Users\lugni\Saved Games\Frontier Developments\Elite Dangerous", "*.cache").ToList();

                List<String> listeFichiers = new List<string>() { fileName };

                using (FileStream fs = new FileStream
                    (fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        while (IsLive)
                        {
                            while (!sr.EndOfStream)
                                ProcessLinr(sr.ReadLine());

                            while (sr.EndOfStream && IsLive)
                            {
                                Thread.Sleep(1000);
                                Int32 _t = DateTime.Now.Hour;
                                if (_t < _lastCheckHour)
                                {
                                    _lastCheckHour = _t;
                                    ReadFile();
                                    return;
                                }
                            }

                            ProcessLinr(sr.ReadLine());
                        }
                    }
                }
            });
        }

        /// <summary>
        /// analyse new log line to get and process an event
        /// </summary>
        /// <param name="readLine"></param>
        private void ProcessLinr(string readLine)
        {
            if (String.IsNullOrEmpty(readLine))
            {
                return;
            }
            Log(readLine);
            EdEventBase eb = JsonConvert.DeserializeObject<EdEventBase>(readLine);
            if (eb == null)
            {
                return;
            }
            Console.WriteLine(eb.Timestamp);
            Console.WriteLine(eb.Event);

            LastEventLabel = $"{eb.Event} at {eb.Timestamp}";
            //Events.Insert(0, eb);
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() => { Events.Insert(0, eb); }));


            switch (eb.Event)
            {
                case "DockingGranted":
                    EdDockingGranted dg = JsonConvert.DeserializeObject<EdDockingGranted>(readLine);
                    DockingGranted(dg);
                    break;
                case "Undocked":
                    EdUndocked ed = JsonConvert.DeserializeObject<EdUndocked>(readLine);
                    Undocked(ed);
                    break;
                case "FSDJump": // arrivé dans le système
                    EdFSDJump fsdj = JsonConvert.DeserializeObject<EdFSDJump>(readLine);
                    FsdJump(fsdj);
                    break;
                case "StartJump":
                    EdStartJump sje = JsonConvert.DeserializeObject<EdStartJump>(readLine);
                    StartJump(sje);
                    break;
                case "SupercruiseExit":
                    EdSupercruiseExit se = JsonConvert.DeserializeObject<EdSupercruiseExit>(readLine);
                    SupercruiseExit(se);
                    break;
                case "FSDTarget":
                    EdFSDTarget fsdt = JsonConvert.DeserializeObject<EdFSDTarget>(readLine);
                    FSDTarget(fsdt);
                    break;
                case "NavRoute":
                    NavRoute();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// sortie d'hyperespace - rien à faire pour l'instant
        /// </summary>
        /// <param name="edFsdJump"></param>
        private void FsdJump(EdFSDJump edFsdJump)
        {

        }

        /// <summary>
        /// use the navroute file to list all the system of the trip
        /// </summary>
        private void NavRoute()
        {
            DirectoryInfo di = new DirectoryInfo(DataHelper.GetString(Helpers.OptionsKeys.LogFile.ToString()));

            if (di == null)
            {
                throw new FileNotFoundException("NavRoute dir not found");
            }

            FileInfo file = new FileInfo(Path.Combine(di.FullName, "NavRoute.json"));

            if (file == null)
            {
                throw new FileNotFoundException("NavRoute file not found");
            }

            var fileName = file.FullName;

            using (FileStream fs = new FileStream
                (fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {

                    String jsonContent = sr.ReadToEnd();
                    var navRouteDetails = JsonConvert.DeserializeObject<EdNavRouteDetails>(jsonContent);


                  NavRoutes = new ObservableCollection<string>(navRouteDetails.Routes.Select(x=>
                  {
                      String scoop = (_scoopableStars.Contains(x.StarClass)) ? " -> SCOOPABLE" : "";
                      return $"{x.StarSystem}{scoop}";
                  }));

                        //NavRoutes.Clear();
                        //foreach (EdRoute edRoute in navRouteDetails.Routes)
                        //{
                        //    String scoop = (_scoopableStars.Contains(edRoute.StarClass)) ? " -> SCOOPABLE" : "";
                        //    NavRoutes.Add($"{edRoute.StarSystem}{scoop}");
                        //}
                

                }
            }
        }

        /// <summary>
        /// undock - clear landing pad highlight
        /// </summary>
        /// <param name="ed"></param>
        private void Undocked(EdUndocked ed)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() => { InitDraws(); }));

        }

        /// <summary>
        /// ciblage et demande de fsd
        /// </summary>
        /// <param name="fsdt"></param>
        private void FSDTarget(EdFSDTarget fsdt)
        {


            if (fsdt == null)
            {
                return;
            }

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                NextSystemLabel = $"Next jump to {fsdt.Name} ({fsdt.RemainingJumpsInRoute} remaining)";
                if (_scoopableStars.Contains(fsdt.StarClass.ToUpper()))
                {
                    FuelScoopImageSource = new BitmapImage(new Uri(
                        @"pack://application:,,,/Assets/FuelScoop_green_193x193.png", UriKind.Absolute));
                }
                else
                {
                    FuelScoopImageSource = new BitmapImage(new Uri(
                        @"pack://application:,,,/Assets/FuelScoop_gray_193x193.png", UriKind.Absolute));
                }

                // mise à jour de la liste des systèmes restants
                NavRoutes.RemoveAt(0);
            }));


        }

        /// <summary>
        /// Sortie de supercruise et choix de l'affichage de l'intérieur de la station ou bien de l'image de la station
        /// </summary>
        /// <param name="se"></param>
        private void SupercruiseExit(EdSupercruiseExit se)
        {
            if (se == null)
            {
                return;
            }

            // comme il peut y avoir plusieurs stations avec le même nom, on récupère le système id
            var sp = new SystemsParams();
            sp.Params[SystemsParams.ParamName.name].Valeur = se.StarSystem;
            var repSystems = Helpers.EDDB.EddbHelper.GetSystems(sp)?.Result;
            if (repSystems.Count > 1)
            {
                //Debugger.Break();
                return;
            }

            // prepare to get station details on eddb
            var ps = new StationsParams();

            ps.Params[StationsParams.ParamName.name].Valeur = se.Body;

            var r = Helpers.EDDB.EddbHelper.GetStations(ps)?.Result;
            if (r == null || r.Count == 0)
            {
                //Debugger.Break();
                return;
            }

            var stati = r.Where(x => x.system_id == repSystems.First().id);

            if (stati == null)
            {
                //Debugger.Break();
                return;
            }


            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Events.Insert(0, new EdEventBase() { Event = stati.First().type.ToLower() });

                switch (stati.First().type.ToLower())
                {
                    case "ocellus starport":
                        MessageCenter.Send(MessageCenterMessages.RefreshStationsImages, new List<String>()
                        {
                            "/Assets/Station_Ocellus.jpg",
                        });
                        LandingPadVisibility = Visibility.Visible;
                        //ImageStation = new BitmapImage(new Uri(
                        //    @"pack://application:,,,/Assets/Station_Ocellus.jpg", UriKind.Absolute));
                        break;
                    case "coriolis starport":
                        MessageCenter.Send(MessageCenterMessages.RefreshStationsImages, new List<String>()
                        {
                            "/Assets/Station_Coriolis.jpg",
                        });
                        LandingPadVisibility = Visibility.Visible;
                        //ImageStation = new BitmapImage(new Uri(
                        //    @"pack://application:,,,/Assets/Station_Coriolis.jpg", UriKind.Absolute));
                        break;
                    case "orbis starport":
                        MessageCenter.Send(MessageCenterMessages.RefreshStationsImages, new List<String>()
                        {
                            "/Assets/Station_Orbis.jpg",
                        });
                        LandingPadVisibility = Visibility.Visible;
                        //ImageStation = new BitmapImage(new Uri(
                        //    @"pack://application:,,,/Assets/Station_Orbis.jpg", UriKind.Absolute));
                        break;
                    case "industrial outpost":
                        MessageCenter.Send(MessageCenterMessages.RefreshStationsImages, new List<String>()
                        {
                            "/Assets/schema_industrial_mining.png",
                        });
                        LandingPadVisibility = Visibility.Hidden;
                        //ImageStation = new BitmapImage(new Uri(
                        //    @"pack://application:,,,/Assets/schema_industrial.png", UriKind.Absolute));
                        break;
                    case "mining outpost":
                        MessageCenter.Send(MessageCenterMessages.RefreshStationsImages, new List<String>()
                        {
                            "/Assets/schema_industrial_mining.png",
                        }); 
                        LandingPadVisibility = Visibility.Hidden;
                        //ImageStation = new BitmapImage(new Uri(
                        //    @"pack://application:,,,/Assets/schema_mining.png", UriKind.Absolute));
                        break;
                    case "commercial outpost":
                        MessageCenter.Send(MessageCenterMessages.RefreshStationsImages, new List<String>()
                        {
                            "/Assets/schema_commercial.png",
                        });
                        LandingPadVisibility = Visibility.Hidden;
                        //ImageStation = new BitmapImage(new Uri(
                        //    @"pack://application:,,,/Assets/schema_commercial.png", UriKind.Absolute));
                        break;
                    case "civilian outpost":
                        MessageCenter.Send(MessageCenterMessages.RefreshStationsImages, new List<String>()
                        {
                            "/Assets/schema_civilian_industrial.png",
                            "/Assets/schema_civillian.png",
                        });
                        LandingPadVisibility = Visibility.Hidden;
                        //ImageStation = new BitmapImage(new Uri(
                        //    @"pack://application:,,,/Assets/schema_civillian.png", UriKind.Absolute));
                        break;
                    case "military outpost":
                        MessageCenter.Send(MessageCenterMessages.RefreshStationsImages, new List<String>()
                        {
                            "/Assets/schema_military.png",
                        });
                        LandingPadVisibility = Visibility.Hidden;
                        //ImageStation = new BitmapImage(new Uri(
                        //    @"pack://application:,,,/Assets/schema_military.png", UriKind.Absolute));
                        break;
                    case "scientific outpost":
                        MessageCenter.Send(MessageCenterMessages.RefreshStationsImages, new List<String>()
                        {
                            "/Assets/schema_scientific.png",
                        });
                        LandingPadVisibility = Visibility.Hidden;
                        //ImageStation = new BitmapImage(new Uri(
                        //    @"pack://application:,,,/Assets/schema_scientific.png", UriKind.Absolute));
                        break;

                    default:
                        break;
                }
            }));
            //String s = stati.First().type.ToLower();
        }

        /// <summary>
        /// passage en vitesse rapide
        /// </summary>
        /// <param name="sje"></param>
        private void StartJump(EdStartJump sje)
        {
            Debug.WriteLine(sje.JumpType);

            switch (sje.JumpType)
            {
                case "Hyperspace": // on va dans un autre système, on regarde si l'étoile est scoopable
                    Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                        new Action(() =>
                        {
                            if (_scoopableStars.Contains(sje.StarClass.ToUpper()))
                            {
                                FuelScoopImageSource = new BitmapImage(new Uri(
                                    @"pack://application:,,,/Assets/FuelScoop_green_193x193.png", UriKind.Absolute));
                            }
                            else
                            {
                                FuelScoopImageSource = new BitmapImage(new Uri(
                                    @"pack://application:,,,/Assets/FuelScoop_gray_193x193.png", UriKind.Absolute));
                            }
                        }));
                    break;
                case "Supercruise": // on reste dans le même système : on ne fait rien
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Highlight the destination pad
        /// </summary>
        /// <param name="dg"></param>
        private void DockingGranted(EdDockingGranted dg)
        {
            Console.WriteLine(dg.LandingPad);
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() => { HighlightPad((Int32)(dg.LandingPad)); }));
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

        /// <summary>
        /// draw the highlighting of the pad
        /// </summary>
        /// <param name="numPad"></param>
        private void HighlightPad(Int32 numPad)
        {
            InitDraws();
            InfosPad ip = GetPad(numPad);
            if (ip != null)
            {
                DrawCircle(ref CanvasMain, ip.Position, ip.LongueurPad / 3);
            }
        }

        /// <summary>
        /// get informations from pad number
        /// </summary>
        /// <param name="numeroPad"></param>
        /// <returns></returns>
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

        #region Dessins
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
        #endregion
    }
}
