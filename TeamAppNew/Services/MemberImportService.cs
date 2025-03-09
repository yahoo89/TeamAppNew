using TeamAppNew.Models;
using System.Text.Json;

namespace TeamAppNew.Services;

public class MemberImportService
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