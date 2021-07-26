using csharp.Repository.Entities;
using csharp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Repository.Implementations
{
    class GildedRoseRepository : IGildedRoseRepository
    {
        /// <summary>
        /// Cria a lista de itens da loja  
        /// </summary>
        /// <returns>Lista de Itens iniciais</returns>
        public List<Item> GetItems()
        {
            try
            {
                List<Item> Items = new List<Item> {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 10,
                        Quality = 49
                    },
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 5,
                        Quality = 49
                    },
				    // this conjured item does not work properly yet
				    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                };

                return Items;
            }
            catch
            {
                throw;
            }
        }
    }
}
