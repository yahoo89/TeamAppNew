using TeamAppNew.Enums;

namespace TeamAppNew.Helpers;

public interface IValidator
{
    int IsValidAge(string message, int max = 100);
    bool IsValidCharacters(string input);
    ContractType ValidateContractType(string message);
    string IsValidString(string message);
    int IsValidTeamSize(string message);
}