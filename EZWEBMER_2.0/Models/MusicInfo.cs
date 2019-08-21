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
        private AudioFileReader afr;
        public System.Media.SoundPlayer player;
        public bool isPlaying;
        public int duration;

        public MusicInfo(String path) {
            afr = new AudioFileReader(path);
            player = new System.Media.SoundPlayer(path);
            player.Load();
            
            duration = (int)(new AudioFileReader(path).TotalTime.TotalSeconds);
        }

        public void Play() {

            player.Play();
            isPlaying = true;
        }

        public void Stop() {
            player.Stop();
            isPlaying = false;
        }
    }
}
