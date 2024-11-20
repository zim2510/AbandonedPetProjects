using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace LexicalRes.Models.Entities
{
    public class WordList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Word> Words { get; set; }
    }

    class WordListConfig : IEntityTypeConfiguration<WordList>
    {
        public void Configure(EntityTypeBuilder<WordList> entity)
        {
            entity.HasData(
                new WordList { Id = 1, Name = "Barron's 333 (1 - 50)" },
                new WordList { Id = 2, Name = "Barron's 333 (51 - 100)" },
                new WordList { Id = 3, Name = "Barron's 333 (101 - 150)" },
                new WordList { Id = 4, Name = "Barron's 333 (151 - 200)" },
                new WordList { Id = 5, Name = "Barron's 333 (201 - 250)" },
                new WordList { Id = 6, Name = "Barron's 333 (251 - 300)" },
                new WordList { Id = 7, Name = "Barron's 333 (301 - End)" }
            );
        }
    }
}
