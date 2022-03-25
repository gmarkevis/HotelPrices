using Microsoft.EntityFrameworkCore;

namespace HotelPrices.Models
{
    public class Context : DbContext
    {
        protected Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<HotelQuarto> HotelQuarto { get; set; }
        public DbSet<HotelQuartoTarifa> HotelQuartoTarifa { get; set; }
    }
}
