using TeamAppNew.Enums;

namespace TeamAppNew.Models;

public class Member
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<ProgramingLanguages> ProgramingLanguages { get; set; }
    public ContractType ContractType { get; set; }

    
    public Member(string name, int age, List<ProgramingLanguages> programingLanguages, ContractType contract)
    {
        Name = name;
        Age = age;
        ProgramingLanguages = programingLanguages;
        ContractType = contract;
    }

    //public string GetMemberDetails()
    //{
    //    var languages = string.Join(", ", ProgramingLanguages);

    //    return $"Member {Name} is {Age} " +
    //        $"years old, knows {languages}" +
    //        $" and works {ContractType}";
    //}
}
