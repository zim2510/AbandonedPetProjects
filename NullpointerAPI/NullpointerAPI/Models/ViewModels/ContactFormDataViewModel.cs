using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NullpointerAPI.Models.ViewModels
{
    public class ContactFormDataViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
