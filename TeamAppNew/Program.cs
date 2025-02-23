using TeamAppNew.Helpers;
using TeamAppNew.Components;
using TeamAppNew.Models;
using TeamAppNew.Enums;

using System;
using Spectre.Console;
using System.Net.WebSockets;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TeamAppNew;

internal class Program
{
    const int MAX_AGE = 100;
    static List<Member> members = new();

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
            members.Add(GetMemberInfo(memberOrder));
        }

        ShowMembers();

        while (true)
        {
            AnsiConsole.MarkupLine(
            "\n\n[bold aqua]SELECT AN ACTION:[/]" +
                "\n[red]'X' to exit the programm,[/]" +
                "\n[bold lime]'A' to add another member,[/]" +
                "\n[bold green]'E' to edit current member,[/]" +
                "\n[bold lime]'R' to remove a member(s),[/]" +
                "\n[bold purple]'P' to display the list of members again[/]");

            var enteredKey = Console.ReadKey();

            switch (enteredKey.Key)
            {
                case ConsoleKey.X:
                    AnsiConsole.MarkupLine("\n[bold blue]Exiting the application...[/]");
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    return;
                case ConsoleKey.A:
                    var member = GetMemberInfo(members.Count + 1);
                    members.Add(member);
                    AnsiConsole.MarkupLine("\n[bold green]Team members have been updated:[/]");
                    ShowMembers();
                    break;
                case ConsoleKey.R:
                    RemoveMember(members);
                    ShowMembers();
                    break;
                case ConsoleKey.E:
                    EditCurrentMembers(members);
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

        Member GetMemberInfo(int memberOrder)
        {
            var name = validator.IsValidString($"\nEnter the name of team member number {memberOrder} : ");

            var age = validator.IsValidAge($"Enter the age of team member number {memberOrder}: ", MAX_AGE);            

            // is it ok that programingLanguage var but not enum ProgramingLanguage
            var programingLanguage = SelectProgramingLanguages("Enter programing language name which using team member number", memberOrder);
            
            ContractType contract = SelectContractType("Please select the type of contract you agree to for team member", memberOrder);

            return new Member(name, age, programingLanguage, contract);
        }

        void ShowMembers() // no nee to pass `members` here as now they are global in class
        {
            if (members == null || members.Count == 0)
            {
                AnsiConsole.MarkupLine("\n[underline red]No members to display.[/]");
                return;
            }

            CustomComponents customComponents = new CustomComponents();

            customComponents.ShowFinalTable(members);

        }
        void EditCurrentMembers (List<Member> members)
        {
            Console.WriteLine(" I am updating members");
        }
        void RemoveMember(List<Member> members)
        {
            var selectedUsers = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title($"\n[green]Please select whom you want to remove :[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more members)[/]")
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to toggle a fruit, " +
                        "[green]<enter>[/] to accept)[/]")
                    .AddChoices(members.Select(member => FormatMemberInfo(member)).ToList()));

            var removedMembers = members.Where(member => selectedUsers.Contains(FormatMemberInfo(member))).ToList();

            members.RemoveAll(member => selectedUsers.Contains(FormatMemberInfo(member)));

            if (removedMembers.Count > 0)
            {
                AnsiConsole.MarkupLine("\n[red]Removed Members:[/]");
                foreach (var member in removedMembers)
                {
                    AnsiConsole.MarkupLine($"[red] - {FormatMemberInfo(member)}[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("\n[bold green]No members were removed.[/]");
            }
        }
        string FormatMemberInfo(Member member)
        {
            return $"Member  {member.Name}, " +
                $"{member.Age} years old; " +
                $"knows - {string.Join(", ", member.ProgramingLanguages)}; " +
                $"Contract type is {member.ContractType}.";
        }
        List<ProgramingLanguages> SelectProgramingLanguages(string message, int memberOrder)
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

        ContractType SelectContractType(string message, int memberOrder)
        {
            var contractTypes = Enum.GetNames(typeof(ContractType));

            var selectedContractType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[green]{message} {memberOrder} :[/]")
                    .PageSize(5)
                    .MoreChoicesText("[grey](Move up and down to reveal more contract types)[/]")
                    .AddChoices(contractTypes));
            AnsiConsole.MarkupLine($"[lime]You have selected the contract type for team member {memberOrder} :[/] {selectedContractType}");

            if (Enum.TryParse(selectedContractType, ignoreCase: true, out ContractType typeName))
            {
                return typeName;
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Invalid selection! Defaulting to FullTime.[/]");
                return ContractType.FullTime;
            }
        }
    }
}

public class MemberService
{
    private static readonly string FilePath = "members.json";

    public static void SaveMembers(List<Member> members)
    {
        var json = JsonSerializer.Serialize(members, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    
    public static List<Member> LoadMembers()
    {
        
        if (!File.Exists(FilePath)) return new List<Member>();

        var json = File.ReadAllText(FilePath);
        var members = JsonSerializer.Deserialize<List<Member>>(json);

        return members ?? new List<Member>();
    }
}