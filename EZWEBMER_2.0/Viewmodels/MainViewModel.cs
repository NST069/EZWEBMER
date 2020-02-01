using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace EZWEBMER_2._0.Viewmodels
{
    class MainViewModel : INotifyPropertyChanged
    {
        ObservableCollection<CommandWndViewModel> _files = new ObservableCollection<CommandWndViewModel>();
        public ObservableCollection<CommandWndViewModel> Files
        {
            get
            {
                return _files;
            }
            private set
            {
                _files = value;
                OnPropertyChanged(nameof(Files));

            }
        }


        public ICommand AddCmd
        {
            get
            {
                return new Models.DelegateCommand((obj) =>
                {
                    Files.Add(new CommandWndViewModel());
                }, (obj) => true);
            }
        }

        public ICommand RemoveCmd
        {
            get
            {
                return new Models.DelegateCommand((obj) =>
                {
                    Files.RemoveAt(Files.Count - 1);
                }, (obj) => Files.Count > 0); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String info = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
