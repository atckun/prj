using System.Collections.Generic;

namespace AbstractProject.Abstractions
{
    public class Response<T>
    {
        public bool Success { get; set; } = true;
        
        public IEnumerable<string> Errors { get; set; }
        
        public T Body { get; set; }
    }
}