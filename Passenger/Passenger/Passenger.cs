using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Passenger
{
    class Passenger
    {
        private readonly int rand, Min = 30000, Max = 999999;

        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public int SECURITY_NUM { get; set; }
        public int SEAT_NUM { get; set; }
        public string FLIGHT_NUMBER { get; set; }
        public string DEPARTURE_TIME { get; set; }
        public string GATE { get; set; }


        public Passenger(string first, string last, string flightNum, string departureTime, string gate)
        {
            FIRST_NAME = shorten(first);
            LAST_NAME = shorten(last);
            SECURITY_NUM = Rand_Security_Num(Min, Max);
            FLIGHT_NUMBER = flightNum;
            DEPARTURE_TIME = departureTime;
            GATE = gate;
        }

        //Random number generator for security number
        private int Rand_Security_Num(int min, int max)
        {
            int Rand;
            Random ran = new Random();
            Rand = ran.Next(min, max);
            return Rand;
        }

        //Truncates string to 5 characters
        private string shorten(string name)
        {
            const int name_maxlength = 5;
            string Name;

            Name = name;

            if (Name.Length > name_maxlength)
            {
                Name = Name.Substring(0, name_maxlength);
            }

            return Name;
        }

        //Diplays the boarding pass
        public void Display_Boarding_Pass()
        {

            WriteLine("-------------------------------------------------------------");
            WriteLine("Flight No.: {0,-8} Departure Time: {1,-8} Gate: {2}", FLIGHT_NUMBER, DEPARTURE_TIME, GATE);
            WriteLine("-------------------------------------------------------------");
            WriteLine("Name: {0,-5} {1,-8} Security Number: {2}", FIRST_NAME, LAST_NAME, SECURITY_NUM);
            WriteLine("Seat Number: {0}", SEAT_NUM);
            WriteLine("*************************************************************\n");

        }

        //Displays the list of passengers in tabular format
        public void List_All_Passengers()
        {
            WriteLine("----------------------------------------------------------------");
            WriteLine("|    {0,-5} {1,-8} |         {2,-12}  |         {3} ", FIRST_NAME, LAST_NAME, SECURITY_NUM, SEAT_NUM);

        }

        //Shows all seats availability
        public void Show_Available_Seats(string[] seats)
        {
            for (int i = 0; i < seats.Length; ++i)
            {
                WriteLine("Seat {0}: {1}", i + 1, seats[i]);
            }

        }

    }
}
