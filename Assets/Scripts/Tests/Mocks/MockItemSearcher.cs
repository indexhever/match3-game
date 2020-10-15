using GridFramework;
using Math3Game.Controller;
using Math3Game.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class MockItemSearcher : ItemSearcher<Gem>
    {
        private Func<Gem> getItemRight;
        private Func<Gem> getItemLeft;
        private Func<Gem> getItemUp;
        private Func<Gem> getItemDown;
        private string neighborType;

        public bool HasItemSwapped;

        public MockItemSearcher
        (
            Func<Gem> getItemRight, 
            Func<Gem> getItemLeft, 
            Func<Gem> getItemUp, 
            Func<Gem> getItemDown, 
            string neighborType = ""
        )
        {
            this.getItemRight = getItemRight;
            this.getItemLeft = getItemLeft;
            this.getItemUp = getItemUp;
            this.getItemDown = getItemDown;
            this.neighborType = neighborType;
        }

        public Gem GetItemAbove(Gem givenItem)
        {
            return getItemUp();
        }

        public Gem GetItemLeft(Gem givenItem)
        {
            return getItemLeft();
        }

        public Gem GetItemRight(Gem givenItem)
        {
            return getItemRight();
        }

        public Gem GetItemUnder(Gem givenItem)
        {
            return getItemDown();
        }

        public void SwapItems(Gem selectedItem, Gem itemSwapped)
        {
            HasItemSwapped = neighborType == (selectedItem as MockItem).GemType;
        }
    }
}