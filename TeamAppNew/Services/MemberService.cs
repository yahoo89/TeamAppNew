
using TeamAppNew.Models;

namespace TeamAppNew.Services;

internal class MemberService
{
    // Property with backing field
    private List<Member> _members;
    public List<Member> Members 
    { 
        get => _members ??= new List<Member>(); // if back-field is null - init List
        private set => _members = value; 
    }

    internal void AddMember(Member member)
    {
        Members.Add(member);
    }

    internal int Remove(List<string> selectedUsers)
    {
        var removedMembers = Members.Where(member => selectedUsers.Contains(member.ToString())).ToList();

        Members.RemoveAll(member => selectedUsers.Contains(member.ToString()));

        return removedMembers.Count;
    }
}
