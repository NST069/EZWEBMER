using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EZWEBMER_2._0.Viewmodels
{
    class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String info = "")
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
