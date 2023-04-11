using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    internal class Event
    {
        //public static async Task RandomEvent(Car car)
        //{
        //    Console.SetCursorPosition(0, 0 + displayPos);

        //    Random rand = new Random();
        //    int randomNumber = rand.Next(49);

        //    if (randomNumber == 0)
        //    {
        //        //Console.WriteLine("Tanka"); // 2%

        //        await RunOutOfFuel();
        //    }
        //    if (1 <= randomNumber && randomNumber <= 2)
        //    {
        //        //Console.WriteLine("Nya däck"); // 4%
        //        await FlatTire();

        //    }
        //    if (3 <= randomNumber && randomNumber <= 7)
        //    {

        //        //Console.WriteLine("Fågel!"); // 10%
        //        await BirdStrike();
        //    }
        //    if (8 <= randomNumber && randomNumber <= 17)
        //    {
        //        //Console.WriteLine("Motorproblem"); //20%
        //        await RoughEngine();
        //    }


        //}

        //public async Task RunOutOfFuel()
        //{

        //    await TimeOut(30, "Out of fuel +30sek");


        //}

        //public async Task FlatTire()
        //{
        //    await TimeOut(20, "Flat tire +20sek");
        //}

        //public async Task BirdStrike()
        //{
        //    await TimeOut(10, "Bird strike, +10 sek");

        //}

        //public async Task RoughEngine()
        //{


        //    MaxSpeed--;
        //    Console.WriteLine($"{car.CarName} Rough engine, new speed: {MaxSpeed}");

        //}

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
