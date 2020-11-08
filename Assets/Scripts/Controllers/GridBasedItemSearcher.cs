using GridFramework;
using Math3Game.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public class GridBasedItemSearcher : ItemSearcher<Gem>
    {
        private GameGrid<Gem> grid;

        public GridBasedItemSearcher(GameGrid<Gem> grid)
        {
            this.grid = grid;
        }

        public Gem GetItemAbove(Gem givenItem)
        {
            return grid.GetItemByRowColumn(givenItem.Row - 1, givenItem.Column);
        }

        public Gem GetItemUnder(Gem givenItem)
        {
            return grid.GetItemByRowColumn(givenItem.Row + 1, givenItem.Column);
        }

        public Gem GetItemRight(Gem givenItem)
        {
            return grid.GetItemByRowColumn(givenItem.Row, givenItem.Column + 1);
        }

        public Gem GetItemLeft(Gem givenItem)
        {
            return grid.GetItemByRowColumn(givenItem.Row, givenItem.Column - 1);
        }

        public void SwapItems(Gem firstItem, Gem secondItem)
        {
            grid.SwapItems(firstItem, secondItem);
        }
    }
}