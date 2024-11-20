using System.Collections.Generic;

namespace LexicalRes.Models.DTOs
{
    public class CrudModelDTO<T> where T : class
    {
        public string Action { get; set; }

        public string Table { get; set; }

        public string KeyColumn { get; set; }

        public object Key { get; set; }

        public T Value { get; set; }

        public List<T> Added { get; set; }

        public List<T> Changed { get; set; }

        public List<T> Deleted { get; set; }

        public IDictionary<string, object> Params { get; set; }
    }
}
