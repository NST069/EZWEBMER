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
        async void IMediaService.Load(String Path) {
            this.MediaPlayer.BeginInit();
            bool x = await this.MediaPlayer.Open(new Uri(Path));
            if(!x) System.Windows.MessageBox.Show("Error "+ this.MediaPlayer.MediaInfo.MediaSource);
            this.MediaPlayer.EndInit();
        }
        async void IMediaService.Pause()
        {
            await this.MediaPlayer.Pause();
        }

        async void IMediaService.Play()
        {
            await this.MediaPlayer.Play();
        }

        async void IMediaService.Stop()
        {
            await this.MediaPlayer.Stop();
        }

        void IMediaService.SkipTo(int ss) {
            this.MediaPlayer.Position = TimeSpan.FromSeconds(ss);
        }

        int IMediaService.GetPosition() {            
            return (int)this.MediaPlayer.Position.TotalSeconds;        
        }
    }
}
