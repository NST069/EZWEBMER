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
                }, (obj) => Files.Count<10);
            }
        }

        public ICommand RemoveCmd
        {
            get
            {
                return new Models.DelegateCommand((obj) =>
                {
                    List<CommandWndViewModel> list = ((Collection<object>)obj).Cast<CommandWndViewModel>().ToList();
                    list.ForEach(f => Files.Remove(f));
                }, (obj) => Files.Count > 0 && ((Collection<object>)obj).Count>0); 
            }
        }

        public ICommand Render
        {
            get
            {
                return new Models.DelegateCommand((obj) =>
                {
                    foreach (var x in Files)
                    {
                        (x as CommandWndViewModel).Execute();
                    }
                }, (obj)=> Files.Count > 0);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String info = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
