using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeApi
{
    public class DalManager
    {
        // List of jokes - let's assume this is the Database :-)
        private List<Joke> jokes = new List<Joke>()
        {
            new Joke(1, "Hvorfor skulle skyen i skole? \n– Fordi den skulle lære at regne", Categories.DadJokes, "DA"),
            new Joke(2, "Hvorfor er det at fisk er så grimme? \n– Det er jo selvfølgelig fordi at de er van(d)skabte", Categories.DadJokes, "DA"),
            new Joke(3, "Hvad er det mindst talte sprog i verden?  \n– Tegnsprog", Categories.DadJokes, "DA"),
            new Joke(4, "Hvad sagde den ene cykel til den anden? \n– Styr dig!", Categories.DadJokes, "DA"),
            new Joke(5, "Hvorfor var blondinen glad for, at samle et puzzlespil på 6 måneder? \n– fordi der stod 2-4 år", Categories.Blond, "DA"),
            new Joke(6, "Hvad er forskellen på en myg og en blondine? \n– Myggen stopper med at suge når man klapper den.", Categories.Blond, "DA"),
            new Joke(7, "Hvorfor har blondiner stiger med ude at købe ind? \n– Fordi priserne er så høje!", Categories.Blond, "DA"),
            new Joke(8, "En blondine, en fed brunette og en tynd rødhåret finder et magisk spejl. Hvis du lyver til spejlet dør du. Den rødhårede siger: “Jeg er fed”, og dør. Den brunette siger: “Jeg ser tynd”, og dør. Den blonde siger, “Jeg tænker lige…” og dør.", Categories.Blond, "DA"),
            new Joke(9, "Hvem der? \n- Finn \nFinn hvem? \n- Find selv af det.", Categories.KnockKnockJokes, "DA"),
            new Joke(10, "Hvem er det? \n- Far! \nFar hvem? \n- Farvel…", Categories.KnockKnockJokes, "DA"),
            new Joke(11, "Hvem der? \n- Kanye \nKanye hvem? \n- Kanye komme op i dig", Categories.KnockKnockJokes, "DA"),
            new Joke(12, "Hvem der? \n- Jens Rohde \nJens Rohde hvem? \n- Jens Rohde, at vi skulle være de første kærester på månen.", Categories.KnockKnockJokes, "DA"),
            new Joke(13, "Programmør med briller \nJeg kan ikke C, derfor bruger jeg briller", Categories.Programming, "DA"),
            new Joke(14, "Komprimeret \n- ARJ hvor er du ZIPpet. Du kan godt PAKke sammen. - Du er ikke ret RAR", Categories.Programming, "DA"),
            new Joke(15, "En omskrivning \nDer er url'er i mosen.", Categories.Programming, "DA"),
            new Joke(16, "Rigtige mænd tager ikke backup - men de græder meget", Categories.Programming, "DA"),
            new Joke(17, "I'm afraid for the calendar. Its days are numbered.", Categories.DadJokes, "EN"),
            new Joke(18, "My wife said I should do lunges to stay in shape. That would be a big step forward.", Categories.DadJokes, "EN"),
            new Joke(19, "Why do fathers take an extra pair of socks when they go golfing? \nIn case they get a hole in one!", Categories.DadJokes, "EN"),
            new Joke(20, "Singing in the shower is fun until you get soap in your mouth. Then it's a soap opera.", Categories.DadJokes, "EN"),
            new Joke(21, "Two blondes fell down a hole. One said, 'It's dark in here isn't it?' The other replied, 'I don't know; I can't see.'", Categories.Blond, "EN"),
            new Joke(22, "Blonde: 'What does IDK stand for?' \nBrunette: 'I don’t know.' \nBlonde: 'OMG, nobody does!'", Categories.Blond, "EN"),
            new Joke(23, "Q: Why can't a blonde dial 911? \nA: She can't find the eleven.", Categories.Blond, "EN"),
            new Joke(24, "A robber comes into the store & steals a TV. A blonde runs after him and says, 'Wait, you forgot the remote!'", Categories.Blond, "EN"),
            new Joke(25, "Knock! Knock! Who's There? The interrupting sheep. The interr..BAAA!!!", Categories.KnockKnockJokes, "EN"),
            new Joke(26, "Knock! Knock! Who's There? Amish. Amish who? You're not a shoe!", Categories.KnockKnockJokes, "EN"),
            new Joke(27, "Knock! Knock! Who's There? Isabel. Isabel who? Isabel working?", Categories.KnockKnockJokes, "EN"),
            new Joke(28, "Knock! Knock! Who’s there? Ben. Ben who? Ben knocking for 10 minutes!", Categories.KnockKnockJokes, "EN"),
            new Joke(29, "What's the best thing about a Boolean? \nEven if you're wrong, you're only off by a bit.", Categories.Programming, "EN"),
            new Joke(30, "What's the object-oriented way to become wealthy? \nInheritance", Categories.Programming, "EN"),
            new Joke(31, "Where do programmers like to hangout? \nThe Foo Bar.", Categories.Programming, "EN"),
            new Joke(32, "Why did the programmer quit his job? \nBecause he didn't get arrays.", Categories.Programming, "EN"),
        };

        //Danish translation of the categories
        private List<string> categoriesDA = new List<string>()
        {
            "Far Jokes",
            "Blondine",
            "Banke banke på",
            "Programmør Jokes",
        };
        // English translaton of the categories
        private List<string> categoriesEN = new List<string>()
        {
            "Dad Jokes",
            "Blonde",
            "Knock knock Jokes",
            "Programming Jokes",
        };
        /// <summary>
        /// Gets a random joke
        /// </summary>
        /// <param name="categoryID">Id of the chosen category</param>
        /// <param name="excluded">list of Id's to exclude from the valid range</param>
        /// <param name="lang">Requested language</param>
        /// <returns>A random joke</returns>
        public Joke GetRandomJoke(int categoryID, List<int> excluded, string lang)
        {
            List<Joke> _jokes = SortOutExcluded(excluded);
            _jokes = SortCategory(_jokes, categoryID);
            Random random = new Random();
            for (int i = 0; i < _jokes.Count; i++)
            {
                int r = random.Next(_jokes.Count);
                if (_jokes[r].Language == lang && (int)_jokes[r].Category == categoryID)
                {
                    return _jokes[r];
                }

            }

            return null;
        }
        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="id">Id of the category to get</param>
        /// <param name="lang">The language - DA or EN</param>
        /// <returns></returns>
        public string GetCategory(Categories id, string lang)
        {
            if (lang == "DA")
            {
                return categoriesDA[(int)id];
            }
            return categoriesEN[(int)id];
        }
        /// <summary>
        /// Gets all of the categories
        /// </summary>
        /// <param name="lang">Language - DA or EN</param>
        /// <returns></returns>
        public List<string> GetAllCategories(string lang)
        {
            if (lang == "DA")
            {
                return categoriesDA;
            }
            return categoriesEN;
        }
        /// <summary>
        /// Excluding all categories but the chosen one
        /// </summary>
        /// <param name="jokes"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private List<Joke> SortCategory(List<Joke> jokes, int category)
        {
            List<Joke> _jokes = new List<Joke>();
            foreach (Joke j in jokes)
            {
                if ((int)j.Category == category)
                {
                    _jokes.Add(j);
                }
            }
            return _jokes;
        }
        /// <summary>
        /// Sorts out all the excluded jokes
        /// </summary>
        /// <param name="excluded"></param>
        /// <returns></returns>
        private List<Joke> SortOutExcluded(List<int> excluded)
        {
            List<Joke> _jokes = new List<Joke>();
            foreach (Joke j in jokes)
            {
                if (!excluded.Contains(j.Id))
                {
                    _jokes.Add(j);
                }
            }
            return _jokes;
        }
    }
}
