using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    internal class Car
    {
        int timeFactor = 10;  // Time factor 

        public string CarName { get; set; }
        private int MaxSpeed { get; set; } = 120;

        public decimal CurrentSpeed { get; set; }

        public ConsoleColor Color { get; set; }

        public int displayPos { get; set; } 


        public decimal TraveledDistance { get; set; } = 0;

        public Car(string name, ConsoleColor color = ConsoleColor.White, int displayPos = 0)
        {
            CarName = name;

            Color = color;
            this.displayPos = displayPos;
        }

        //Events
        public async Task RunOutOfFuel()
        {

            await TimeOut(30, "Out of fuel +30sek");


        }

        public async Task FlatTire()
        {
            await TimeOut(20, "Flat tire +20sek");
        }

        public async Task BirdStrike()
        {
            await TimeOut(10, "Bird strike, +10 sek");

        }

        public async Task RoughEngine()
        {


                MaxSpeed--;
                Console.WriteLine($"{CarName} Rough engine, new speed: {MaxSpeed}");
            
        }

        public async Task TimeOut(int Time, string eventDesc)
        {
            decimal temp = CurrentSpeed;

            CurrentSpeed = 0;
            Console.WriteLine($"{CarName} {eventDesc}");
            await Task.Delay((Time * 1000) / timeFactor);
            CurrentSpeed = temp;

        }



        public async Task Drive()
        {
            int timeLapsed = 0;
            double totalTime = 0;
            
            
            

            bool running = true;
            //Console.WriteLine("Hej " + CarName );
            while (running)
            {


                // km/h
                if (CurrentSpeed != 0)
                {

                    CurrentSpeed = MaxSpeed;
                }
                decimal distancePerSecond = CurrentSpeed / 3600;
                decimal displayDistance = Math.Round(TraveledDistance, 5);

                //Console.WriteLine($"{CarName} Speed:{CurrentSpeed} Distance: {displayDistance}km");
                Thread.Sleep(1000 / timeFactor);


                //if(totalTime == 0)
                //{
                //    RandomEvent();
                //}

                //30sec?
                if (timeLapsed == 30)
                {
                    Console.Clear();
                    


                    timeLapsed = 0;
                    //await Text(CarName + " Distance:" + displayDistance + " km? " + CurrentSpeed + " ");
                    //Console.ForegroundColor = Color;
                    //Console.SetCursorPosition(0, 0 + displayPos);
                    //Console.WriteLine(CarName + " Distance:" + displayDistance + " km? " + CurrentSpeed + " ");
                    //Console.ReadLine();

                    //Event
                    //Console.CursorTop = -1;
                    //Console.CursorLeft = 30;
                    RandomEvent();


                }

                timeLapsed++;
                totalTime++;

                TraveledDistance += distancePerSecond;


                if(TraveledDistance >= 10)
                {
                    var timeSpan = TimeSpan.FromSeconds(totalTime);
                    int hh = timeSpan.Hours;
                    int mm = timeSpan.Minutes;
                    int ss = timeSpan.Seconds;

                    //Console.WriteLine($"Finish in time: {Math.Round((totalTime / 60),2)} minutes");
                    await Text($"Finish in time: {mm}:{ss} minutes");
                    //Console.WriteLine($"Finish in time: {mm}:{ss} minutes");
                    running = false;

                }
                //Console.SetCursorPosition(0, 0);


            }


        }

        public async Task Text(string text)
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(0, 0 + displayPos);
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public async Task Start()
        {
            CurrentSpeed = MaxSpeed;
            Console.WriteLine($"{CarName} started");
            Drive();
        }


        public async Task RandomEvent()
        {
            Console.SetCursorPosition(0, 0 + displayPos);

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
