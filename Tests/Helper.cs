using csharp.Repository.Entities;
using csharp.Repository.Implementations;
using csharp.Service.Implementations;
using csharp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Tests
{
    public static class Helper
    {
        /// <summary>
        /// Executa o método updateQuality com a lista inicial de itens em 31 dias (representando os dias do mês)
        /// </summary>
        /// <returns>Retorna a lista inicial de itens após o processamento pelo método updateQuality</returns>
        public static IList<Item> GetFinalStageItems()
        {
            IGildedRoseService _service = new GildedRoseService(new GildedRoseRepository());
            var items = _service.GetItems();
            GildedRose app = new GildedRose(items);

            for (int i = 0; i < 31; i++)
            {
                app.UpdateQuality();
            }

            var finalStageItems = app.GetItems();
            return finalStageItems;

        }
       
    }
}
