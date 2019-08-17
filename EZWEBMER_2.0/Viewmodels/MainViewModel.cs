using System;
using System.Collections.Generic;
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
                    Image_Path = Models.FileHandler.OpenFile("Image");
                });
            }
        }
        public ICommand OpenAudio {
            get {
                return new Models.DelegateCommand((obj) =>
                {
                    Audio_Path = Models.FileHandler.OpenFile("Audio");
                });
            }
        }
        public ICommand Render{
            get {
                return new Models.DelegateCommand((obj) =>
                {
                    if (!File.Exists(Image_Path)) System.Windows.MessageBox.Show("No Picture Found");
                    if (!File.Exists(Audio_Path)) System.Windows.MessageBox.Show("No Music Found");
                    if (File.Exists(Image_Path) && File.Exists(Audio_Path))
                    {
                        String saveFile = Models.FileHandler.SaveFile(".webm");
                        Models.FFMpegProcess.Start(Image_Path, Audio_Path, saveFile);
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String info = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
