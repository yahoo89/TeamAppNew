using TeamAppNew.Enums;

namespace TeamAppNew.Models;

public class Member
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<ProgramingLanguages> ProgramingLanguages { get; set; }
    public ContractType ContractType { get; set; } = ContractType.FullTime; // default value

    public Member(string name, int age, List<ProgramingLanguages> programingLanguages) // one of ctors (to avoid repetitions)
    {
        Name = name;
        Age = age;
        ProgramingLanguages = programingLanguages;
    }
    // this ctor calls other ctor to reuse initialization
    public Member(string name, int age, List<ProgramingLanguages> programingLanguages, ContractType contractType) : this(name, age, programingLanguages)
    {
        ContractType = contractType;
    }

    public Member(string name, int age, List<ProgramingLanguages> programingLanguages, string contract) : this(name, age, programingLanguages)
    {
        if (Enum.TryParse(contract, ignoreCase: true, out ContractType typeName))
        {
            ContractType = typeName;
        }
    }

    public override string ToString() // one of Object's method that you can override
    {
        return $"Member {Name}, " +
            $"{Age} years old; " +
            $"knows - {string.Join(", ", ProgramingLanguages)}; " +
            $"Contract type is {ContractType}.";
    }
}
