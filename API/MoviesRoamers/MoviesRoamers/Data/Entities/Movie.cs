using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public string Description { get; set; }
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public string? ImdbId { get; set; }
        public string? TmdbId { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
