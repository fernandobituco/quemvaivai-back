using QuemVaiVai.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuemVaiVai.Application.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? EventDate { get; set; }
        public int Going { get; set; }
        public int Interested { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public Guid InviteCode { get; set; }
        public bool ActiveVote { get; set; }
        public Status Status { get; set; }

        //public ICollection<Comment> Comments { get; set; } = [];
        //public ICollection<VoteOption> VoteOptions { get; set; } = [];
    }
    public class CreateEventDTO
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? EventDate { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public Guid InviteCode { get; }
    }
    public class UpdateEventDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? EventDate { get; set; }

        [Required]
        public int GroupId { get; set; }
    }

    public class EventCardDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? EventDate { get; set; }
        public int Going { get; set; }
        public int Interested { get; set; }
        public bool ActiveVote { get; set; }
        public bool UserHasTaskItem { get; set; }
        public bool CanEdit { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public Guid InviteCode { get; }
        public Status Status { get; set; }
    }

    public class JoinEventDTO
    {
        public Guid InviteCode { get; set; }
        public Status Status { get; set; }
    }

    public class EventFiltersDto
    {
        public int? GroupId { get; set; }
        public EventStatusFilter? Status { get; set; }
        public int? Situation { get; set; }
    }
    public enum EventStatusFilter
    {
        Upcoming,
        Past
    }
}
