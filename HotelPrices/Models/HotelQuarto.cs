using System;
using System.Collections.Generic;

namespace HotelPrices.Models
{
    public class HotelQuarto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Nome { get; set; }
        public int NumeroAdultos { get; set; }
        public List<HotelQuartoTarifa> Tarifas { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
