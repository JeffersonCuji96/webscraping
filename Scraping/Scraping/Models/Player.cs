namespace Scraping.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Posicion { get; set; } = "";
        public int Edad { get; set; }
        public string Club { get; set; } = "";
        public string ValorMercado { get; set; } = "";
    }
}
