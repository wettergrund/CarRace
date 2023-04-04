namespace CarRace
{
    internal class Program
    {
        static void Main(string[] args)
        {

            RunRace();

            

        }



        static void RunRace()
        {

            Car car1 = new Car("Volvo");

            car1.Start();

            //Random();


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

            Console.WriteLine($"{(a / turns) * 100 }: 2%?");
            Console.WriteLine($"{(b / turns) * 100 }: 4%?");
            Console.WriteLine($"{(c / turns) * 100 }: 10%?");
            Console.WriteLine($"{(d / turns) * 100 }: 20%?");

            /*
             0 : + 30 sek tanka
             1-2: + 20 sek nya däck
             3-7: + 10 sek fågel
             8-18: -1km/h 
             
             */

        }
    }
}