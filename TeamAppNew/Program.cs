using TeamAppNew.Enums;

namespace TeamAppNew;

internal partial class Program
{
    static void Main(string[] args)
    {
        var isClosing = false;
        var members = new List<Member>();
        int membersCount = isValidInt("Enter cout of team members, value shoud be greater than '0': ");

        AddMember(members, membersCount);

        ShowMembers(members);

        while (!isClosing)
        {

            Console.Write("\n\nPress 'X' to exit, 'A' to add another member, or 'P' to display the list of members again:");
            var enteredKey = Console.ReadKey();

            Console.WriteLine();
            switch (enteredKey.Key)
            {
                case ConsoleKey.X:
                    Console.WriteLine("Exiting the application...");
                    Thread.Sleep(2000);
                    isClosing = true;
                    break;
                case ConsoleKey.A:
                    AddMember(members);
                    Console.WriteLine("\nTeam members have been updated:");
                    ShowMembers(members);
                    break;
                case ConsoleKey.P:
                    ShowMembers(members);
                    break;
                default:
                    Console.WriteLine("Unknown key pressed");
                    break;
            }
        }
    }
}