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
            ItemSearcher itemSearcher = CreateUpMockItemSearcher(() => itemAbove);
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
            ItemSearcher itemSearcher = CreateRightMockItemSearcher(() => itemOnTheRight);
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
            ItemSearcher itemSearcher = CreateDownMockItemSearcher(() => itemDown);
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
            ItemSearcher itemSearcher = CreateLeftMockItemSearcher(() => itemLeft);
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
            ItemSearcher itemSearcher = CreateUpMockItemSearcher(() => itemAbove);
            Swapper swapper = CreateSwapper(itemSearcher);
            swapper.Initialize(selectedItem);

            swapper.SwapUp();
            swapper.Reset();
            Vector2 selectedItemPosition = selectedItem.Position;
            Vector2 itemAbovePosition = itemAbove.Position;

            Assert.AreEqual(expectedSelectedItemPosition, selectedItemPosition);
            Assert.AreEqual(initialItemAbovePosition, itemAbovePosition);
        }

        // TODO: finish ending swapping tests
        // when releasing swapp if two itens can stay at their new places, there is a match and board is updated
        [Test]
        public void EndingSlideWhenThereIsAMatchWillSwappItemsPositions()
        {
            Vector2 expectedSelectedItemPosition = Vector2.up;
            Vector2 selectedItemInitialPosition = Vector2.zero;
            Item itemLeft = CreateItem(expectedSelectedItemPosition, "apple");
            Item selectedItem = CreateItem(selectedItemInitialPosition, "banana");
            string neighborType = "banana";
            ItemSearcher itemSearcher = CreateLeftMockItemSearcher(() => itemLeft, neighborType);
            Swapper swapper = CreateSwapper(itemSearcher);
            swapper.Initialize(selectedItem);

            swapper.SwapLeft();
            swapper.CompleteSwap();
            Vector2 selectedItemPosition = selectedItem.Position;
            Vector2 itemLeftPosition = itemLeft.Position;

            Assert.AreEqual(expectedSelectedItemPosition, selectedItemPosition);
            Assert.AreEqual(selectedItemInitialPosition, itemLeftPosition);
            Assert.IsTrue((itemSearcher as MockItemSearcher).HasItemSwapped);
        }

        [Test]
        public void EndingSlideWhenThereIsNotAMatchWillLeaveItemsAtInitialPlaces()
        {
            Vector2 expectedSelectedItemPosition = Vector2.up;
            Vector2 selectedItemInitialPosition = Vector2.zero;
            Item itemLeft = CreateItem(expectedSelectedItemPosition, "apple");
            Item selectedItem = CreateItem(selectedItemInitialPosition, "banana");
            string neighborType = "apple";
            ItemSearcher itemSearcher = CreateLeftMockItemSearcher(() => new NullItem(), neighborType);
            Swapper swapper = CreateSwapper(itemSearcher);
            swapper.Initialize(selectedItem);

            swapper.SwapLeft();
            swapper.CompleteSwap();
            Vector2 selectedItemPosition = selectedItem.Position;
            Vector2 itemLeftPosition = itemLeft.Position;

            Assert.AreEqual(selectedItemInitialPosition, selectedItemPosition);
            Assert.AreEqual(expectedSelectedItemPosition, itemLeftPosition);
            Assert.IsFalse((itemSearcher as MockItemSearcher).HasItemSwapped);
        }

        private Item CreateItem(Vector2 initialPosition, string gemType = "")
        {
            return new MockItem(initialPosition, gemType);
        }

        private Swapper CreateSwapper(ItemSearcher itemSearcher)
        {
            return new Swapper(itemSearcher, new MockMatchScannerTrigger());
        }

        private ItemSearcher CreateRightMockItemSearcher(Func<Item> getItemRight, string neighborType = "")
        {
            return CreateMockItemSearcher(
                        getItemRight,
                        () => new NullItem(),
                        () => new NullItem(),
                        () => new NullItem(),
                        neighborType);
        }

        private ItemSearcher CreateLeftMockItemSearcher(Func<Item> getItemLeft, string neighborType = "")
        {
            return CreateMockItemSearcher(
                        () => new NullItem(),
                        getItemLeft,
                        () => new NullItem(),
                        () => new NullItem(),
                        neighborType);
        }

        private ItemSearcher CreateUpMockItemSearcher(Func<Item> getItemUp, string neighborType = "")
        {
            return CreateMockItemSearcher(
                        () => new NullItem(),
                        () => new NullItem(),
                        getItemUp,
                        () => new NullItem(),
                        neighborType);
        }

        private ItemSearcher CreateDownMockItemSearcher(Func<Item> getItemDown, string neighborType = "")
        {
            return CreateMockItemSearcher(
                        () => new NullItem(),
                        () => new NullItem(),
                        () => new NullItem(),
                        getItemDown,
                        neighborType);
        }

        private ItemSearcher CreateMockItemSearcher
            (
                Func<Item> getItemRight, 
                Func<Item> getItemLeft, 
                Func<Item> getItemUp, 
                Func<Item> getItemDown, 
                string neighborType = "")
        {
            return new MockItemSearcher(getItemRight, getItemLeft, getItemUp, getItemDown, neighborType);
        }
    }
}
