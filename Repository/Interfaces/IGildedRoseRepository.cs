using csharp.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Repository.Interfaces
{
    public interface IGildedRoseRepository
    {
        List<Item> GetItems();
    }
}
