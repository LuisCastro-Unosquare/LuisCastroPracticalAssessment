using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LC_Assessment_Todo.Models
{
    public class TaskDto
    {
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DefaultValue(false)]
        public string IsDone { get; set; }
    }
}
