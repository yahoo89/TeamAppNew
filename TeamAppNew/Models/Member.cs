using TeamAppNew.Enums;

namespace TeamAppNew.Models;

public class Member
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string ProgramingLanguage { get; set; }
    public ContractType ContractType { get; set; }

    public Member(string name, int age, string programingLanguage, ContractType contract)
    {
        Name = name;
        Age = age;
        ProgramingLanguage = programingLanguage;
        ContractType = contract;
    }

    public string GetMemberDetails()
    {
        return $"Member {Name} is {Age} " +
            $"years old, knows {ProgramingLanguage}" +
            $" and works {ContractType}";
    }
}
