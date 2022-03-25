namespace HotelPrices.Models
{
    public class HotelQuartoTarifa
    {
        public int Id { get; set; }
        public int HotelQuartoId { get; set; }
        public string Condicao { get; set; }
        public string ValorMedio { get; set; }
        public string ValorTotal { get; set; }
    }
}
