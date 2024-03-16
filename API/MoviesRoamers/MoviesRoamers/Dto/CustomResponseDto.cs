using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesRoamers.Dto
{
    public class CustomResponseDto<T> where T : class
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public List<string> errors { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
