using Data.Entities;

namespace MoviesRoamers.Dto
{
    public class WatchListDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime AddedAt { get; set; }
        public Movie Movie { get; set; }
    }
}
