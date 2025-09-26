
namespace QuemVaiVai.Application.DTOs
{
    public class TaskListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }

    public class CreateTaskListDTO
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
    }

    public class UpdateTaskListDTO
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
