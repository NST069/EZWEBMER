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
        private WaveOutEvent outputDevice;
        private AudioFileReader afr;
        public String Path { get; set; }
        public bool isPlaying
        {
            get
            {
                if (outputDevice!=null)
                    return (outputDevice.PlaybackState == PlaybackState.Playing);
                return false;
            }
        }
        public int duration { get; set; }

        public MusicInfo(String path) {
            isValid = false;
            this.Path = path;
            afr = new AudioFileReader(path);
            
            duration = (int)(afr.TotalTime.TotalSeconds);

            isValid = true;
        }

        public void Play()
        {
            Task.Factory.StartNew(() =>
            {
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                    outputDevice.Init(afr);
                }
                outputDevice.Play();
            });
            
        }

        public void Stop() {
            Task.Factory.StartNew(() =>
            {
                outputDevice?.Stop();
            });
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outputDevice.Dispose();
            outputDevice = null;
        }

        public enum Formats {
            wav, mp3
        }
    }
}
