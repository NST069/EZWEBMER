using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZWEBMER_2._0.Models
{
    abstract class MediaInfo
    {
        public abstract void Load(String Path);
        public abstract void Play();
        public virtual String Information()
        {
            return "Empty Media File";
        }
    }
}