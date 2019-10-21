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
        /*
        System.Timers.Timer timer = new System.Timers.Timer();
        private void UpdateSeekBar()
        {
            if (MusicInfo != null)
            {
                if (MusicInfo.isPlaying == NAudio.Wave.PlaybackState.Playing)
                {
                    aud_duration = MusicInfo.duration;
                    aud_position = MusicInfo.position;
                }
            }
            else
            {
                //aud_duration = 1;
                //aud_position = 0;
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            UpdateSeekBar();
        }
        */
        private Models.ImageInfo _imageInfo;
        public Models.ImageInfo ImageInfo {
            get { return _imageInfo; }
            set {
                _imageInfo = value;
                OnPropertyChanged(nameof(ImageInfo));
            }
        }
        private Models.MusicInfo _musicinfo;
        public Models.MusicInfo MusicInfo {
            get { return _musicinfo; }
            set {
                _musicinfo = value;
                OnPropertyChanged(nameof(MusicInfo));
            }
        }
        private Models.VideoInfo _videoinfo;
        public Models.VideoInfo VideoInfo {
            get { return _videoinfo; }
            set {
                _videoinfo = value;
                OnPropertyChanged(nameof(VideoInfo));
            }
        }

        /*
        private int _dur;
        public int aud_duration {
            get { return _dur; }
            set {
                _dur = value;
                OnPropertyChanged(nameof(aud_duration));
            }
        }
        private int _pos;
        public int aud_position{
            get { return _pos; }
            set
            {
                _pos = value;
                OnPropertyChanged(nameof(aud_position));
            }
        }

        public String ImageStr {
            get {
                if (ImageInfo != null)
                    return ImageInfo.Information();
                return "Picture Not Selected";
            }
            set { OnPropertyChanged(nameof(ImageStr)); }
        }
        public String MusicStr {
            get {
                if(MusicInfo!=null)
                    return MusicInfo.Information();
                return "Music Not Selected";
            }
            set { OnPropertyChanged(nameof(MusicStr)); }
        }
        */

        String _outputname;
        public String OutputName {
            get { return _outputname; }
            set {
                _outputname = value;
                OnPropertyChanged(nameof(OutputName));
            }
        }

        List<String> _avformats;
        public List<String> AvailableFormats {
            get { return _avformats; }
            set {
                _avformats = value;
                OnPropertyChanged(nameof(AvailableFormats));
            }
        }
        String _selectedfmt;
        public String SelectedFormat {
            get { return _selectedfmt; }
            set {
                _selectedfmt = value;
                OnPropertyChanged(nameof(SelectedFormat));
            }
        }

        public List<String> AvailableFunctions {
            get {
                return Models.FFMpegProcess.GetFunctions();
            }
        }

        String _selectedfunc;
        public String SelectedFunc {
            get { return _selectedfunc; }
            set {
                List<String> avf = new List<String>();
                switch (value)
                {
                    case "Img+Music=Video":
                        foreach (Models.VideoInfo.Formats x in Enum.GetValues(typeof(Models.VideoInfo.Formats)))
                            avf.Add("." + x);
                        break;
                    case "Video->Music":
                        foreach (Models.MusicInfo.Formats x in Enum.GetValues(typeof(Models.MusicInfo.Formats)))
                            avf.Add("." + x);
                        break;
                    case "Video->Gif":
                        avf.Add(".gif");
                        break;
                }
                AvailableFormats = avf;
                _selectedfunc = value;
                OnPropertyChanged(nameof(SelectedFunc));
            }
        }
 
        public ICommand OpenImage
        {
            get
            {
                return new Models.DelegateCommand((obj) =>
                {
                    String Image_Path = Models.FileHandler.OpenFile("Image");
                    if (Image_Path != "")
                    {
                        ImageInfo = new Models.ImageInfo(Image_Path);
                        //ImageStr += "";
                    }

                }, (obj) => { return (SelectedFunc == "Img+Music=Video"); });
            }
        }
        public ICommand OpenAudio
        {
            get
            {
                return new Models.DelegateCommand((obj) =>
                {
                    String Audio_Path = Models.FileHandler.OpenFile("Audio");
                    if (Audio_Path != "")
                    {
                        //timer.Stop();
                        MusicInfo = new Models.MusicInfo(Audio_Path);
                        /*
                        aud_duration = MusicInfo.duration;
                        aud_position = MusicInfo.position;
                        MusicStr += "";
                        S_PlayPause += "";
                        timer.Interval = 300;
                        timer.Elapsed += Timer_Elapsed;
                        timer.Start();
                        */
                    }
                }, (obj) =>
                {
                    return (SelectedFunc == "Img+Music=Video");
                });
            }
        }

        public ICommand OpenVideo
        {
            get
            {
                return new Models.DelegateCommand((obj) =>
                {
                    String VideoPath = Models.FileHandler.OpenFile("Video");
                    if (VideoPath != "") {
                        VideoInfo = new Models.VideoInfo(VideoPath);
                    }
                }, (obj) =>
                {
                    return (SelectedFunc == "Video->Music" || SelectedFunc == "Video->Gif");
                });
            }
        }
        /*
        public String S_PlayPause {
            get {
                if (MusicInfo != null && MusicInfo.isPlaying==NAudio.Wave.PlaybackState.Playing) return "Stop";
                else return "Play";
            }
            set { OnPropertyChanged(nameof(S_PlayPause)); }
        }
        public ICommand PlayPause {
            get {
                return new Models.DelegateCommand((obj) =>
                {
                    if (MusicInfo.isPlaying == NAudio.Wave.PlaybackState.Playing)
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
        }*/

        public ICommand Render {
            get {
                return new Models.DelegateCommand((obj) =>
                {

                    switch (SelectedFunc)
                    {
                        case "Img+Music=Video":
                            Models.FFMpegProcess.StaticImgAndMusicVid(ImageInfo.Path, MusicInfo.Path, OutputName, MusicInfo.duration, SelectedFormat);
                            break;
                        case "Video->Music":
                            Models.FFMpegProcess.AudioFromVideo(VideoInfo.Path, SelectedFormat);
                            break;
                        case "Video->Gif":
                            Models.FFMpegProcess.VideoToGif(VideoInfo.Path);
                            break;
                    }


                }, (obj)=> {
                    if (SelectedFunc == "Img+Music=Video")
                    {
                        if (MusicInfo != null && ImageInfo != null)
                            return MusicInfo.isValid && ImageInfo.isValid;
                    }
                    else if (VideoInfo!=null) return true;
                    return false;
                });
            }
        }
        /*
        public ICommand Slider_Down
        {
            get
            {
                return new Models.DelegateCommand((obj) =>
                {
                    MusicInfo.Pause();
                }, (obj)=> { return (MusicInfo.isPlaying == NAudio.Wave.PlaybackState.Playing); });
            }
        }
        public ICommand Slider_Up
        {
            get
            {
                return new Models.DelegateCommand((obj) =>
                {
                    MusicInfo.position = aud_position;
                    MusicInfo.Play();
                }, (obj)=> { return (MusicInfo.isPlaying==NAudio.Wave.PlaybackState.Paused); });
            }
        }
        */
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String info = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
