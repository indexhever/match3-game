using GridFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public class GridBasedItemSearcher : ItemSearcher
    {
        private GameGrid grid;

        public GridBasedItemSearcher(GameGrid grid)
        {
            this.grid = grid;
        }

        public Item GetItemAbove(Item givenItem)
        {
            // TODO: return NulItem if row and column is out of bounds of grid
            return grid.GetItemByRowColumn(givenItem.Row - 1, givenItem.Column);
        }

        public Item GetItemUnder(Item givenItem)
        {
            return grid.GetItemByRowColumn(givenItem.Row + 1, givenItem.Column);
        }

        public Item GetItemRight(Item givenItem)
        {
            return grid.GetItemByRowColumn(givenItem.Row, givenItem.Column + 1);
        }

        public Item GetItemLeft(Item givenItem)
        {
            return grid.GetItemByRowColumn(givenItem.Row, givenItem.Column - 1);
        }
    }
}