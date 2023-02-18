using System.ComponentModel.DataAnnotations;

namespace KisiselWebSitesi.Models.Classes
{
    public class Icon
    {
        [Key]
        public int id { get; set; }
        public string ikon { get; set; }
        public string link { get; set; }

    }
}