using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketApp.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public int? MarketId { get; set; }
        public virtual Market Market { get; set; }
    }
}
