namespace Application.Data.Movies
{
    // [DATA]: An example database entity
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
    }
}