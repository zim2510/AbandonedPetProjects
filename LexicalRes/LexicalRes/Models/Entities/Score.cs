using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace LexicalRes.Models.Entities
{
    public class Score
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int WordId { get; set; }
        [Range(0, 3)]
        public int Value { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual Word Word { get; set; }
    }
}
