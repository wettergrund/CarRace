using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    internal class Tools
    {

        public static async Task EventHandler(Car car, string eventDesc, int Time = 0)
        {
            
            decimal temp = car.CurrentSpeed;
            int timeFactor = 10;

            

            car.CurrentSpeed = 0;

            var time = SecondsToMinutes(car.TotalTime);

            Program.ClearCurrentConsoleLine();


            Console.WriteLine($"{car.CarName} {eventDesc} at {time.min}:{time.sec} minutes");
            
            await Task.Delay((Time * 1000) / timeFactor);
            
            
            car.CurrentSpeed = temp;

        }

        public static (int min, int sec) SecondsToMinutes(double seconds)
        {

            var timeSpan = TimeSpan.FromSeconds(seconds);
            int mm = timeSpan.Minutes;
            int ss = timeSpan.Seconds;

            return (mm, ss);
        }
    
    }
}
