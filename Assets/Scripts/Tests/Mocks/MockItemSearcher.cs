using GridFramework;
using Math3Game.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class MockItemSearcher : ItemSearcher
    {
        private Func<Item> getItemRight;
        private Func<Item> getItemLeft;
        private Func<Item> getItemUp;
        private Func<Item> getItemDown;
        private string neighborType;

        public bool HasItemSwapped;

        public MockItemSearcher
        (
            Func<Item> getItemRight, 
            Func<Item> getItemLeft, 
            Func<Item> getItemUp, 
            Func<Item> getItemDown, 
            string neighborType = ""
        )
        {
            this.getItemRight = getItemRight;
            this.getItemLeft = getItemLeft;
            this.getItemUp = getItemUp;
            this.getItemDown = getItemDown;
            this.neighborType = neighborType;
        }

        public Item GetItemAbove(Item givenItem)
        {
            return getItemUp();
        }

        public Item GetItemLeft(Item givenItem)
        {
            return getItemLeft();
        }

        public Item GetItemRight(Item givenItem)
        {
            return getItemRight();
        }

        public Item GetItemUnder(Item givenItem)
        {
            return getItemDown();
        }

        public void SwapItems(Item selectedItem, Item itemSwapped)
        {
            HasItemSwapped = neighborType == (selectedItem as MockItem).GemType;
        }
    }
}