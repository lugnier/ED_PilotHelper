using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Utils
{
    /// <summary>
    /// classe mère de tous les viewmodels
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// évenement de changement de valeur
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// UI principale
        /// </summary>
        public Window CurrentWindow { get; set; }

        /// <summary>
        /// UserControl que l'on a inséré
        /// </summary>
        public UserControl CurrentUserControl { get; set; }


        /// <summary>
        /// propagation de l'information de modification d'une information
        /// </summary>
        /// <param name="pPropName"></param>
        protected void RaisePropertyChanged(String pPropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pPropName));
        }

        /// <summary>
        /// propagation de l'information de modification d'une information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pProp"></param>
        /// <param name="pValue"></param>
        /// <param name="pPropName"></param>
        protected void RaisePropertyChanged<T>(
            ref T pProp, T pValue, [CallerMemberName] String pPropName = "")
        {
            if (
                (pProp == null && pValue != null)
                ||
                (pProp != null && pValue != null && !pProp.Equals(pValue)))
            {

                pProp = pValue;
                RaisePropertyChanged(pPropName);

            }
        }

        /// <summary>
        /// propagation de l'information de modification d'une information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pProp"></param>
        /// <param name="pValue"></param>
        /// <param name="pPropName"></param>
        protected void RaisePropertyChangedEnableNull<T>(
            ref T pProp, T pValue, [CallerMemberName] String pPropName = "")
        {
            if (
                (pProp == null)
                ||
                (pProp != null && !pProp.Equals(pValue)))
            {

                pProp = pValue;
                RaisePropertyChanged(pPropName);

            }
        }
    }
}
