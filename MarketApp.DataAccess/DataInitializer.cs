using MarketApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.DataAccess
{
    public class DataInitializer : CreateDatabaseIfNotExists<MarketContext>
    {
        protected override void Seed(MarketContext context)
        {
            context.Markets.AddRange
            (
                new List<Market>()
                {
                    new Market
                    {
                        Product="Морковь",
                        Price=1000,
                        Quantity=20
                    },

                    new Market
                    {
                        Product="Лук",
                        Price=1000,
                        Quantity=20
                    },

                    new Market
                    {
                        Product="Капуста",
                        Price=1000,
                        Quantity=20
                    },

                    new Market
                    {
                        Product="Перец",
                        Price=1000,
                        Quantity=20
                    },

                    new Market
                    {
                        Product="Баклажан",
                        Price=1000,
                        Quantity=20
                    },

                    new Market
                    {
                        Product="Помидов",
                        Price=1000,
                        Quantity=20
                    }
                }
            );
        }
    }
}
