namespace ModulesImplementation.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReminderDateTime { get; set; }
        public string Email { get; set; } 
        public bool IsSent { get; set; }
    }
}
