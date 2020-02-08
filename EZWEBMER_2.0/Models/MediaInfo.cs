using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZWEBMER_2._0.Models
{
    interface MediaInfo
    {
        void Load(String Path);
        void Play();
        String Information();
    }
}
