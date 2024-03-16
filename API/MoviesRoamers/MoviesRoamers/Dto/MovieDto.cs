namespace MoviesRoamers.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public string Description { get; set; }
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public List<string> Genres { get; set; }
    }
}
