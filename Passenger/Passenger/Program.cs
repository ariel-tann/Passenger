using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Passenger
{
    class Program
    {
        static void Main(string[] args)
        {

            const int TOTAL_PASSENGERS = 40;
            const string taken = "TAKEN", Flight_Num = "EH669", Departure_Time = "1700h", Gate = "49C";
            int P_ID = 0, Seat_Number, entries = 0;
            string First_Name = "", Last_Name = "", Passenger_Continue, Seats = "";
            bool CHECK, Airline_Assistant = true;

            Passenger[] passengers = new Passenger[TOTAL_PASSENGERS];
            string[] seats = new string[TOTAL_PASSENGERS];
            Create_Available_Seats(seats);

            WriteLine("\nWelcome to the Airline Check-in system! ");
            WriteLine("------------------------------------------");

            do
            {
                WriteLine("Press Y if you are an Airline Assistant, or N if you are not one.");
                Passenger_Continue = Limit_Input_To_Y_N(ReadLine().ToUpper());
                if (Passenger_Continue == "Y") //allows access to seat availability list
                {
                    Airline_Assistant = true;
                    Clear();
                }
                else if (Passenger_Continue == "N")
                {
                    Airline_Assistant = false;
                    Clear();
                }
                else
                if (Passenger_Continue == "Q")
                {
                    break;
                }

                do
                {
                    //Get first and last name
                    First_Name = Get_Name(1);
                    Last_Name = Get_Name(2);

                    //Create passenger obj with all necessary info
                    passengers[P_ID] = new Passenger(First_Name, Last_Name, Flight_Num, Departure_Time, Gate);


                    if (entries < 2) //limits entries per check-in to 3
                    {
                        WriteLine("Do you want to enter another passenger? Press Y or N");
                        Passenger_Continue = Limit_Input_To_Y_N(ReadLine().ToUpper());

                        if (Passenger_Continue == "Q")
                        {
                            break;
                        }

                        if (Passenger_Continue == "Y")
                        {

                            if (!(P_ID == (TOTAL_PASSENGERS - 1)))
                            {
                                ++P_ID;
                                ++entries;
                                Clear();

                            }
                            else if (P_ID == (TOTAL_PASSENGERS - 1))
                            {
                                WriteLine("The flight is full. No New passengers can be added. Please proceed to pick your seats.");
                                break;

                            }

                        }
                        else if (Passenger_Continue == "N" || Passenger_Continue == "Q")
                        {
                            break;
                        }
                    }
                    else
                    {
                        WriteLine("The maximum passengers per check-in is three. Please press Y to proceed to seats selection.");
                        Passenger_Continue = Limit_Input_To_Y(ReadLine().ToUpper());
                        if (Passenger_Continue == "Y")
                        {
                            Clear();
                            break;
                        }
                        else
                        if (Passenger_Continue == "Q")
                        {
                            break;
                        }
                    }

                } while (!(P_ID == (TOTAL_PASSENGERS)));

                if (Passenger_Continue == "Q")
                {
                    break;
                }

                //Seating section
                if (Airline_Assistant == true) //gives different messages to Airline Assistant and normal user.
                {
                    WriteLine("\nPress Y to pick seats, N to let the system allocate them, or L to bring up the seating list.");
                }
                else
                {
                    WriteLine("\nPress Y to pick your seat, or N to let the system allocate them for you.");

                }
                Seats = Limit_Input_To_Y_N_L(ReadLine().ToUpper());

                if (Seats == "Q")
                {
                    break;
                }

                Clear();


                if (Seats == "Y" || Seats == "L")
                {


                    listing(passengers, seats, P_ID, Seats); //list all available seats

                    switch (entries)
                    {
                        case 0:
                            WriteLine("Choose a seat number between 1 and {0} for {1} {2}:", TOTAL_PASSENGERS, passengers[P_ID].FIRST_NAME, passengers[P_ID].LAST_NAME);
                            CHECK = int.TryParse(ReadLine(), out Seat_Number);
                            Seat_Number = Check_Seat_Range(CHECK, Seat_Number, TOTAL_PASSENGERS);
                            Seat_Number = Check_Seat_Availability(seats, Seat_Number, taken);
                            seats[Seat_Number - 1] = taken;
                            passengers[P_ID].SEAT_NUM = Seat_Number;

                            break;

                        case 1:
                            for (int i = 1; i >= 0; --i)
                            {
                                WriteLine("Choose a seat number between 1 and {0} for {1} {2}:", TOTAL_PASSENGERS, passengers[P_ID - i].FIRST_NAME, passengers[P_ID - i].LAST_NAME);
                                CHECK = int.TryParse(ReadLine(), out Seat_Number);
                                Seat_Number = Check_Seat_Range(CHECK, Seat_Number, TOTAL_PASSENGERS);
                                Seat_Number = Check_Seat_Availability(seats, Seat_Number, taken);
                                seats[Seat_Number - 1] = taken;
                                passengers[P_ID - i].SEAT_NUM = Seat_Number;

                            }
                            break;

                        case 2:
                            for (int i = 2; i >= 0; --i)
                            {
                                WriteLine("Choose a seat number between 1 and {0} for {1} {2}:", TOTAL_PASSENGERS, passengers[P_ID - i].FIRST_NAME, passengers[P_ID - i].LAST_NAME);
                                CHECK = int.TryParse(ReadLine(), out Seat_Number);
                                Seat_Number = Check_Seat_Range(CHECK, Seat_Number, TOTAL_PASSENGERS);
                                Seat_Number = Check_Seat_Availability(seats, Seat_Number, taken);
                                seats[Seat_Number - 1] = taken;
                                passengers[P_ID - i].SEAT_NUM = Seat_Number;

                            }
                            break;
                    }
                }
                else if (Seats == "N")
                {

                    switch (entries)
                    {
                        case 0: //finds available seat array and assigns seat number to passenger
                            int y = 0;
                            while (seats[y] == taken)
                            {
                                ++y;
                            }
                            seats[y] = taken;
                            passengers[P_ID].SEAT_NUM = y + 1;
                            break;

                        case 1: //finds 2 adjacent available seat array and assigns seat numbers to passengers
                            y = 0;
                            while (seats[y] == taken || seats[y + 1] == taken)
                            {
                                ++y;
                            }

                            seats[y] = taken;
                            seats[y + 1] = taken;
                            passengers[P_ID - 1].SEAT_NUM = y + 1;
                            passengers[P_ID].SEAT_NUM = y + 2;
                            break;

                        case 2: //finds 3 adjacent available seat array and assigns seat numbers to passengers
                            y = 0;
                            while (seats[y] == taken || seats[y + 1] == taken || seats[y + 2] == taken)
                            {
                                ++y;
                            }

                            seats[y] = taken;
                            seats[y + 1] = taken;
                            seats[y + 2] = taken;
                            passengers[P_ID - 2].SEAT_NUM = y + 1;
                            passengers[P_ID - 1].SEAT_NUM = y + 2;
                            passengers[P_ID].SEAT_NUM = y + 3;
                            break;
                    }
                }

                //Display boarding passes
                if (!(Passenger_Continue == "Q"))
                {
                    Clear();
                    WriteLine("Boarding Pass:");
                    for (int i = (P_ID - entries); i <= P_ID; ++i)
                    {
                        Get_Date();
                        passengers[i].Display_Boarding_Pass();

                    }
                }

                //Let's user choose to input more passengers or end the system
                WriteLine("Press Y to check in more passengers, or N to close the flight.");
                Passenger_Continue = Limit_Input_To_Y_N(ReadLine().ToUpper());

                if (Passenger_Continue == "Y" && !(P_ID == TOTAL_PASSENGERS - 1))
                {
                    ++P_ID;
                    entries = 0;
                    Clear();
                }

                if (Passenger_Continue == "N" || Passenger_Continue == "Q")
                {
                    break;
                }

                if (P_ID == TOTAL_PASSENGERS - 1)
                {
                    WriteLine("The flight is full. No New passengers can be added.");
                    break;
                }

            } while (P_ID <= TOTAL_PASSENGERS);


            //Display passenger list 
            if (P_ID == TOTAL_PASSENGERS - 1)
            {
                Clear();
                WriteLine("\nThe flight is full. No New passengers can be added.\n");
                WriteLine("**********************************************************\n");
            }

            if (!(Passenger_Continue == "Q" || Seats == "Q"))
            {
                if (!(P_ID == TOTAL_PASSENGERS - 1))
                {
                    Clear();
                }
                WriteLine("Passenger list for flight {0}", Flight_Num);
                WriteLine("\n        Name        |    Security Number    |    Seat Number");
                for (int i = 0; i <= P_ID; i++)
                {
                    passengers[i].List_All_Passengers();
                }
                WriteLine("\n****************************************************************");
                WriteLine("\nThank you for using the system!");
            }
            else if (Passenger_Continue == "Q" || Seats == "Q")
            {
                WriteLine("\n**********************************************************");
                WriteLine("\nThank you for using the system!");
            }
            Read();
        }


        public static void listing(Passenger[] passengers, string[] seats, int P_ID, string x)
        {
            if (x == "L")
            {
                passengers[P_ID].Show_Available_Seats(seats);
                WriteLine("");

            }
        }

        //Get current date and time
        public static void Get_Date()
        {
            DateTime date = DateTime.Now;
            WriteLine("\n" + date.ToString("dddd, dd MMMM yyyy hh:mm tt"));
        }

        //Check that input only consist of the letter Y
        public static string Limit_Input_To_Y(string input)
        {
            int x = 0;
            bool pass = true;

            while (!(input == "Y") && x < 3)
            {
                WriteLine("\nError: Please try again: ");
                input = ReadLine().ToUpper();

                ++x;
            }
            if (input == "Y")
            {
                pass = false;
            }
            while (pass)
            {
                WriteLine("\nYou've tried more than 3 times. Press Q to quit.");
                input = ReadLine().ToUpper();
                if (input == "Q")
                {
                    pass = false;
                }
            }

            return input;
        }

        //Check that input only consist of the letter Y & N
        public static string Limit_Input_To_Y_N(string input)
        {
            int x = 0;
            bool pass = true;

            while (!(input == "Y" || input == "N") && x < 3)
            {
                WriteLine("\nError: Please try again: ");
                input = ReadLine().ToUpper();

                ++x;
            }
            if (input == "Y" || input == "N")
            {
                pass = false;
            }

            while (pass)
            {
                WriteLine("\nYou've tried more than 3 times. Press Q to quit.");
                input = ReadLine().ToUpper();
                if (input == "Q")
                {
                    pass = false;
                }
            }

            return input;

        }

        //Check that input only consist of the letter Y & N & L
        public static string Limit_Input_To_Y_N_L(string input)
        {
            int x = 0;
            bool pass = true;

            while (!(input == "Y" || input == "N" || input == "L") && x < 3)
            {
                WriteLine("\nError: Please try again: ");
                input = ReadLine().ToUpper();

                ++x;
            }
            if (input == "Y" || input == "N" || input == "L")
            {
                pass = false;


            }

            while (pass)
            {
                WriteLine("\nYou've tried more than 3 times. Press Q to quit.");
                input = ReadLine().ToUpper();
                if (input == "Q")
                {
                    pass = false;
                }
            }

            return input;

        }

        //Searches seats array containing the string "available". Returns seat number that is available.
        public static int Check_Seat_Availability(string[] seats, int seat_num, string taken)
        {
            while (seats[seat_num - 1] == taken)
            {
                WriteLine("That seat is taken. Please choose again:");
                seat_num = int.Parse(ReadLine());
            }
            return seat_num;
        }

        //Checks that the selected seat in within range. Returns seat number that is within range.
        public static int Check_Seat_Range(bool check, int num, int max)
        {

            while (!check || num < 1 || num > max)
            {
                WriteLine("Error. Please enter a number between 1 and {0}:", max);
                check = int.TryParse(ReadLine(), out num);
            }
            return num;
        }

        //Fills seat array with string "available"
        public static void Create_Available_Seats(string[] seats)
        {

            for (int i = 0; i < seats.Length; ++i)
            {
                seats[i] = "AVAILABLE";
            }

        }

        //Get user to input first and last name. Returns first and last name.
        public static string Get_Name(int pos)
        {
            string name = "";
            bool Check;
            int Check_Name;

            switch (pos)
            {
                case 1:
                    WriteLine("Please enter the first name: ");
                    name = ReadLine().ToUpper();
                    Check = int.TryParse(name, out Check_Name);

                    while (Check)
                    {
                        WriteLine("Error: Please enter the first name again: ");
                        name = ReadLine().ToUpper();
                        Check = int.TryParse(name, out Check_Name);

                    }
                    break;

                case 2:

                    WriteLine("Please enter the last name: ");
                    name = ReadLine().ToUpper();
                    Check = int.TryParse(name, out Check_Name);

                    while (Check)
                    {
                        WriteLine("Error: Please enter the last name again: ");
                        name = ReadLine().ToUpper();
                        Check = int.TryParse(name, out Check_Name);

                    }
                    break;
            }
            return name;
        }

    }
}
