using System;
using System.Collections;
using System.Collections.Generic;
using GridFramework;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Math3Game.Controller;
using Math3Game.View;

namespace Tests.Unit
{
    public class SwappingTests
    {
        [Test]
        public void SwappingUpFromItemWillSwapItWithUpItem()
        {
            Vector2 expectedSelectedItemPosition = Vector2.up;
            Vector2 selectedItemInitialPosition = Vector2.zero;
            Gem itemAbove = CreateGem(expectedSelectedItemPosition);
            Gem selectedItem = CreateGem(selectedItemInitialPosition);
            ItemSearcher<Gem> itemSearcher = CreateUpMockItemSearcher(() => itemAbove);
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
            Gem itemOnTheRight = CreateGem(expectedSelectedItemPosition);
            Gem selectedItem = CreateGem(selectedItemInitialPosition);
            ItemSearcher<Gem> itemSearcher = CreateRightMockItemSearcher(() => itemOnTheRight);
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
            Gem itemDown = CreateGem(expectedSelectedItemPosition);
            Gem selectedItem = CreateGem(selectedItemInitialPosition);
            ItemSearcher<Gem> itemSearcher = CreateDownMockItemSearcher(() => itemDown);
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
            Gem itemLeft = CreateGem(expectedSelectedItemPosition);
            Gem selectedItem = CreateGem(selectedItemInitialPosition);
            ItemSearcher<Gem> itemSearcher = CreateLeftMockItemSearcher(() => itemLeft);
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
            Gem itemAbove = CreateGem(initialItemAbovePosition);
            Gem selectedItem = CreateGem(selectedItemInitialPosition);
            ItemSearcher<Gem> itemSearcher = CreateUpMockItemSearcher(() => itemAbove);
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
            Gem itemLeft = CreateGem(expectedSelectedItemPosition, "apple");
            Gem selectedItem = CreateGem(selectedItemInitialPosition, "banana");
            string neighborType = "banana";
            ItemSearcher<Gem> itemSearcher = CreateLeftMockItemSearcher(() => itemLeft, neighborType);
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
            Gem itemLeft = CreateGem(expectedSelectedItemPosition, "apple");
            Gem selectedItem = CreateGem(selectedItemInitialPosition, "banana");
            string neighborType = "apple";
            ItemSearcher<Gem> itemSearcher = CreateLeftMockItemSearcher(() => new NullGem(), neighborType);
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

        [Test]
        public void MatchScannerIsTriggeredAftwerCompleteSwapping()
        {
            Vector2 expectedSelectedItemPosition = Vector2.up;
            Vector2 selectedItemInitialPosition = Vector2.zero;
            Gem itemLeft = CreateGem(expectedSelectedItemPosition, "apple");
            Gem selectedItem = CreateGem(selectedItemInitialPosition, "banana");
            string neighborType = "banana";
            ItemSearcher<Gem> itemSearcher = CreateLeftMockItemSearcher(() => itemLeft, neighborType);
            MatchScannerTrigger matchScannerTrigger = CreateMatchScannerTrigger();
            Swapper swapper = CreateSwapper(itemSearcher, matchScannerTrigger);
            swapper.Initialize(selectedItem);

            swapper.SwapLeft();
            swapper.CompleteSwap();
            bool wasMatchScannerTriggerCalled = (matchScannerTrigger as MockMatchScannerTrigger).WasCalled;

            Assert.IsTrue(wasMatchScannerTriggerCalled);
        }

        private MatchScannerTrigger CreateMatchScannerTrigger()
        {
            return new MockMatchScannerTrigger();
        }

        private Gem CreateGem(Vector2 initialPosition, string gemType = "")
        {
            return new MockItem(initialPosition, gemType);
        }

        private Swapper CreateSwapper(ItemSearcher<Gem> itemSearcher)
        {
            return new Swapper(
                itemSearcher, 
                new MockMatchScannerTrigger(), 
                new MockSwapSoundController(),
                null, 
                (coroutine) => { });
        }
        private Swapper CreateSwapper(ItemSearcher<Gem> itemSearcher, MatchScannerTrigger matchScannerTrigger)
        {
            return new Swapper(itemSearcher, matchScannerTrigger, new MockSwapSoundController(), null, null);
        }

        private ItemSearcher<Gem> CreateRightMockItemSearcher(Func<Gem> getItemRight, string neighborType = "")
        {
            return CreateMockItemSearcher(
                        getItemRight,
                        () => new NullGem(),
                        () => new NullGem(),
                        () => new NullGem(),
                        neighborType);
        }

        private ItemSearcher<Gem> CreateLeftMockItemSearcher(Func<Gem> getItemLeft, string neighborType = "")
        {
            return CreateMockItemSearcher(
                        () => new NullGem(),
                        getItemLeft,
                        () => new NullGem(),
                        () => new NullGem(),
                        neighborType);
        }

        private ItemSearcher<Gem> CreateUpMockItemSearcher(Func<Gem> getItemUp, string neighborType = "")
        {
            return CreateMockItemSearcher(
                        () => new NullGem(),
                        () => new NullGem(),
                        getItemUp,
                        () => new NullGem(),
                        neighborType);
        }

        private ItemSearcher<Gem> CreateDownMockItemSearcher(Func<Gem> getItemDown, string neighborType = "")
        {
            return CreateMockItemSearcher(
                        () => new NullGem(),
                        () => new NullGem(),
                        () => new NullGem(),
                        getItemDown,
                        neighborType);
        }

        private ItemSearcher<Gem> CreateMockItemSearcher
            (
                Func<Gem> getItemRight, 
                Func<Gem> getItemLeft, 
                Func<Gem> getItemUp, 
                Func<Gem> getItemDown, 
                string neighborType = "")
        {
            return new MockItemSearcher(getItemRight, getItemLeft, getItemUp, getItemDown, neighborType);
        }
    }
}
