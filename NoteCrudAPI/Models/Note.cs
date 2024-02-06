namespace NoteCrudAPI.Models
{
    public class Note
    {
        public int Id { get; set; }
        public  string Title { get; set; }
        public  string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        //public  int UserId { get; set; } 
        //public List<Tag> Tags { get; set; } = new List<Tag>(); 
    }
}
