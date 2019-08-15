using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EZWEBMER_2._0.Viewmodels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private int clicks;
        public int Clicks {
            get { return clicks; }
            set {
                clicks = value;
                OnPropertyChanged();
            }
        }
        public ICommand Click {
            get {
                return new Models.DelegateCommand((obj) =>
                {
                    Clicks++;
                }, (obj) => Clicks < 10);
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String info = "")
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
