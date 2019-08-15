using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private String _image_path;
        public String Image_Path {
            get { return _image_path; }
            set {
                _image_path = value;
                OnPropertyChanged("Image_Path");
            }
        }

        private String _audio_path;
        public String Audio_Path {
            get { return _audio_path; }
            set {
                _audio_path = value;
                OnPropertyChanged("Audio_Path");
            }
        }

        public ICommand OpenImage {
            get {
                return new Models.DelegateCommand((obj) =>
                {
                    Image_Path = OpenFile("Image");
                });
            }
        }
        public ICommand OpenAudio {
            get {
                return new Models.DelegateCommand((obj) =>
                {
                    Audio_Path = OpenFile("Audio");
                });
            }
        }

        public String OpenFile(String filter="") {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            switch (filter) {
                case "Audio":
                    ofd.Filter = "Audio Files|*.wav;*.mp3";
                    break;
                case "Image":
                    ofd.Filter = "Images|*.png;*.jpg";
                    break;
                default:
                    break;
            }
            if (ofd.ShowDialog() == true) return ofd.FileName;
            return "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String info = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
