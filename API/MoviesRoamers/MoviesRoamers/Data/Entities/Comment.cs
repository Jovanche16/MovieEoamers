using MovieRoamers.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
