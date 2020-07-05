using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ED_PilotHelper.Helpers;
using Microsoft.Win32;
using Utils;

namespace ED_PilotHelper.ViewModels
{
    class OptionsPageViewModel : ViewModelBase
    {
        public String LogFileDirectoryLabel => "Emplacement des fichiers de log";
        public String LogFileDirectoryTooltip => @"Par défaut dans C:\Users\" + Environment.UserName + @"\Saved Games\Frontier Developments\Elite Dangerous";

        public String ButtonSetDefaultPathLabel => "Utiliser ce dossier";

        public String ButtonBrowseLabel => "Parcourir";

        public String LogFileDirectory
        {
            get => DataHelper.GetString(Helpers.OptionsKeys.LogFile.ToString());
            set
            {
                DataHelper.AddOrUpdate(Helpers.OptionsKeys.LogFile.ToString(), value);
                RaisePropertyChanged(nameof(LogFileDirectory));
            }
        }

        public RelayCommand ButtonSetDefaultPathCommand { get; set; }
        public RelayCommand ButtonBrowseCommand { get; set; }

        public OptionsPageViewModel()
        {
            InitCommands();
        }

        private void InitCommands()
        {
            ButtonSetDefaultPathCommand = new RelayCommand(() => ButtonSetDefaultPathImplementation());

            ButtonBrowseCommand = new RelayCommand(() => ButtonBrowseImplementation());
        }

        private void ButtonBrowseImplementation()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = "*.log";
            ofd.Filter = ("Log (*.log)|*.log|Tous|*.*");
            ofd.FilterIndex = 0;
            ofd.InitialDirectory = "c:\\";
            ofd.Title = "Select any log file";
            if (ofd.ShowDialog() ?? false)
            {
                // get the directory
                FileInfo fi = new FileInfo(ofd.FileName);
                LogFileDirectory = fi?.DirectoryName ?? "";
            }



        }

        private void ButtonSetDefaultPathImplementation()
        {
            LogFileDirectory = @"C:\Users\" + Environment.UserName + @"\Saved Games\Frontier Developments\Elite Dangerous";
        }
    }
}
