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
            // Utilizo a instância da classe service para recuperar os itens no 'repositorio'
            IGildedRoseService _service = new GildedRoseService(new GildedRoseRepository());

            Console.WriteLine("OMGHAI!");

            // Recupero os itens
            var items = _service.GetItems();
            var app = new GildedRose(items);

            // Simula os dias do mês
            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                
                // Imprime os itens e seus respectivos valores para o dia em questão
                for (var j = 0; j < items.Count; j++)
                {
                    System.Console.WriteLine(items[j].ToString());
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }

            
        }
    }
}
