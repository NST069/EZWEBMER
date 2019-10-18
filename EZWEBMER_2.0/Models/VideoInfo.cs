using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZWEBMER_2._0.Models
{
    class VideoInfo : MediaInfo
    {

        public VideoInfo(String path) {
            Load(path);
        }

        public string Information()
        {
            throw new NotImplementedException();
        }

        public void Load(string path)
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public enum Formats {
            webm, mp4
        }
    }
}
