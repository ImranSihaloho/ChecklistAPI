namespace ChecklistAPI.Models
{
    namespace ChecklistApi.Models
    {
        public class ChecklistItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsCompleted { get; set; }
            public int ChecklistId { get; set; }
            public Checklist Checklist { get; set; }
        }
    }

}
