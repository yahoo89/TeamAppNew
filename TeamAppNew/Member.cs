namespace TeamAppNew;

internal partial class Program
{
    class Member
    {
        public string Name;
        public int Age;
        public string ProgramingLanguage;
        public bool IsFullTime;

        public Member(string name, int age, string programingLanguage, bool isFullTime)
        {
            Name = name;
            Age = age;
            ProgramingLanguage = programingLanguage;
            IsFullTime = isFullTime;
        }
    }
    static Member GetMemberInfo(int memberOrder)
    {
        var name = isValidString($"Enter the name of team member number {memberOrder + 1} : ");

        var age = isValidInt($"Enter the age of team member number {memberOrder + 1}: ", 100);

        var programingLanguage = isValidString($"Enter programing language name which using team member number {memberOrder + 1}: ");

        var isFulltime = isValidContractType($"Enter 'YES' if team member number {memberOrder + 1} is full-time contract and 'NO' if not: ");

        return new Member(name, age, programingLanguage, isFulltime);
    }
    static void AddMember(List<Member> members, int membersCount)
    {
        for (int i = members.Count; i < membersCount; i++)
        {
            Member newMember = GetMemberInfo(i);
            members.Add(newMember);
        }
    }
    static void AddMember(List<Member> members)
    {
        Member newMember = GetMemberInfo(members.Count);
        members.Add(newMember);
    }
    static void ShowMembers(List<Member> members)
    {
        if (members == null || members.Count == 0)
        {
            Console.WriteLine("No members to display.");
            return;
        }

        Console.WriteLine("\nMembers List:");

        foreach (var member in members)
        {
            var currentContractType = member.IsFullTime ? "Full Time" : "Part Time";
            Console.WriteLine($"Member {member.Name} is {member.Age} " +
                $"years old, knows {member.ProgramingLanguage}" +
                $" and works {currentContractType}");
        }
    }
}