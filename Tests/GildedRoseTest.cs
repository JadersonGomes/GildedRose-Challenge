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

        [Test]
        public void shouldHaveItems()
        {
            // Arrange
            IGildedRoseRepository sut = new GildedRoseRepository();

            // Act
            var items = sut.GetItems();

            // Assert
            Assert.That(items, Is.Not.Empty);
        }

        [Test]
        public void shouldHaveMaxQualityForAgedBrie()
        {
            // Arrange
            var finalStageItems = Helper.GetFinalStageItems();

            // Act            
            var result = 50; // 50 = Max quality            
            var finalQuality = finalStageItems.First(x => x.Name.Equals("Aged Brie")).Quality;

            // Assert
            Assert.That(finalQuality, Is.EqualTo(result));
        }

        [Test]
        public void shouldHavePositiveQuality()
        {
            //Arrange            
            var items = Helper.GetFinalStageItems();

            // Act
            var result = items.Where(x => x.Quality < 0);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void shouldNotHaveMoreThanFiftyQuality()
        {
            // Arrange
            var finalStageItems = Helper.GetFinalStageItems();

            // Act                        
            var itemWithMoreThanFiftyQuality = finalStageItems.FirstOrDefault(x => x.Quality > 50 && !x.Name.Contains("Sulfuras"));

            // Assert
            Assert.That(itemWithMoreThanFiftyQuality, Is.Null);
        }

        [Test]
        public void shouldBeSulfurasWithLegendQuality()
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

        [Test]
        public void shouldHaveQualityEqualZero()
        {
            // Arrange
            var finalStageItems = Helper.GetFinalStageItems();

            // Act            
            var qualities = finalStageItems.Where(x => x.SellIn < 0 && x.Quality != 0 && !x.Name.Equals("Sulfuras, Hand of Ragnaros") && !x.Name.Equals("Aged Brie")).ToList();

            // Assert
            Assert.That(qualities, Is.Empty);

        }

        [Test]
        public void shouldDoubleDecreaseQualityForConjured()
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


        [Test]
        public void shouldBeZeroWhenSellInIsNegative()
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

        [Test]
        public void shouldAddOneForAgedBrie()
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

        [Test]
        [TestCase(10, 22)]
        [TestCase(5, 23)]
        public void shouldIncreaseTwoIfSellInIsLessThanTen(int sellIn, int expected)
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
