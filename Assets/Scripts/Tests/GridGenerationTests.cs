using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GridFramework;

namespace Tests.Unit.Grid
{
    public class GridGenerationTests
    {
        [Test]
        public void GridHasRowsAndColumns()
        {
            int rows = 3;
            int columns = 2;
            ItemFactory itemFactory = CreateItemFactory(new Vector2(1, 2));
            Vector2 origin = Vector2.zero;
            GameGrid grid = CreateGameGrid(rows, columns, itemFactory, origin, 1);

            grid.GenerateItems();

            Assert.AreEqual(rows, grid.Rows);
            Assert.AreEqual(columns, grid.Columns);
        }

        [Test]
        public void GetFirstItemByRowAndColumn()
        {
            int rows = 3;
            int columns = 3;
            ItemFactory itemFactory = CreateItemFactory(new Vector2(1, 2));
            Vector2 origin = Vector2.zero;
            GameGrid grid = CreateGameGrid(rows, columns, itemFactory, origin, 1);
            grid.GenerateItems();

            Item item = grid.GetItemByRowColumn(0, 0);

            Assert.IsNotNull(item);
        }

        private ItemFactory CreateItemFactory(Vector2 itemMeasuresInUnit)
        {
            return new MockItemFactory(itemMeasuresInUnit);
        }

        private GameGrid CreateGameGrid(int rows, int columns, ItemFactory itemFactory, Vector2 origin, float offsetBetweenItems)
        {
            return new GameGrid(rows, columns, itemFactory, origin, offsetBetweenItems);
        }
    }
}
