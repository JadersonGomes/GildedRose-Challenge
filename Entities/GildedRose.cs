using csharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Repository.Entities
{
    public class GildedRose
    {
        private IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        /// <summary>
        /// Utilizado para recuperar a lista privada da classe (Getter)
        /// </summary>
        /// <returns></returns>
        public IList<Item> GetItems()
        {
            return this.Items;
        }

        /// <summary>
        /// Método responsável por atualizar a qualidade dos itens da lista
        /// </summary>
        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                Category category = GetCategory(item);
                SubCategory subCategory = GetSubCategory(item);

                if (category != Category.Special)
                {
                    if (item.Quality > 0)
                    {
                        item.Quality = item.Quality - 1;

                        if (subCategory == SubCategory.Magic)
                            item.Quality = item.Quality - 1;
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;

                        if (subCategory == SubCategory.BackstagePass && item.Quality < 50)
                        {
                            if (item.SellIn < 6)
                                item.Quality = item.Quality + 2;

                            else if (item.SellIn < 11)
                                item.Quality = item.Quality + 1;

                        }
                    }
                }

                if (subCategory != SubCategory.Legendary)
                    item.SellIn = item.SellIn - 1;

                if (item.SellIn < 0)
                {
                    if (category != Category.Special)
                    {
                        if (item.Quality > 0)
                            item.Quality = item.Quality - 1;

                    }
                    else
                    {
                        if (subCategory == SubCategory.Cheese)
                        {
                            if (item.Quality < 50)
                                item.Quality = item.Quality + 1;

                        }
                        else if (subCategory == SubCategory.BackstagePass)
                        {
                            item.Quality = item.Quality - item.Quality;
                        }
                    }
                }

            }
        }

        private SubCategory GetSubCategory(Item item)
        {
            switch (item.Name)
            {
                case "Aged Brie":
                    return SubCategory.Cheese;
                case "Backstage passes to a TAFKAL80ETC concert":
                    return SubCategory.BackstagePass;
                case "Sulfuras, Hand of Ragnaros":
                    return SubCategory.Legendary;
                case "Conjured Mana Cake":
                    return SubCategory.Magic;
                default:
                    return SubCategory.Normal;
            }
        }

        private Category GetCategory(Item item)
        {
            if (item.Name == "Aged Brie" || item.Name == "Backstage passes to a TAFKAL80ETC concert" || item.Name == "Sulfuras, Hand of Ragnaros")
                return Category.Special;
            else
                return Category.Common;
        }

    }
}
