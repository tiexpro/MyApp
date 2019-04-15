
namespace MyApp.Models
{
    public class Menu
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? parent { get; set; }
        public int? type { get; set; }
        public string icon { get; set; }
        public bool child { get; set; }
        public string action { get; set; }
        public string controller { get; set; }
        public string area { get; set; }
    }
}
