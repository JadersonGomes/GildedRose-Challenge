using csharp.Repository.Entities;
using csharp.Repository.Implementations;
using csharp.Repository.Interfaces;
using csharp.Tests;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        
        [Test]
        public void foo()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual("foo", Items[0].Name);
        }

        /// <summary>
        /// Testa se a lista possui no mínimo um item
        /// </summary>
        [Test]
        public void ShouldHaveItems()
        {
            // Arrange
            IGildedRoseRepository sut = new GildedRoseRepository();

            // Act
            var items = sut.GetItems();

            // Assert
            Assert.That(items, Is.Not.Empty);
        }

        /// <summary>
        /// Testa se o item 'Aged Brie' não está ultrapassando o limite de máximo de qualidade (50)
        /// </summary>
        [Test]
        public void ShouldHaveMaxQualityForAgedBrie()
        {
            // Arrange
            var finalStageItems = Helper.GetFinalStageItems();

            // Act            
            var result = 50; // 50 = Max quality            
            var finalQuality = finalStageItems.First(x => x.Name.Equals("Aged Brie")).Quality;

            // Assert
            Assert.That(finalQuality, Is.EqualTo(result));
        }

        /// <summary>
        /// Testa se todos os itens possuem qualidade acima de 0
        /// </summary>
        [Test]
        public void ShouldHavePositiveQuality()
        {
            //Arrange            
            var items = Helper.GetFinalStageItems();

            // Act
            var result = items.Where(x => x.Quality < 0);

            // Assert
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// Testa se existe algum item da lista que possui qualidade acima da máxima permitida, exceto para itens lendários
        /// </summary>
        [Test]
        public void ShouldNotHaveMoreThanFiftyQuality()
        {
            // Arrange
            var finalStageItems = Helper.GetFinalStageItems();

            // Act                        
            var itemWithMoreThanFiftyQuality = finalStageItems.FirstOrDefault(x => x.Quality > 50 && !x.Name.Contains("Sulfuras"));

            // Assert
            Assert.That(itemWithMoreThanFiftyQuality, Is.Null);
        }

        /// <summary>
        /// Teste se o item lendário possui qualidade acima do máximo permitido para itens comuns
        /// </summary>
        [Test]
        public void ShouldBeSulfurasWithLegendQuality()
        {
            // Arrange
            var finalStageItems = Helper.GetFinalStageItems();

            // Act                        
            var legendQualityItem = finalStageItems.Where(x => x.Quality > 50).ToList();

            // Assert
            foreach (var item in legendQualityItem)
            {
                Assert.That(item.Name, Is.EqualTo("Sulfuras, Hand of Ragnaros"));
            }
        }

        /// <summary>
        /// Testa se os itens com data de venda expirada estão com a qualidade zero (exceto itens lendários e 'Aged Brie')
        /// </summary>
        [Test]
        public void ShouldHaveQualityEqualZero()
        {
            // Arrange
            var finalStageItems = Helper.GetFinalStageItems();

            // Act            
            var qualities = finalStageItems.Where(x => x.SellIn < 0 && x.Quality != 0 && !x.Name.Equals("Sulfuras, Hand of Ragnaros") && !x.Name.Equals("Aged Brie")).ToList();

            // Assert
            Assert.That(qualities, Is.Empty);

        }

        /// <summary>
        /// Testa se o novo item adicionado está diminuindo a qualidade duas vezes mais rápido que os demais
        /// </summary>
        [Test]
        public void ShouldDoubleDecreaseQualityForConjured()
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 },
                new Item { Name = "+5 Dexterity Vest", SellIn = 3, Quality = 6 }
            };

            GildedRose app = new GildedRose(items);
            IList<Item> processedItems = new List<Item>();
            var result = 3;

            // Act            
            for (int i = 0; i <= 31; i++)
            {
                processedItems = app.GetItems();
                var conjuredQuality = processedItems.First(x => x.Name.Equals("Conjured Mana Cake")).Quality;

                if (conjuredQuality > 0)
                    app.UpdateQuality();
                else
                    break;
            }

            var dexterityQuality = processedItems.First(x => x.Name.Equals("+5 Dexterity Vest")).Quality;


            // Assert
            Assert.That(dexterityQuality, Is.EqualTo((result)));
        }

        /// <summary>
        /// Testa se os itens 'Backstage' zeram a qualidade imediatamente após a data de venda ser menor que 0
        /// </summary>
        [Test]
        public void ShouldBeZeroWhenSellInIsNegative()
        {
            // Arrange
            IList<Item> items = new List<Item>()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -1, Quality = 20 },
            };
            GildedRose app = new GildedRose(items);
            var isZero = false;

            // Act
            app.UpdateQuality();
            
            foreach (var item in app.GetItems())
            {
                if(item.SellIn < 0 && item.Quality == 0)
                    isZero = true;
            }
           
            // Assert
            Assert.That(isZero, Is.EqualTo(true));
        }

        /// <summary>
        /// Testa se o item 'Aged Brie' aumenta a sua qualidade de um em um
        /// </summary>
        [Test]
        public void ShouldAddOneForAgedBrie()
        {
            // Arrange
            IList<Item> items = new List<Item>()
            {
                new Item { Name = "Aged Brie", SellIn = 5, Quality = 0 },
            };
            GildedRose app = new GildedRose(items);
            var expectedQuality = 1;

            // Act
            app.UpdateQuality();
            var returnedQuality = app.GetItems()[0].Quality;

            // Assert
            Assert.That(expectedQuality, Is.EqualTo(returnedQuality));
        }

        /// <summary>
        /// Testa se o item 'Backstage' diminui 2 ou 3 caso a qualidade esteja abaixo de 11 e 6 respectivamente
        /// </summary>
        /// <param name="sellIn">Valor inicial da data de venda</param>
        /// <param name="expected">Valor esperado para o teste</param>
        [Test]
        [TestCase(10, 22)]
        [TestCase(5, 23)]
        public void ShouldIncreaseTwoOrThreeIfSellInIsLessThanTen(int sellIn, int expected)
        {
            // Arrange
            IList<Item> items = new List<Item>()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 20 },
            };
            GildedRose app = new GildedRose(items);
            var expectedQuality = expected;

            // Act
            app.UpdateQuality();
            var returnedQuality = app.GetItems()[0].Quality;

            // Assert
            Assert.That(expectedQuality, Is.EqualTo(returnedQuality));
        }


    }
}
