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
        private String _path;
        public String Path
        {
            get { return _path; }
            set { _path = value; }
        }
        public System.Media.SoundPlayer player;
        public bool isPlaying;
        public int duration;

        public MusicInfo(String path) {
            isValid = false;
            this.Path = path;
            afr = new AudioFileReader(path);
            player = new System.Media.SoundPlayer(path);
            player.Load();
            
            duration = (int)(afr.TotalTime.TotalSeconds);

            isValid = true;
        }

        public void Play() {

            player.Play();
            isPlaying = true;
        }

        public void Stop() {
            player.Stop();
            isPlaying = false;
        }

        public enum Formats {
            wav
        }
    }
}
