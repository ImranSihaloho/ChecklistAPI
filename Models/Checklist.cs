namespace ChecklistAPI.Models
{
    using System.Collections.Generic;

    namespace ChecklistApi.Models
    {
        public class Checklist
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public ICollection<ChecklistItem> Items { get; set; }
        }
    }


}
