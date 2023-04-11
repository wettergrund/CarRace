using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    internal class Event
    {
        
        public static async Task TimeOut(Car car, string eventDesc, int Time)
        {
            decimal temp = car.CurrentSpeed;
            int timeFactor = 1;

            car.CurrentSpeed = 0;

            var time = SecondsToMinutes(car.TotalTime);

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
