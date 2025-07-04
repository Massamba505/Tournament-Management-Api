using System.ComponentModel.DataAnnotations;

namespace Tournament.Management.API.Models.Domain
{
    public class Member
    {
        public int Id { get; set; }
        
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        // Navigation properties can be added if needed
        public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    }
}
