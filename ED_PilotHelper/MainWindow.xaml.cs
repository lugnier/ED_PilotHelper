using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ED_PilotHelper.ViewModels;
using Syncfusion.Windows.Tools.Controls;

namespace ED_PilotHelper
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

            ((this.DataContext) as MainWindowViewModel).CanvasMain = CanvasMain;
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
                bss.Background = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
