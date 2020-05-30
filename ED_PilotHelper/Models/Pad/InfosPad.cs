using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Utils;

namespace ED_PilotHelper.Models.Pad
{
	class InfosPad : ViewModelBase
    {
        private Point _Position;
        public Point Position
        {
            get => _Position;
            set => RaisePropertyChanged(ref _Position, value);
        }

        private String _Taille;
        public String Taille
        {
            get => _Taille;
            set => RaisePropertyChanged(ref _Taille, value);
        }
        private Double _LongueurPad;
        public Double LongueurPad
        {
            get => _LongueurPad;
            set => RaisePropertyChanged(ref _LongueurPad, value);
        }



        private Int32 _Numero;
        public Int32 Numero
        {
            get => _Numero;
            set => RaisePropertyChanged(ref _Numero, value);
        }



    }
}
