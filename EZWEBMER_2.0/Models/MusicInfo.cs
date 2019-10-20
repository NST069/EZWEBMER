using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZWEBMER_2._0.Models
{
    class MusicInfo : MediaInfo
    {
        public bool isValid;
        private WaveOutEvent outputDevice;
        private AudioFileReader afr;
        public String Path { get; set; }
        public PlaybackState isPlaying
        {
            get
            {
                if (outputDevice != null)
                    return outputDevice.PlaybackState;
                return PlaybackState.Stopped;
            }
        }

        public int duration { get; set; }
        public int position
        {
            get
            {
                if (outputDevice != null)
                    return (int)afr.CurrentTime.TotalSeconds;
                return 0;
            }
            set
            {
                if (outputDevice != null)
                {
                    afr.CurrentTime = TimeSpan.FromSeconds(value);
                }

            }
        }

        public MusicInfo(String path) {
            isValid = false;
            Load(path);            
            isValid = true;
        }

        public override void Load(String path) {
            this.Path = path;
            afr = new AudioFileReader(path);
            duration = (int)afr.TotalTime.TotalSeconds;

        }
        public override void Play()
        {
            Task.Factory.StartNew(() =>
            {
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                    outputDevice.Init(afr);
                }
                if (outputDevice.PlaybackState != PlaybackState.Playing)
                {
                    if (outputDevice.PlaybackState == PlaybackState.Stopped)
                    {
                        outputDevice.Init(afr);
                    }
                    outputDevice.Play();
                }
            });
            
        }

        public override String Information() {
            return "[" + (isValid ? "Valid" : "Invalid") + "]" + Path + " " + (isPlaying.ToString());
        }

        public void Stop(){
            outputDevice?.Stop();
        }
        public void Pause() {
            outputDevice?.Pause();
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
