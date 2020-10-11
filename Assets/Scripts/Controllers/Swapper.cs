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
        private Item selectedItem;
        private Item itemSwapped;
        private Vector2 itemInitialPosition;
        private Vector2 itemSwappedInitialPosition;

        public Swapper(ItemSearcher itemSearcher)
        {
            this.itemSearcher = itemSearcher;
        }

        public void Initialize(Item selectedItem)
        {
            this.selectedItem = selectedItem;
            this.itemInitialPosition = selectedItem.Position;
        }

        public void SwapUp()
        {
            itemSwapped = itemSearcher.GetItemAbove(selectedItem);
            itemSwappedInitialPosition = itemSwapped.Position;
            Vector2 selectedItemCurrentPosition = selectedItem.Position;
            selectedItem.Position = itemSwapped.Position;
            itemSwapped.Position = selectedItemCurrentPosition;
        }

        public void SwapRight()
        {
            itemSwapped = itemSearcher.GetItemRight(selectedItem);
            itemSwappedInitialPosition = itemSwapped.Position;
            Vector2 selectedItemCurrentPosition = selectedItem.Position;
            selectedItem.Position = itemSwapped.Position;
            itemSwapped.Position = selectedItemCurrentPosition;
        }

        public void SwapDown()
        {
            itemSwapped = itemSearcher.GetItemUnder(selectedItem);
            itemSwappedInitialPosition = itemSwapped.Position;
            Vector2 selectedItemCurrentPosition = selectedItem.Position;
            selectedItem.Position = itemSwapped.Position;
            itemSwapped.Position = selectedItemCurrentPosition;
        }

        public void SwapLeft()
        {
            itemSwapped = itemSearcher.GetItemLeft(selectedItem);
            itemSwappedInitialPosition = itemSwapped.Position;
            Vector2 selectedItemCurrentPosition = selectedItem.Position;
            selectedItem.Position = itemSwapped.Position;
            itemSwapped.Position = selectedItemCurrentPosition;
        }

        public void Reset()
        {
            if (selectedItem == null || itemSwapped == null)
                return;
            if (selectedItem.Position == itemInitialPosition && itemSwapped.Position == itemSwappedInitialPosition)
                return;

            selectedItem.Position = itemInitialPosition;
            itemSwapped.Position = itemSwappedInitialPosition;
        }
    }
}