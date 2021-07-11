namespace Application.Domain.Movies
{
    public interface IMovie
    {
         int Id { get; set; }
         string Title { get; set; }
         string Description { get; set; }
         MovieGenre Genre { get; set; }
    }

    public class Movie : IMovie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MovieGenre Genre { get; set; }
    }
}