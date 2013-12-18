using System.Collections.Generic;

namespace GildedRose.Console
{
    class Program
    {
        IList<Item> Items;
        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                {
                    Items = new List<Item>
                    {
                        new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                        new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                        new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                        new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                        new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 15,
                            Quality = 20
                        },
                        new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                    }
                };

            app.UpdateQuality();

            System.Console.ReadKey();
        }

        public void UpdateQuality()
        {
            /* for (var i = 0; i < Items.Count; i++)
             {
                 if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                 {
                     if (Items[i].Quality > 0)
                     {
                         if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                         {
                             Items[i].Quality = Items[i].Quality - 1;
                         }
                     }
                 }
                 else // aged brie or concert pass
                 {
                     if (Items[i].Quality < 50)
                     {
                         Items[i].Quality = Items[i].Quality + 1;

                         if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                         {
                             if (Items[i].SellIn < 11)
                             {
                                 if (Items[i].Quality < 50)
                                 {
                                     Items[i].Quality = Items[i].Quality + 1;
                                 }
                             }

                             if (Items[i].SellIn < 6)
                             {
                                 if (Items[i].Quality < 50)
                                 {
                                     Items[i].Quality = Items[i].Quality + 1;
                                 }
                             }
                         }
                     }
                 }

                 if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                 {
                     Items[i].SellIn = Items[i].SellIn - 1;
                 }

                 if (Items[i].SellIn < 0)
                 {
                     if (Items[i].Name != "Aged Brie")
                     {
                         if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                         {
                             if (Items[i].Quality > 0)
                             {
                                 if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                 {
                                     Items[i].Quality = Items[i].Quality - 1;
                                 }
                             }
                         }
                         else
                         {
                             Items[i].Quality = Items[i].Quality - Items[i].Quality;
                         }
                     }
                     else
                     {
                         if (Items[i].Quality < 50)
                         {
                             Items[i].Quality = Items[i].Quality + 1;
                         }
                     }
                 }
             }*/
        }
    };

    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }
    };

    public class ItemWrapper
    {
        public Item item;
        public bool isConjured;

        public ItemWrapper(Item item) 
        { 
            this.item = item;
            if (item.Name.ToLower().Contains("conjure"))
            {
                isConjured = true;
            }
            else
            {
                isConjured = false;
            }
        }

        public void update() 
        {
            --item.SellIn;
            updateQuality();

            if (item.SellIn < 0) // past sell date, update again
            {
                updateQuality();
            }
        }

        public virtual void updateQuality() { }
    }

    public class RegularItem : ItemWrapper
    {
        public override void updateQuality()
        {
            if (isConjured)
            {
                item.Quality -= 2;
            }
            else
            {
                item.Quality--;
            }

            if (item.Quality < 0)
            {
                item.Quality = 0;
            }
        }
    }

    public class AgedBrie : ItemWrapper
    {
        public override void updateQuality()
        {
            if (isConjured)
            {
                item.Quality += 2;
            }
            else 
            {
                item.Quality++;
            }

            if (item.Quality > 50)
            {
                item.Quality = 50;
            }
        }
    }

    public class Sulfuras : ItemWrapper { };

    public class BackstagePass : ItemWrapper
    {
        public override void updateQuality()
        {
            if (item.SellIn == 0)
            {
                item.Quality = 0;
            }
            else if (item.SellIn <= 5)
            {
                if (isConjured)
                {
                    item.Quality += 6;
                }
                else
                {
                    item.Quality += 3;
                }
            }
            else if (item.SellIn <= 10)
            {
                if (isConjured)
                {
                    item.Quality += 4;
                }
                else
                {
                    item.Quality += 2;
                }
            }
            else
            {
                if (isConjured)
                {
                    item.Quality += 2;
                }
                else
                {
                    item.Quality++;
                }
            }

            if (item.Quality > 50)
            {
                item.Quality = 50;
            }
        }
    };
}
