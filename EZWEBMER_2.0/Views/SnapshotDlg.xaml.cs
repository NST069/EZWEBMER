using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EZWEBMER_2._0.Views
{
    /// <summary>
    /// Interaction logic for SnapshotDlg.xaml
    /// </summary>
    /// 
    public interface IMediaService
    {
        void Load(String Path);
        void Play();
        void Pause();
        void Stop();
        void SkipTo(int ss);
        int GetPosition();
    }
    public partial class SnapshotDlg : Window, IMediaService
    {
        

        public SnapshotDlg()
        {
            InitializeComponent();
        }
        void IMediaService.Load(String Path) {
            this.MediaPlayer.BeginInit();
            this.MediaPlayer.Source = new Uri(Path);
            this.MediaPlayer.EndInit();
            if (!this.MediaPlayer.IsLoaded) System.Windows.MessageBox.Show("Error loading "+this.MediaPlayer.Source);
            this.MediaPlayer.Play();
            this.MediaPlayer.Pause();
        }
        void IMediaService.Pause()
        {
            this.MediaPlayer.Pause();
        }

        void IMediaService.Play()
        {
            this.MediaPlayer.Play();
        }

        void IMediaService.Stop()
        {
            this.MediaPlayer.Stop();
        }

        void IMediaService.SkipTo(int ss) {
            this.MediaPlayer.Position = TimeSpan.FromSeconds(ss);
            this.MediaPlayer.Play();
            this.MediaPlayer.Pause();
        }

        int IMediaService.GetPosition() {            
            return (int)this.MediaPlayer.Position.TotalSeconds;        
        }
    }
}
