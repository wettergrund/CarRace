using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    internal class Car
    {
        public string CarName { get; set; }
        public ConsoleColor Color { get; set; }
        public int MaxSpeed { get; set; } = 120;

        public decimal CurrentSpeed { get; set; }

        public double TotalTime { get; set; }

        public decimal TraveledDistance { get; set; } = 0;

        public decimal DisplayDistance 
        { 
            get 
            { 
                return Math.Round(TraveledDistance, 2); 
            } 
        }


        public string LastEvent { get; set; } = "No event";

        public Car(string name, ConsoleColor color = ConsoleColor.White)
        {
            CarName = name;

            Color = color;
        }

                
        public decimal DitanceLeft()
        {
            decimal result = 10 - TraveledDistance; 
        
            return result;
        }
    }
}
