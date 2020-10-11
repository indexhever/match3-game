using GridFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public class Swapper
    {
        private ItemSearcher itemSearcher;

        public Swapper(ItemSearcher itemSearcher)
        {
            this.itemSearcher = itemSearcher;
        }

        public void SwapUp(Item selectedItem)
        {
            Item aboveItem = itemSearcher.GetItemAbove(selectedItem);
            Vector2 selectedItemCurrentPosition = selectedItem.Position;
            selectedItem.Position = aboveItem.Position;
            aboveItem.Position = selectedItemCurrentPosition;
        }

        public void SwapRight(Item selectedItem)
        {
            Item rightItem = itemSearcher.GetItemRight(selectedItem);
            Vector2 selectedItemCurrentPosition = selectedItem.Position;
            selectedItem.Position = rightItem.Position;
            rightItem.Position = selectedItemCurrentPosition;
        }

        public void SwapDown(Item selectedItem)
        {
            Item downItem = itemSearcher.GetItemUnder(selectedItem);
            Vector2 selectedItemCurrentPosition = selectedItem.Position;
            selectedItem.Position = downItem.Position;
            downItem.Position = selectedItemCurrentPosition;
        }

        public void SwapLeft(Item selectedItem)
        {
            Item leftItem = itemSearcher.GetItemLeft(selectedItem);
            Vector2 selectedItemCurrentPosition = selectedItem.Position;
            selectedItem.Position = leftItem.Position;
            leftItem.Position = selectedItemCurrentPosition;
        }
    }
}