using Kerem.CodingTracker.Features.CreateCodingSession;

namespace Kerem.CodingTracker.UI ;

    public class ConsoleMenu
    {
        public void Menu(CreateCodingSession createCodingSession)
        {
            bool exit = false;
            Console.WriteLine("Welcome to the Coding Tracker!");
            Console.WriteLine();
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Create a new coding session");
                Console.WriteLine("2. View all coding sessions");
                Console.WriteLine("3. Delete a coding session");
                Console.WriteLine("4. Update a coding session");
                Console.WriteLine("5. Exit");
                int choice = int.TryParse(Console.ReadLine() ?? string.Empty, out choice) ? choice : 0;
                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        createCodingSession.Create();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        exit = true;
                        Console.WriteLine();
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
    