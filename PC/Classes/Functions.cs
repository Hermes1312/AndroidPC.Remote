using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.VisualBasic.Devices;
using Packets;

namespace PC
{
    public static class Functions
    {
        public static void Shutdown()
        {
            new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C shutdown -s",
                    CreateNoWindow = true
                }
            }.Start();
        }

        public static void SetVolume(int direction)
        {
            switch (direction)
            {
                case -1:
                    VolumeChanger.VolumeDown();
                    break;

                case 0:
                    VolumeChanger.Mute();
                    break;

                case 1:
                    VolumeChanger.VolumeUp();
                    break;
            }
        }

        public static void SetTimer(int time)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > time)
                {
                    Shutdown();
                    break;
                }
            }
        }

        public static void PausePlay() => Mouse.ClickOnCenter();    
    }
}
