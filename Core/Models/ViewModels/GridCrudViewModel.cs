using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models.ViewModels
{
    public class GridCrudViewModel<T> where T : class
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
