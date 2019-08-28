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
        public PlaybackState isPlaying
        {
            get
            {
                if (outputDevice != null)
                    return outputDevice.PlaybackState;
                return PlaybackState.Stopped;
            }
        }
        public int duration
        {
            get
            {
                if (outputDevice != null)
                    return (int)afr.TotalTime.TotalSeconds;
                return 0;
            }
        }
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
            this.Path = path;
            afr = new AudioFileReader(path);
            
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
