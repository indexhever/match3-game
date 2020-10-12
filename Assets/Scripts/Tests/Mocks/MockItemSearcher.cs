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
        private Func<Item> searcher;
        public MockItemSearcher(Func<Item> searcher)
        {
            this.searcher = searcher;
        }

        public Item GetItemAbove(Item givenItem)
        {
            return searcher();
        }

        public Item GetItemLeft(Item givenItem)
        {
            return searcher();
        }

        public Item GetItemRight(Item givenItem)
        {
            return searcher();
        }

        public Item GetItemUnder(Item givenItem)
        {
            return searcher();
        }

        public void SwapItems(Item selectedItem, Item itemSwapped)
        {
            
        }
    }
}