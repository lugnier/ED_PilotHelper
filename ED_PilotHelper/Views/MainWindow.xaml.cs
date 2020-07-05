using ED_PilotHelper.ViewModels;
using Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ED_PilotHelper.Helpers;
using Syncfusion.Windows.Tools;
using Syncfusion.Windows.Tools.Controls;

namespace ED_PilotHelper.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
            (DataContext as ViewModelBase).CurrentWindow = this;

            ((this.DataContext) as MainWindowViewModel).CanvasMain = CanvasMain;

            MessageCenter.Subscribe(MessageCenterMessages.Quitter, () => this.Close());
            MessageCenter.Subscribe(MessageCenterMessages.RefreshStationsImages, (x) => RefreshStationsImages(x));

            DataHelper.Load();

            //this.DockingManagerMain.LoadDockState();
        }

        private void RefreshStationsImages(object o)
        {
            List<String> ls = o as List<String>;
            if (ls == null) 
            { return; }

            GridStationsImages.RowDefinitions.Clear();
            GridStationsImages.Children.Clear();
            for (int numRow = 0; numRow < ls.Count; numRow++)
            {
                GridStationsImages.RowDefinitions.Add(new RowDefinition());
                //ImageStation = new BitmapImage(new Uri(
                //    @"pack://application:,,,/Assets/Station_Ocellus.jpg", UriKind.Absolute));
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(String.Concat(@"pack://application:,,,", ls[numRow])));
                GridStationsImages.Children.Add(img);
                Grid.SetRow(img, numRow);
            }
        }

        ~MainWindow()
        {
            MessageCenter.UnsubscribeAll();
        }


        private void MainWindow_OnContentRendered(object? sender, EventArgs e)
        {
            ((this.DataContext) as MainWindowViewModel).InitDraws();
        }

        private void Backstage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_ribbonCoreUI.BackStage.SelectedItem != null)
            {
                if ((_ribbonCoreUI.BackStage.SelectedItem) is BackstageTabItem)
                {
                    //(_ribbonMain.BackStage.SelectedItem as BackstageTabItem).Loaded += MainWindow_Loaded;
                    Border bd = (_ribbonCoreUI.BackStage.SelectedItem as BackstageTabItem).Template.FindName("selectedBorder", _ribbonCoreUI.BackStage.SelectedItem as BackstageTabItem) as Border;
                    if (bd != null)
                        bd.Opacity = 0.5;
                }

            }
        }

        private void Backstage_Loaded(object sender, RoutedEventArgs e)
        {
            // recherche du controle
            Control bss = _ribbonCoreUI.BackStage.Template.FindName("BackStageSeparator", _ribbonCoreUI.BackStage) as Control;
            // si on a une BackStageSeparator
            if (bss != null)
            {
                // on change la couleur
                bss.Background = new SolidColorBrush(Colors.Yellow);
            }

            RibbonButton BackstageBackButton = _ribbonCoreUI.BackStage.Template.FindName("BackstageBackButton", _ribbonCoreUI.BackStage) as RibbonButton;
            if (BackstageBackButton != null)
            {
                BackstageBackButton.Visibility = Visibility.Collapsed;
                //BackstageBackButton.BorderBrush = new SolidColorBrush(Colors.PaleGreen);
                //BackstageBackButton.IconType = IconType.Icon;
                //BackstageBackButton.LargeIcon =
                //    new BitmapImage(new Uri(@"pack://application:,,,/Assets/button_orange_play.png", UriKind.Absolute));
                //BackstageBackButton.SmallIcon =
                //    new BitmapImage(new Uri(@"pack://application:,,,/Assets/button_orange_play.png", UriKind.Absolute));
                //BackstageBackButton.Background = new SolidColorBrush(Colors.PaleGreen);
                //BackstageBackButton.Foreground = new SolidColorBrush(Colors.PaleGreen);
                //BackstageBackButton.Opacity = 0.2;
                //BackstageBackButton = new RibbonButton() {Content = "aa"};
            }

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _ribbonCoreUI.HideBackStage();
        }

        private void ButtonQuit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
