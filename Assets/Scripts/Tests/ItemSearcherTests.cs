using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Math3Game;
using Math3Game.Controller;
using GridFramework;
using System;
using Math3Game.View;

namespace Tests.Unit
{
    public class ItemSearcherTests
    {
        [Test]
        public void GetItemAboveWhenThereIsOneAbove()
        {
            GameGrid<Gem> grid = CreateGrid(3, 3);
            grid.GenerateItems();
            ItemSearcher<Gem> itemSearcher = CreateItemSearcher(grid);
            Gem givenItem = grid.GetItemByRowColumn(1, 1);
            Gem expectedItemUnder = grid.GetItemByRowColumn(0, 1);

            Gem itemAboveGivenOne = itemSearcher.GetItemAbove(givenItem);

            Assert.IsNotNull(itemAboveGivenOne);
            Assert.AreEqual(expectedItemUnder, itemAboveGivenOne);
        }

        [Test]
        public void GetItemUnderWhenThereIsOneUnder()
        {
            GameGrid<Gem> grid = CreateGrid(3, 3);
            grid.GenerateItems();
            ItemSearcher<Gem> itemSearcher = CreateItemSearcher(grid);
            Gem givenItem = grid.GetItemByRowColumn(1, 1);
            Gem expectedItemAbove = grid.GetItemByRowColumn(2, 1);

            Gem itemUnderGivenOne = itemSearcher.GetItemUnder(givenItem);

            Assert.IsNotNull(itemUnderGivenOne);
            Assert.AreEqual(expectedItemAbove, itemUnderGivenOne);
        }

        [Test]
        public void GetItemRightWhenThereIsOneOnTheRight()
        {
            GameGrid<Gem> grid = CreateGrid(3, 3);
            grid.GenerateItems();
            ItemSearcher<Gem> itemSearcher = CreateItemSearcher(grid);
            Gem givenItem = grid.GetItemByRowColumn(1, 1);
            Gem expectedItemOnTheRight = grid.GetItemByRowColumn(1, 2);

            Gem itemOnTheRightOfGivenOne = itemSearcher.GetItemRight(givenItem);

            Assert.IsNotNull(itemOnTheRightOfGivenOne);
            Assert.AreEqual(expectedItemOnTheRight, itemOnTheRightOfGivenOne);
        }

        [Test]
        public void GetItemLeftWhenThereIsOneOnTheLeft()
        {
            GameGrid<Gem> grid = CreateGrid(3, 3);
            grid.GenerateItems();
            ItemSearcher<Gem> itemSearcher = CreateItemSearcher(grid);
            Gem givenItem = grid.GetItemByRowColumn(1, 1);
            Gem expectedItemOnTheLeft = grid.GetItemByRowColumn(1, 0);

            Gem itemOnTheLeftOfGivenOne = itemSearcher.GetItemLeft(givenItem);

            Assert.IsNotNull(itemOnTheLeftOfGivenOne);
            Assert.AreEqual(expectedItemOnTheLeft, itemOnTheLeftOfGivenOne);
        }

        private GameGrid<Gem> CreateGrid(int rows, int columns)
        {
            return new GameGrid<Gem>(
                rows, 
                columns, 
                new MockItemFactory(new Vector2(2, 2)), 
                Vector2.zero, 
                1);
        }

        private ItemSearcher<Gem> CreateItemSearcher(GameGrid<Gem> grid)
        {
            return new GridBasedItemSearcher(grid);
        }

        private Item CreateItem()
        {
            throw new NotImplementedException();
        }
    }
}
