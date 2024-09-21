namespace BloggingPlatformApi.Models
{
    public class Writer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
