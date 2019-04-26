using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Models
{
    public class Market
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
