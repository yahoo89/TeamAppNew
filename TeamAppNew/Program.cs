using TeamAppNew.Helpers;
using TeamAppNew.Components;
using TeamAppNew.Models;
using TeamAppNew.Enums;
using Spectre.Console;
using TeamAppNew.Services;

namespace TeamAppNew;

internal class Program
{
    const int MAX_AGE = 100;
    static CustomComponents customComponents = new(); // later this will be done by DI
    static MemberService memberService = new();

    static void Main(string[] args)
    {
        IValidator validator = new Validator();
        DoMainWork(validator);
    }

    private static void DoMainWork(IValidator validator)
    {
        int membersCount = validator.IsValidTeamSize("\nEnter count of team members, value shoud be greater than '0': ");

        for (int memberOrder = 1; memberOrder <= membersCount; memberOrder++)
        {
            memberService.AddMember(GetMemberInfo(validator));
        }

        ShowMembers();
        
        ConsoleKeyInfo enteredKey;
        
        do // nice loop variant when you want to execute logic at least 1 time
        {
            AnsiConsole.MarkupLine(
            "\n\n[bold aqua]SELECT AN ACTION:[/]" +
                "\n[red]'X' to exit the programm,[/]" +
                "\n[bold lime]'A' to add another member,[/]" +
                "\n[bold green]'E' to edit current member,[/]" +
                "\n[bold lime]'R' to remove a member(s),[/]" +
                "\n[bold purple]'P' to display the list of members again[/]");

            enteredKey = Console.ReadKey();

            switch (enteredKey.Key)
            {
                case ConsoleKey.A:
                    var member = GetMemberInfo(validator);
                    memberService.AddMember(member);
                    AnsiConsole.MarkupLine("\n[bold green]Team members have been updated:[/]");
                    break;
                case ConsoleKey.R:
                    RemoveMember();
                    ShowMembers();
                    break;
                case ConsoleKey.E:
                    EditCurrentMembers();
                    ShowMembers();
                    break;
                case ConsoleKey.P:
                    ShowMembers();
                    break;
                default:
                    AnsiConsole.MarkupLine("[bold red]Unknown key pressed![/]");
                    break;
            }
        }
        while (enteredKey.Key != ConsoleKey.X);

        AnsiConsole.MarkupLine("\n[bold blue]Exiting the application...[/]");
        Thread.Sleep(TimeSpan.FromSeconds(2));
        return;
    }

    private static void RemoveMember()
    {
        var members = memberService.Members;
        var selectedUsers = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title($"\n[green]Please select whom you want to remove :[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more members)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a fruit, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(members.Select(member => member.ToString()).ToList()));

        int membersCount = memberService.Remove(selectedUsers);        

        if (membersCount > 0)
        {
            AnsiConsole.MarkupLine("\n[red]Removed Members:[/]");
            // On List object you have this nice extension method to iterate and do something
            selectedUsers.ForEach(member => AnsiConsole.MarkupLine($"[red] - {member.ToString()}[/]"));           
        }
        else
        {
            AnsiConsole.MarkupLine("\n[bold green]No members were removed.[/]");
        }
    }

    private static void EditCurrentMembers()
    {
        Console.WriteLine(" I am updating members");
        // implement logic for edit
        //memberService.EditMember();
    }

    private static void ShowMembers()
    {
        var members = memberService.Members;
        if (members == null || members.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[underline red]No members to display.[/]");
            return;
        }

        customComponents.ShowFinalTable(members);
    }

    private static Member GetMemberInfo(IValidator validator)
    {
        int memberOrder = memberService.Members.Count;

        var name = validator.IsValidString($"\nEnter the name of team member number {++memberOrder} : ");
        var age = validator.IsValidAge($"Enter the age of team member number {memberOrder}: ", MAX_AGE);

        // is it ok that programingLanguage var but not enum ProgramingLanguage (depends, in this case - ok)
        var programingLanguage = SelectProgramingLanguages("Enter programing language name which using team member number", memberOrder);

        string contract = SelectContractType("Please select the type of contract you agree to for team member", memberOrder);

        return new Member(name, age, programingLanguage, contract);
    }

    private static List<ProgramingLanguages> SelectProgramingLanguages(string message, int memberOrder)
    {
        var languages = Enum.GetNames(typeof(ProgramingLanguages));

        var selectedLanguageNames = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title($"[green]{message} {memberOrder} :[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more programing languages)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a fruit, " +
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices(languages));
        
        AnsiConsole.MarkupLine($"[lime]You have selected the programming languages for team member {memberOrder}:[/] {string.Join(", ", selectedLanguageNames)}");

        var selectedLanguages = selectedLanguageNames.Select(name => Enum.Parse<ProgramingLanguages>(name)).ToList();

        return selectedLanguages;
    }

    private static string SelectContractType(string message, int memberOrder)
    {
        var contractTypes = Enum.GetNames(typeof(ContractType));

        var selectedContractType = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[green]{message} {memberOrder} :[/]")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to reveal more contract types)[/]")
                .AddChoices(contractTypes));

        AnsiConsole.MarkupLine($"[lime]You have selected the contract type for team member {memberOrder} :[/] {selectedContractType}");

        return selectedContractType;
    }
}
