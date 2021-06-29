using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Pie.EntityFramework.Services
{
    public class PropertyChangedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
