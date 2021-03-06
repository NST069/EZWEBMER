﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZWEBMER_2._0.Models
{
    static class FFMpegProcess
    {
        public static void Start(ImageInfo p, MusicInfo m, String s) {
            if (p!=null && m!=null)
            {
                String folder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + @"\ffmpeg\";
                String cmdtext = "/k cd " + folder + " & " +
                    "ffmpeg -loop 1 -r 1 " +
                    "-i \"" + p.Path + "\" -i \"" + m.Path +
                    "\" -t " + m.duration +
                    " -b:v 0 -crf 2 -b:a 160K -shortest -g 9999 " +
                    "-pix_fmt yuv420p -speed 0 -deadline 0 -threads 4 " +
                    s;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = cmdtext;
                process.StartInfo = startInfo;
                process.Start();
            }
        }
    }
}
