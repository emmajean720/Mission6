using System.ComponentModel.DataAnnotations;
using Mission6.Models;
namespace Mission6.Models
{
    //create Movie class
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }
        public bool Edited { get; set; }
        public string LentTo { get; set; }
        public bool CopiedToPlex { get; set; }
        public string Notes { get; set; }

    }
}
