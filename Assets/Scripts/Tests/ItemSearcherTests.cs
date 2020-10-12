using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Math3Game;
using Math3Game.Controller;
using GridFramework;
using System;

namespace Tests.Unit
{
    public class ItemSearcherTests
    {
        [Test]
        public void GetItemAboveWhenThereIsOneAbove()
        {
            GameGrid grid = CreateGrid(3, 3);
            grid.GenerateItems();
            ItemSearcher itemSearcher = CreateItemSearcher(grid);
            Item givenItem = grid.GetItemByRowColumn(1, 1);
            Item expectedItemUnder = grid.GetItemByRowColumn(0, 1);

            Item itemAboveGivenOne = itemSearcher.GetItemAbove(givenItem);

            Assert.IsNotNull(itemAboveGivenOne);
            Assert.AreEqual(expectedItemUnder, itemAboveGivenOne);
        }

        [Test]
        public void GetItemUnderWhenThereIsOneUnder()
        {
            GameGrid grid = CreateGrid(3, 3);
            grid.GenerateItems();
            ItemSearcher itemSearcher = CreateItemSearcher(grid);
            Item givenItem = grid.GetItemByRowColumn(1, 1);
            Item expectedItemAbove = grid.GetItemByRowColumn(2, 1);

            Item itemUnderGivenOne = itemSearcher.GetItemUnder(givenItem);

            Assert.IsNotNull(itemUnderGivenOne);
            Assert.AreEqual(expectedItemAbove, itemUnderGivenOne);
        }

        [Test]
        public void GetItemRightWhenThereIsOneOnTheRight()
        {
            GameGrid grid = CreateGrid(3, 3);
            grid.GenerateItems();
            ItemSearcher itemSearcher = CreateItemSearcher(grid);
            Item givenItem = grid.GetItemByRowColumn(1, 1);
            Item expectedItemOnTheRight = grid.GetItemByRowColumn(1, 2);

            Item itemOnTheRightOfGivenOne = itemSearcher.GetItemRight(givenItem);

            Assert.IsNotNull(itemOnTheRightOfGivenOne);
            Assert.AreEqual(expectedItemOnTheRight, itemOnTheRightOfGivenOne);
        }

        [Test]
        public void GetItemLeftWhenThereIsOneOnTheLeft()
        {
            GameGrid grid = CreateGrid(3, 3);
            grid.GenerateItems();
            ItemSearcher itemSearcher = CreateItemSearcher(grid);
            Item givenItem = grid.GetItemByRowColumn(1, 1);
            Item expectedItemOnTheLeft = grid.GetItemByRowColumn(1, 0);

            Item itemOnTheLeftOfGivenOne = itemSearcher.GetItemLeft(givenItem);

            Assert.IsNotNull(itemOnTheLeftOfGivenOne);
            Assert.AreEqual(expectedItemOnTheLeft, itemOnTheLeftOfGivenOne);
        }

        private GameGrid CreateGrid(int rows, int columns)
        {
            return new GameGrid(
                rows, 
                columns, 
                new MockItemFactory(new Vector2(2, 2)), 
                Vector2.zero, 
                1);
        }

        private ItemSearcher CreateItemSearcher(GameGrid grid)
        {
            return new GridBasedItemSearcher(grid);
        }

        private Item CreateItem()
        {
            throw new NotImplementedException();
        }
    }
}
