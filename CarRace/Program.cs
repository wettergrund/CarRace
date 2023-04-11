﻿namespace CarRace
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            await RunRace();

        }



        public async static Task RunRace()
        {

            Car car1 = new Car("Volvo", ConsoleColor.Magenta);
            Car car2 = new Car("BMW", ConsoleColor.Cyan);

            List<Car> carList = new List<Car>
            {
                car1,
                car2,

            };

            for (int i = 0; i < carList.Count; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                else
                {

                    carList[i].displayPos = i * 20;
                }
            }


            var firstCar = DriveRace(car1);
            var secondCar = DriveRace(car2);

            Console.WriteLine("Press Enter to get a status update");
            Console.WriteLine("Race events:");

            var keyListener = KeyEvent(carList);

            var carRace = new List<Task> { firstCar, secondCar, keyListener };

            int carsFinished = 0;

            while (carRace.Count > 0)
            {
                Task finishRace = await Task.WhenAny(carRace);

                string position = carsFinished == 0 ? "came first" : "finished";

                if (carsFinished == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                if (finishRace == firstCar)
                {

                    var timeSpan = TimeSpan.FromSeconds(car1.TotalTime);
                    int mm = timeSpan.Minutes;
                    int ss = timeSpan.Seconds;
                    carsFinished++;

                    ClearCurrentConsoleLine();

                    Console.WriteLine($"{car1.CarName} {position} in time: {mm}:{ss} minutes");
                }
                else if (finishRace == secondCar)
                {

                    var timeSpan = TimeSpan.FromSeconds(car2.TotalTime);
                    int mm = timeSpan.Minutes;
                    int ss = timeSpan.Seconds;
                    carsFinished++;

                    ClearCurrentConsoleLine();

                    Console.WriteLine($"{car2.CarName} {position} in time: {mm}:{ss} minutes");
                }

                else if (finishRace == keyListener)
                {

                    ClearCurrentConsoleLine();

                    Console.WriteLine("Alla bilar i mål");
                }
                Console.ResetColor();

                await finishRace;
                carRace.Remove(finishRace);
            }
            Console.ReadLine();


        }


        public async static Task KeyEvent(List<Car> cars)
        {
            while (true)
            {
                //Do-while loop to wait for Enter key to be pressed

                do
                {

                    while (!Console.KeyAvailable)
                    {
                        //While no key press is available


                        // Check if all cars are finished. Break outer while loop.
                        var distanceLeft = cars.Select(car => car.DitanceLeft()).Sum();

                        if (distanceLeft <= 0)
                        {
                            await Task.Delay(1000);
                            return;
                        }
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Enter);

                // Code if enter is pressed
                (int Left, int Top) position = Console.GetCursorPosition();

                await RaceStatus(cars);

                Console.SetCursorPosition(position.Left, position.Top);
            }

        }

        public async static Task RaceStatus(List<Car> cars)
        {
            Console.SetCursorPosition(0, 10);

            foreach (Car car in cars)
            {
                Console.ForegroundColor = car.Color;

                ClearCurrentConsoleLine();
                Console.WriteLine($"Distance: {car.DisplayDistance} km");


                if (car.CurrentSpeed < car.MaxSpeed)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                ClearCurrentConsoleLine();
                Console.WriteLine($"CurrentSpeed: {car.CurrentSpeed}km/h");
                Console.ForegroundColor = car.Color;

                ClearCurrentConsoleLine();
                Console.WriteLine($"Maxspeed:{car.MaxSpeed}/kmh");
                
                ClearCurrentConsoleLine();
                Console.WriteLine($"Last event: {car.LastEvent}");
                Console.WriteLine();
                
                Console.CursorLeft = 0;
            }

            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

        }


        public async static Task<Car> DriveRace(Car car)
        {
            car.CurrentSpeed = car.MaxSpeed;
            Console.WriteLine($"{car.CarName} has started");

            int timeLapsed = 0;

            while (true)
            {
                await Task.Delay(1000 / 10);

                decimal distancePerSecond = car.CurrentSpeed / 3600; //km/h to km/s


                if (car.CurrentSpeed != 0)
                {
                    car.CurrentSpeed = car.MaxSpeed;
                }

                timeLapsed++;
                car.TotalTime++;

                if (car.TraveledDistance >= 10)
                {
                    // When car reach 10km
                    return car;
                }

                //Add distance per second to traveled distance
                car.TraveledDistance += distancePerSecond;


                //Generate an event every 30 seconds
                if (timeLapsed == 30)
                {
                    timeLapsed = 0;

                    Random rand = new Random();
                    int randomNumber = rand.Next(49);


                    switch (randomNumber)
                    {
                        case 0:
                            //Run out of fuel 30sek (Risk: 2%)

                            string newEvent = "ran out of fuel";
                            await Event.TimeOut(car, newEvent, 30);
                            car.LastEvent = newEvent;

                            break;
                        case int n when (n <= 1 && n <= 2):
                            //Flat tire (Risk: 4%)

                            await Event.TimeOut(car, "got flat tire", 20);
                            car.LastEvent = "Flat tire";


                            break;
                        case int n when (n <= 3 && n <= 7):
                            //Bird strike (Risk: 10%)

                            await Event.TimeOut(car, "got bird strike", 10);
                            car.LastEvent = "Bird strike";

                            break;
                        case int n when (n <= 8 && n <= 17):
                            //Enginge problem (Risk: 20%)
                            car.MaxSpeed--;
                            car.LastEvent = $"Engine problem, slowed down to {car.MaxSpeed}";
                            break;
                        default:
                            // No problemfor the 30 sec period. (Chance: 64%)
                            car.LastEvent = "No event";
                            break;
                    }

                }

            }

        }


        public static void ClearCurrentConsoleLine()
        {
            //A method to clear current line (without console clear).
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        
    }

}