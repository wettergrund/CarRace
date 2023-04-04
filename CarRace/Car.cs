using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    internal class Car
    {
        int warp = 10; // Spee factor 

        public string CarName { get; set; }
        private int MaxSpeed { get; set; } = 120;

        public decimal CurrentSpeed { get; set; }

        public ConsoleColor Color { get; set; }


        public decimal TraveledDistance { get; set; } = 0;

        public Car(string name, ConsoleColor color = ConsoleColor.White){
            CarName = name;
            
            Color = color;
        }

        //Events
        public async Task RunOutOfFuel()
        {
            decimal temp = CurrentSpeed;    

            Console.WriteLine($"{CarName} Out of fuel +30sek");
            CurrentSpeed = 0;
            await Task.Delay(30000 / warp);

            CurrentSpeed = temp;
           
        }

        public async Task FlatTire()
        {
            decimal temp = CurrentSpeed;    

            CurrentSpeed = 0;

            Console.WriteLine($"{CarName} Flat tire +20sek");
            await Task.Delay(20000 / warp);
            CurrentSpeed = temp;


        }

        public async Task BirdStrike()
        {
            decimal temp = CurrentSpeed;

            CurrentSpeed = 0;

            Console.WriteLine($"Bird strike! {CarName} + 10sek");
                await Task.Delay(10000 / warp);
            CurrentSpeed = temp;


        }

        public async Task RoughEngine()
        {


                MaxSpeed--;
                Console.WriteLine($"{CarName} Rough engine, new speed: {MaxSpeed}");
            
        }

        public void TimeOut(int Time)
        {
            decimal temp = CurrentSpeed;

            CurrentSpeed = 0;
            Task.Delay(Time / warp);
            CurrentSpeed = temp;

        }



        public void Drive()
        {
            int timeLapsed = 0;
            int totalTime = 0;

            
            
            

            bool running = true;

            while (running)
            {
                Console.CursorTop = 0;

                // km/h
                if(CurrentSpeed != 0)
                {

                    CurrentSpeed = MaxSpeed;
                }
                decimal distancePerSecond = CurrentSpeed / 3600;
                decimal displayDistance = Math.Round(TraveledDistance, 5);

                Console.WriteLine($"Speed:{CurrentSpeed} Distance: {displayDistance}km");
                Thread.Sleep(1000 / warp);


                //Console.BackgroundColor = Color;
                //30sec?
                if(timeLapsed == 30)
                {
                    Console.CursorTop = 5;

                    timeLapsed = 0;
                    Console.WriteLine("30 sek");
                    Console.WriteLine("Distance:" + displayDistance + " km? " + CurrentSpeed);
                    //Console.ReadLine();

                    //Event
                    RandomEvent();


                }

                timeLapsed++;
                totalTime++;

                TraveledDistance += distancePerSecond;





            }


        }

        public void Start()
        {
            CurrentSpeed = MaxSpeed;
            Console.WriteLine($"{CarName} started");
            Drive();
        }


        public async Task RandomEvent()
        {

            Random rand = new Random();
            int randomNumber = rand.Next(49);

            if (randomNumber == 0)
            {
                //Console.WriteLine("Tanka"); // 2%

                await RunOutOfFuel();
            }
            if (1 <= randomNumber && randomNumber <= 2)
            {
                //Console.WriteLine("Nya däck"); // 4%
                await FlatTire();

            }
            if (3 <= randomNumber && randomNumber <= 7)
            {

                //Console.WriteLine("Fågel!"); // 10%
                await BirdStrike();
            }
            if (8 <= randomNumber && randomNumber <= 17)
            {
                //Console.WriteLine("Motorproblem"); //20%
                await RoughEngine();
            }


        }
    }
}
