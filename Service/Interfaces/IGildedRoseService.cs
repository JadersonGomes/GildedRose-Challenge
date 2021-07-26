using csharp.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Service.Interfaces
{
    public interface IGildedRoseService
    {
        IList<Item> GetItems();
    }
}
