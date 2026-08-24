
namespace QuemVaiVai.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public int TaskListId { get; set; }
        public TaskList TaskList { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }

        public bool IsDone { get; set; } = false;
    }
}
