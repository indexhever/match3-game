using GridFramework;
using Math3Game.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public class Swapper
    {
        private ItemSearcher<Gem> itemSearcher;
        private MatchScannerTrigger matchScannerTrigger;
        private Gem selectedItem;
        private Gem itemSwapped;
        private Vector2 itemInitialPosition;
        private Vector2 itemSwappedInitialPosition;
        private SwapSoundController swapSoundController;

        public Swapper(ItemSearcher<Gem> itemSearcher, MatchScannerTrigger matchScannerTrigger, SwapSoundController swapSoundController)
        {
            this.itemSearcher = itemSearcher;
            this.matchScannerTrigger = matchScannerTrigger;
            this.swapSoundController = swapSoundController;
        }

        public void Initialize(Gem selectedItem)
        {
            this.selectedItem = selectedItem;
            this.itemInitialPosition = selectedItem.Position;
        }

        public void SwapUp()
        {
            itemSwapped = itemSearcher.GetItemAbove(selectedItem);
            Swap();
        }

        public void SwapRight()
        {
            itemSwapped = itemSearcher.GetItemRight(selectedItem);
            Swap();
        }

        public void SwapDown()
        {
            itemSwapped = itemSearcher.GetItemUnder(selectedItem);
            Swap();
        }

        public void SwapLeft()
        {
            itemSwapped = itemSearcher.GetItemLeft(selectedItem);
            Swap();
        }

        private void Swap()
        {
            swapSoundController.PlaySwapSound();
            itemSwappedInitialPosition = itemSwapped.Position;
            Vector2 selectedItemCurrentPosition = selectedItem.Position;
            selectedItem.Position = itemSwapped.Position;
            itemSwapped.Position = selectedItemCurrentPosition;

            itemSearcher.SwapItems(selectedItem, itemSwapped);
        }

        public void Reset()
        {
            if (selectedItem == null || itemSwapped == null)
                return;
            if (selectedItem.Position == itemInitialPosition && itemSwapped.Position == itemSwappedInitialPosition)
                return;

            selectedItem.Position = itemInitialPosition;
            itemSwapped.Position = itemSwappedInitialPosition;
            itemSearcher.SwapItems(selectedItem, itemSwapped);
        }

        public void CompleteSwap()
        {
            if (!CanSwap())
            {
                Reset();
                return;
            }

            matchScannerTrigger.Scan();
        }

        private bool CanSwap()
        {
            return CanSwapRight() || CanSwapLeft() || CanSwapUp() || CanSwapDown();
        }

        private bool CanSwapRight()
        {
            return CanSwapRoutine(itemSearcher.GetItemRight);
        }

        private bool CanSwapLeft()
        {
            return CanSwapRoutine(itemSearcher.GetItemLeft);
        }

        private bool CanSwapUp()
        {
            return CanSwapRoutine(itemSearcher.GetItemAbove);
        }

        private bool CanSwapDown()
        {
            return CanSwapRoutine(itemSearcher.GetItemUnder);
        }

        private bool CanSwapRoutine(Func<Gem, Gem> getNeighborOf)
        {
            bool isTherAMatchInOneSide = true;
            bool isTherAMatchInOtherSide = true;
            Gem foundItemSwappedNeighbor = itemSwapped;
            Gem foundSelectedItemNeighbor = selectedItem;
            for (int i = 0; i < 2; i++)
            {
                // check if new neighbor of itemSwapped match its type
                foundItemSwappedNeighbor = getNeighborOf(foundItemSwappedNeighbor);
                if (!foundItemSwappedNeighbor.Equals(itemSwapped))
                {
                    isTherAMatchInOneSide = false;
                }

                // check if new neighbor of selectedItem match its type
                foundSelectedItemNeighbor = getNeighborOf(foundSelectedItemNeighbor);
                if (!foundSelectedItemNeighbor.Equals(selectedItem))
                {
                    isTherAMatchInOtherSide = false;
                }
            }

            return isTherAMatchInOneSide || isTherAMatchInOtherSide;
        }
    }
}