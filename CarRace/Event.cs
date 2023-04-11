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
            int timeFactor = 10;

            car.CurrentSpeed = 0;

            var timeSpan = TimeSpan.FromSeconds(car.TotalTime);
            int mm = timeSpan.Minutes;
            int ss = timeSpan.Seconds;


            Console.WriteLine($"{car.CarName} {eventDesc} at {mm}:{ss} minutes");
            await Task.Delay((Time * 1000) / timeFactor);
            car.CurrentSpeed = temp;

        }
    }
}
