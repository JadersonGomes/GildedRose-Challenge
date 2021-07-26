using csharp.Repository.Entities;
using csharp.Repository.Implementations;
using csharp.Service.Implementations;
using csharp.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace csharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IGildedRoseService _service = new GildedRoseService(new GildedRoseRepository());

            Console.WriteLine("OMGHAI!");

            var items = _service.GetItems();
            var app = new GildedRose(items);

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < items.Count; j++)
                {
                    System.Console.WriteLine(items[j]);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }

            
        }
    }
}
