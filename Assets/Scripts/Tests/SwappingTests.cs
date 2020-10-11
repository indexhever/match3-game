using System;
using System.Collections;
using System.Collections.Generic;
using GridFramework;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Math3Game.Controller;

namespace Tests.Unit
{
    public class SwappingTests
    {
        [Test]
        public void SwappingUpFromItemWillSwapItWithUpItem()
        {
            Vector2 expectedSelectedItemPosition = Vector2.up;
            Vector2 selectedItemInitialPosition = Vector2.zero;
            Item itemAbove = CreateItem(expectedSelectedItemPosition);
            Item selectedItem = CreateItem(selectedItemInitialPosition);
            ItemSearcher itemSearcher = CreateMockItemSearcher(() => itemAbove);
            Swapper swapper = CreateSwapper(itemSearcher);
            swapper.Initialize(selectedItem);

            swapper.SwapUp();
            Vector2 selectedItemPosition = selectedItem.Position;

            Assert.AreEqual(expectedSelectedItemPosition, selectedItemPosition);
        }

        [Test]
        public void SwappingRightFromItemWillSwapItWithRightItem()
        {
            Vector2 expectedSelectedItemPosition = Vector2.up;
            Vector2 selectedItemInitialPosition = Vector2.zero;
            Item itemOnTheRight = CreateItem(expectedSelectedItemPosition);
            Item selectedItem = CreateItem(selectedItemInitialPosition);
            ItemSearcher itemSearcher = CreateMockItemSearcher(() => itemOnTheRight);
            Swapper swapper = CreateSwapper(itemSearcher);
            swapper.Initialize(selectedItem);

            swapper.SwapRight();
            Vector2 selectedItemPosition = selectedItem.Position;

            Assert.AreEqual(expectedSelectedItemPosition, selectedItemPosition);
        }

        [Test]
        public void SwappingDownFromItemWillSwapItWithDownItem()
        {
            Vector2 expectedSelectedItemPosition = Vector2.up;
            Vector2 selectedItemInitialPosition = Vector2.zero;
            Item itemDown = CreateItem(expectedSelectedItemPosition);
            Item selectedItem = CreateItem(selectedItemInitialPosition);
            ItemSearcher itemSearcher = CreateMockItemSearcher(() => itemDown);
            Swapper swapper = CreateSwapper(itemSearcher);
            swapper.Initialize(selectedItem);

            swapper.SwapDown();
            Vector2 selectedItemPosition = selectedItem.Position;

            Assert.AreEqual(expectedSelectedItemPosition, selectedItemPosition);
        }

        [Test]
        public void SwappingLeftFromItemWillSwapItWithLeftItem()
        {
            Vector2 expectedSelectedItemPosition = Vector2.up;
            Vector2 selectedItemInitialPosition = Vector2.zero;
            Item itemLeft = CreateItem(expectedSelectedItemPosition);
            Item selectedItem = CreateItem(selectedItemInitialPosition);
            ItemSearcher itemSearcher = CreateMockItemSearcher(() => itemLeft);
            Swapper swapper = CreateSwapper(itemSearcher);
            swapper.Initialize(selectedItem);

            swapper.SwapLeft();
            Vector2 selectedItemPosition = selectedItem.Position;

            Assert.AreEqual(expectedSelectedItemPosition, selectedItemPosition);
        }

        [Test]
        public void ResetingWillReturnSwappedItemsToInitialPlaces()
        {
            Vector2 initialItemAbovePosition = Vector2.up;
            Vector2 selectedItemInitialPosition = Vector2.zero;
            Vector2 expectedSelectedItemPosition = selectedItemInitialPosition;            
            Item itemAbove = CreateItem(initialItemAbovePosition);
            Item selectedItem = CreateItem(selectedItemInitialPosition);
            ItemSearcher itemSearcher = CreateMockItemSearcher(() => itemAbove);
            Swapper swapper = CreateSwapper(itemSearcher);
            swapper.Initialize(selectedItem);

            swapper.SwapUp();
            swapper.Reset();
            Vector2 selectedItemPosition = selectedItem.Position;
            Vector2 itemAbovePosition = itemAbove.Position;

            Assert.AreEqual(expectedSelectedItemPosition, selectedItemPosition);
            Assert.AreEqual(initialItemAbovePosition, itemAbovePosition);
        }

        // when releasing swapp if two itens can stay at their new places, there is a match and board is updated

        private Item CreateItem(Vector2 initialPosition)
        {
            return new MockItem(initialPosition);
        }

        private Swapper CreateSwapper(ItemSearcher itemSearcher)
        {
            return new Swapper(itemSearcher);
        }

        private ItemSearcher CreateMockItemSearcher(Func<Item> aboveSearcher)
        {
            return new MockItemSearcher(aboveSearcher);
        }
    }
}
