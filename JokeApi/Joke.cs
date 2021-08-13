using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeApi
{
    public class Joke
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Categories Category { get; set; }
        public string Language { get; set; }


        public Joke(int id, string text, Categories category, string language)
        {
            this.Id = id;
            this.Text = text;
            this.Category = category;
            this.Language = language;
        }
    }
}
