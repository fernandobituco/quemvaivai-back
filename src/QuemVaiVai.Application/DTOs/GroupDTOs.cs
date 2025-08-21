using System.ComponentModel.DataAnnotations;

namespace QuemVaiVai.Application.DTOs
{
    public class CreateGroupDTO
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
    public class UpdateGroupDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }

    public class GroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Deleted { get; set; }
    }

    public class GroupCardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MemberCount { get; set; }
        public int EventCount { get; set; }
        public DateTime NextEvent { get; set; }
        public bool CanEdit { get; set; }
    }
}
