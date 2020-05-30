using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Utils;

namespace ED_PilotHelper.Models.Pad
{
	class InfosZone : ViewModelBase
    {
        private Int32 _NumeroZone;
        public Int32 NumeroZone
        {
            get => _NumeroZone;
            set => RaisePropertyChanged(ref _NumeroZone, value);
        }

        private Int32 _NombrePads;
        public Int32 NombrePads
        {
            get => _NombrePads;
            set => RaisePropertyChanged(ref _NombrePads, value);
        }

        private ObservableCollection<InfosPad> _Pads = new ObservableCollection<InfosPad>();
        public ObservableCollection<InfosPad> Pads
        {
            get => _Pads;
            set => RaisePropertyChanged(ref _Pads, value);
        }



    }
}
