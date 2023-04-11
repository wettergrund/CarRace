namespace CarRace
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

            Car car1 = new Car("Volvo");
            Car car2 = new Car("BMW");
            //Car car3 = new Car("Toyota");

            List<Car> carList = new List<Car>
            {
                car1,
                car2

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

            //var raceStatus = RaceStatus(carList);
            //var status = RaceStatus(carList);
            var keyListener = KeyEvent(carList);

            var carRace = new List<Task> { firstCar, secondCar, keyListener };

            int carsFinished = 0;

            while (carRace.Count > 0)
            {
                Console.WriteLine("Test");
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


                //else if (finishRace == thirdCar)
                //{
                //    string position = carsFinished == 0 ? "came first" : "finished";


                //    var timeSpan = TimeSpan.FromSeconds(car3.TotalTime);
                //    int mm = timeSpan.Minutes;
                //    int ss = timeSpan.Seconds;
                //    carsFinished++;

                //    Console.WriteLine($"{car3.CarName} {position} in time: {mm}:{ss} minutes");
                //}
                else if (finishRace == keyListener)
                {

                    ClearCurrentConsoleLine();

                    Console.WriteLine("Alla bilar i mål");
                }
                Console.ResetColor();

                await finishRace;
                carRace.Remove(finishRace);
            }
            Console.WriteLine("Test");
            Console.ReadLine();


        }


        public async static Task RaceStatus(List<Car> cars)
        {
            while (true)
            {




                var distanceLeft = cars.Select(car => car.DitanceLeft()).Sum();

                if (distanceLeft <= 0)
                {
                    return;
                }

                Console.Clear();
                Console.SetCursorPosition(0, 20);

                foreach (Car car in cars)
                {
                    //Console.ForegroundColor = car.Color;
                    ////decimal displayDistance = Math.Round(car.TraveledDistance, 2);
                    //Console.WriteLine($"{car.CarName} distance: {car.DisplayDistance}km CurrentSpeed/MaxSpeed:{car.CurrentSpeed}/{car.MaxSpeed}km/h Last event: {car.LastEvent}");
                    //Console.WriteLine();
                    //Console.CursorLeft = 0;
                    ////for (int i = 0; i < 20; i++)
                    ////{
                    ////    Console.ForegroundColor = ConsoleColor.Gray;
                    ////    Console.Write(".");

                    ////}
                    //Console.ForegroundColor = car.Color;
                    //Console.CursorLeft = 0;
                    decimal distance = 0;
                    int carName = car.CarName.Length;
                    int roadLength = 10;
                    Console.Write(car.CarName + " ");


                    for (int i = 0; i < roadLength; i++)
                    {
                        if (i >= carName - 4)
                        {
                            Console.Write("----");
                        }




                    }
                    Console.WriteLine();
                    for (int i = 0; i < roadLength; i++)
                    {
                        distance = (car.DisplayDistance * 100) * (roadLength / 10);

                        if (distance > (i * 100) && distance < (i + 1) * 100)
                        {
                            Console.Write("-🔴- ");
                        }
                        else
                        {
                            Console.Write("- - ");
                        }


                    }
                    Console.Write("🏁");

                    Console.WriteLine();
                    for (int i = 0; i < roadLength; i++)
                    {
                        if (distance > i * 100 && distance < (i + 1) * 100)
                        {
                            Console.Write($"{car.DisplayDistance} ");
                        }
                        else
                        {
                            Console.Write("----");

                        }

                    }


                    Console.WriteLine();
                }
                await Task.Delay(1000);



            }
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
                await RaceStatusOnce(cars);
                Console.SetCursorPosition(position.Left, position.Top);
            }


            //while (true)
            //{

            //    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            //    {
            //        Console.Clear();

            //        RaceStatusOnce(cars);
            //    }



            //}
        }

        public async static Task RaceStatusOnce(List<Car> cars)
        {
            Console.SetCursorPosition(0, 10);

            foreach (Car car in cars)
            {
                //Console.ForegroundColor = car.Color;
                //decimal displayDistance = Math.Round(car.TraveledDistance, 2);

                ClearCurrentConsoleLine();
                Console.WriteLine($"{car.CarName} distance: {car.DisplayDistance}km CurrentSpeed/MaxSpeed:{car.CurrentSpeed}/{car.MaxSpeed}km/h ");
                if (car.CurrentSpeed < car.MaxSpeed)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                ClearCurrentConsoleLine();

                Console.WriteLine($"CurrentSpeed: {car.CurrentSpeed}km/h");
                Console.ResetColor();
                ClearCurrentConsoleLine();

                Console.WriteLine($"Maxspeed:{car.MaxSpeed}/kmh");
                ClearCurrentConsoleLine();

                Console.WriteLine($"Last event: {car.LastEvent}");
                Console.WriteLine();
                Console.CursorLeft = 0;


            }
            Console.SetCursorPosition(0, 0);


        }


        public async static Task<Car> DriveRace(Car car)
        {
            car.CurrentSpeed = car.MaxSpeed;
            Console.WriteLine($"{car.CarName} has started");

            int timeLapsed = 0;
            //double totalTime = 0;
            int timeFactor = 10;
            //int maxSpeed = 120;
            //decimal currentSpeed = maxSpeed;

            int rows = 0;

            while (true)
            {

                decimal distancePerSecond = car.CurrentSpeed / 3600; //km/h to km/s

                //Move out to status method

                //Console.SetCursorPosition(0, 10);
                //Console.CursorLeft = 0; 
                //Console.Write(car.DisplayDistance);

                await Task.Delay(500 / 10);
                if (car.CurrentSpeed != 0)
                {

                    car.CurrentSpeed = car.MaxSpeed;
                }

                timeLapsed++;
                //totalTime++;
                car.TotalTime++;

                if (car.TraveledDistance >= 10)
                {
                    // When car reach 10km

                    return car;
                }

                car.TraveledDistance += distancePerSecond;







                if (timeLapsed == 30)
                {

                    timeLapsed = 0;

                    //Event.RandomEvent(car);

                    Random rand = new Random();
                    int randomNumber = rand.Next(49);



                    switch (randomNumber)
                    {
                        case 0:
                            //Run out of fuel 30sek (2%)
                            string newEvent = "ran out of fuel";
                            await Event.TimeOut(car, newEvent, 30);
                            car.LastEvent = newEvent;

                            break;
                        case int n when (n <= 1 && n <= 2):
                            //Flat tire (4%)
                            await Event.TimeOut(car, "got flat tire", 20);
                            car.LastEvent = "Flat tire";


                            break;
                        case int n when (n <= 3 && n <= 7):
                            //Bird strike (10%)
                            await Event.TimeOut(car, "got bird strike", 10);
                            car.LastEvent = "Bird strike";

                            break;
                        case int n when (n <= 8 && n <= 17):
                            //Enginge problem (20%)
                            car.MaxSpeed--;
                            //Console.WriteLine($"{car.CarName} have engine problem. New speed: {car.MaxSpeed}");
                            car.LastEvent = $"Engine problem, slowd down to {car.MaxSpeed}";
                            break;
                        default:
                            //Console.WriteLine("Inget hände");
                            car.LastEvent = "No event";
                            //Event.TimeOut(car, car.LastEvent, 0);

                            break;
                    }


                }

            }

        }


        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        public static void Random()
        {
            double turns = 1e6;

            double a = 0; //2%
            double b = 0; //4%
            double c = 0; //10%
            double d = 0; //20%





            for (int i = 0; i < turns; i++)
            {


                Random rand = new Random();
                int randomNumber = rand.Next(49);

                if (randomNumber == 0)
                {
                    //Console.WriteLine("Tanka"); // 2%
                    a++;
                }
                if (1 <= randomNumber && randomNumber <= 2)
                {
                    //Console.WriteLine("Nya däck"); // 4%
                    b++;

                }
                if (3 <= randomNumber && randomNumber <= 7)
                {

                    //Console.WriteLine("Fågel!"); // 10%
                    c++;
                }
                if (8 <= randomNumber && randomNumber <= 17)
                {
                    //Console.WriteLine("Motorproblem"); //20%
                    d++;
                }
            }

            Console.WriteLine($"{(a / turns) * 100}: 2%?");
            Console.WriteLine($"{(b / turns) * 100}: 4%?");
            Console.WriteLine($"{(c / turns) * 100}: 10%?");
            Console.WriteLine($"{(d / turns) * 100}: 20%?");

            /*
             0 : + 30 sek tanka
             1-2: + 20 sek nya däck
             3-7: + 10 sek fågel
             8-18: -1km/h 
             
             */

        }
    }

}