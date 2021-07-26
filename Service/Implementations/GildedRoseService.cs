using csharp.Repository.Entities;
using csharp.Repository.Interfaces;
using csharp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Service.Implementations
{
    public class GildedRoseService : IGildedRoseService
    {
        private readonly IGildedRoseRepository _repositoryGildedRose;

        public GildedRoseService(IGildedRoseRepository repositoryGildedRose)
        {
            _repositoryGildedRose = repositoryGildedRose;
        }

        /// <summary>
        /// Utiliza a dependência do repositório para retornar a lista inicial de itens
        /// </summary>
        /// <returns>Lista inicial de itens</returns>
        public IList<Item> GetItems()
        {
            try
            {
                return _repositoryGildedRose.GetItems();
            }
            catch 
            {
                throw;
            }
        }
    }
}
