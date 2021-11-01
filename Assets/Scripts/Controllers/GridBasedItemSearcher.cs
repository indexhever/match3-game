using GridFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public class GridBasedItemSearcher : ItemSearcher
    {
        private readonly GameGrid grid;

        public GridBasedItemSearcher(GameGrid grid)
        {
            this.grid = grid;
        }

        public Item GetItemAbove(Item givenItem)
        {
           return givenItem == null ? new NullItem() : grid.GetItemByRowColumn(givenItem.Row - 1, givenItem.Column);
        }

        public Item GetItemUnder(Item givenItem)
        {
            return givenItem == null ? new NullItem() : grid.GetItemByRowColumn(givenItem.Row + 1, givenItem.Column);
        }

        public Item GetItemRight(Item givenItem)
        {
            return givenItem == null ? new NullItem() : grid.GetItemByRowColumn(givenItem.Row, givenItem.Column + 1);
        }

        public Item GetItemLeft(Item givenItem)
        {
            return givenItem == null ? new NullItem() : grid.GetItemByRowColumn(givenItem.Row, givenItem.Column - 1);
        }

        public void SwapItems(Item firstItem, Item secondItem)
        {
            grid.SwapItems(firstItem, secondItem);
        }
    }
}