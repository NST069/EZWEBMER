using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZWEBMER_2._0.Models
{
    class MusicInfo
    {
        public bool isValid;
        private AudioFileReader afr;
        public String Path { get; set; }
        public System.Media.SoundPlayer player;
        public bool isPlaying { get; set; }
        public int duration { get; set; }

        public MusicInfo(String path) {
            isValid = false;
            this.Path = path;
            afr = new AudioFileReader(path);
            player = new System.Media.SoundPlayer(path);
            player.Load();
            
            duration = (int)(afr.TotalTime.TotalSeconds);
            isPlaying = false;

            isValid = true;
        }

        public void Play()
        {
            isPlaying = true;
            Task.Factory.StartNew(() =>
            {
                player.Play();
            });
            
        }

        public void Stop() {
            Task.Factory.StartNew(() =>
            {
                player.Stop();
            });
            isPlaying = false;
        }

        public enum Formats {
            wav
        }
    }
}
