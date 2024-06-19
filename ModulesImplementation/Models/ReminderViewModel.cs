using System.ComponentModel.DataAnnotations;

namespace ModulesImplementation.Models
{
    public class ReminderViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReminderDateTime { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
