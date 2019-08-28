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
        private Models.ImageInfo _imageInfo;
        public Models.ImageInfo ImageInfo {
            get { return _imageInfo; }
            set {
                _imageInfo = value;
                OnPropertyChanged("ImageInfo");
            }
        }
        private Models.MusicInfo _musicinfo;
        public Models.MusicInfo MusicInfo {
            get { return _musicinfo; }
            set {
                _musicinfo = value;
                OnPropertyChanged("Musicinfo");
            }
        }

        public String ImageStr {
            get {
                if (ImageInfo != null)
                    return "["+(ImageInfo.isValid?"Valid":"Invalid")+"]"+ImageInfo.Path+" "+ImageInfo.Width+"x"+ImageInfo.Height +((ImageInfo.gif!=null)?" Frames: "+ImageInfo.gif.framesCount:""); 
                return "Picture Not Selected";
            }
            set { OnPropertyChanged("ImageStr"); }
        }
        public String MusicStr {
            get {
                if (MusicInfo != null)
                    return "[" + (MusicInfo.isValid ? "Valid" : "Invalid") + "]" + MusicInfo.Path + " " + (MusicInfo.isPlaying?"Playing":"Stopped");
                return "Music Not Selected";
            }
            set { OnPropertyChanged("MusicStr"); }
        }

        public ICommand OpenImage {
            get {
                return new Models.DelegateCommand((obj) =>
                {
                    String Image_Path = Models.FileHandler.OpenFile("Image");
                    if (Image_Path != "")
                    {
                        ImageInfo = new Models.ImageInfo(Image_Path);
                        ImageStr += "";
                    }
                });
            }
        }
        public ICommand OpenAudio {
            get {
                return new Models.DelegateCommand((obj) =>
                {
                    String Audio_Path = Models.FileHandler.OpenFile("Audio");
                    if (Audio_Path != "")
                    {
                        MusicInfo = new Models.MusicInfo(Audio_Path);
                        MusicStr += "";
                        S_PlayPause += "";
                    }
                });
            }
        }
        public String S_PlayPause {
            get {
                if (MusicInfo != null && MusicInfo.isPlaying) return "Stop";
                else return "Play";
            }
            set { OnPropertyChanged("Playpause"); }
        }
        public ICommand PlayPause {
            get {
                return new Models.DelegateCommand((obj) =>
                {
                    if (MusicInfo.isPlaying)
                    {
                        MusicInfo.Stop();
                    }
                    else
                    {
                        MusicInfo.Play();
                    }

                    MusicStr += "";
                    S_PlayPause += "";

                }, (obj)=>{
                    if (MusicInfo != null) return MusicInfo.isValid;
                    return false;
                });
            }
        }
        public ICommand Render{
            get {
                return new Models.DelegateCommand((obj) =>
                {
                    
                    if (ImageInfo!=null && MusicInfo!=null)
                    {
                        String saveFile = Models.FileHandler.SaveFile();
                        if (saveFile != "")
                        {
                            Models.FFMpegProcess.Start(ImageInfo, MusicInfo, saveFile);
                        }
                    }
                }, (obj)=> {
                    if (MusicInfo != null && ImageInfo != null)
                        return MusicInfo.isValid && ImageInfo.isValid;
                    return false;
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
