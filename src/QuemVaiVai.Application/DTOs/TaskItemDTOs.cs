
namespace QuemVaiVai.Application.DTOs
{
    public class TaskItemDTO
    {
        public int Id { get; set; }
        public string Descritption { get; set; } = string.Empty;
        public int TaskListId { get; set; }
        public int? AssignedTo { get; set; }
        public bool IsDone { get; set; }
    }

    public class CreateTaskItemDTO
    {
        public string Descritption { get; set; } = string.Empty;
        public int TaskListId { get; set; }
    }

    public class UpdateTaskItemDTO
    {
        public int Id { get; set; }
        public string Descritption { get; set; } = string.Empty;
        public int? AssignedTo { get; set; }
        public bool IsDone { get; set; }
    }
}
