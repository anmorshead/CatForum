namespace CatForum.Models
{
    public class Discussion
    {
        //primary key
        public int DiscussionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        string ImageFileName { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }

        //Navigation Property - a discussion can have many comments
        public List<Comment>? Comments { get; set; } //nullable

    }

}
