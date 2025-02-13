using System.ComponentModel.DataAnnotations;
using Mission6.Models;
namespace Mission6.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }
        public bool? Edited {  get; set; }
        public string LentTo { get; set; }
        [StringLength(25)]
        public string Notes { get; set; }
    }
}
