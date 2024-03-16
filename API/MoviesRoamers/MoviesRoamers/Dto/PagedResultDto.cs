using MoviesRoamers.Data.Requests;

namespace MoviesRoamers.Dto
{
    public class PagedResultDto<T>
    {
        public PagingRequest? QueryObject { get; set; }
        public List<T>? Items { get; set; }

    }
}
