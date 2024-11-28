using TeamAppNew.Helpers;
using TeamAppNew.Models;
using TeamAppNew.Enums;

namespace TeamAppNew;

internal class Program
{
    const int MAX_AGE = 100;
    static List<Member> members = new(); // 1 option keep it here (more global available for all methods within Program class)

    static void Main(string[] args)
    {
        IValidator validator = new Validator(); // for this in future we use DI (Dependency injection)

        DoMainWork(validator);        
    }

    private static void DoMainWork(IValidator validator)
    {
        //var members = new List<Member>(); // 2nd option keep it here (local in method)

        int membersCount = validator.IsValidTeamSize("\nEnter count of team members, value shoud be greater than '0': ");

        for (int memberOrder = 1; memberOrder <= membersCount; memberOrder++)
        {
            members.Add(GetMemberInfo(memberOrder));
        }

        ShowMembers();

        while (true)
        {
            Console.Write("\n\nPress 'X' to exit, 'A' to add another member, or 'P' to display the list of members again:");
            var enteredKey = Console.ReadKey();

            switch (enteredKey.Key)
            {
                case ConsoleKey.X:
                    Console.WriteLine("\nExiting the application...");
                    Thread.Sleep(TimeSpan.FromSeconds(2)); // old way: Thread.Sleep(2000);
                    return;
                case ConsoleKey.A:
                    var member = GetMemberInfo(members.Count + 1);
                    members.Add(member);
                    Console.WriteLine("\nTeam members have been updated:");
                    ShowMembers();
                    break;
                case ConsoleKey.P:
                    ShowMembers();
                    break;
                default:
                    Console.WriteLine("Unknown key pressed");
                    break;
            }
        }

        Member GetMemberInfo(int memberOrder)
        {
            var name = validator.IsValidString($"\nEnter the name of team member number {memberOrder} : ");

            var age = validator.IsValidAge($"Enter the age of team member number {memberOrder}: ", MAX_AGE);

            var programingLanguage = validator.IsValidString($"Enter programing language name which using team member number {memberOrder}: ");

            ContractType contract = validator.ValidateContractType($"Enter 'YES' if team member number {memberOrder} is full-time contract and 'NO' if not: ");

            return new Member(name, age, programingLanguage, contract);
        }

        void ShowMembers() // no nee to pass `members` here as now they are global in class
        {
            if (members == null || members.Count == 0)
            {
                Console.WriteLine("No members to display.");
                return;
            }

            Console.WriteLine("\nMembers List:");

            foreach (Member member in members)
            {
                Console.WriteLine(member.GetMemberDetails());
            }
        }
    }
}