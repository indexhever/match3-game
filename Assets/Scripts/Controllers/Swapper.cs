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
        private SwapSoundController swapSoundController;
        private GameGrid<Slot> slotGrid;
        private Gem selectedItem;
        private Gem itemSwapped;
        private Vector2 itemInitialPosition;
        private Vector2 itemSwappedInitialPosition;
        private bool hasSelectedArrivedAtSlot;
        private bool hasItemSwappedArrivedAtSlot;
        private WaitUntil waitUntilBothGemsArriveAtDestination;
        private Action<IEnumerator> startCoroutine;

        public Swapper(
            ItemSearcher<Gem> itemSearcher, 
            MatchScannerTrigger matchScannerTrigger, 
            SwapSoundController swapSoundController,
            GameGrid<Slot> slotGrid,
            Action<IEnumerator> startCoroutine)
        {
            this.itemSearcher = itemSearcher;
            this.matchScannerTrigger = matchScannerTrigger;
            this.swapSoundController = swapSoundController;
            this.slotGrid = slotGrid;
            this.startCoroutine = startCoroutine;
            waitUntilBothGemsArriveAtDestination = new WaitUntil(() => hasItemSwappedArrivedAtSlot && hasSelectedArrivedAtSlot);
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
            if (itemSwapped.Row == -1)
                return;

            swapSoundController.PlaySwapSound();
            itemSwappedInitialPosition = itemSwapped.Position;
            Vector2 selectedItemCurrentPosition = selectedItem.Position;
            //selectedItem.Position = itemSwapped.Position;
            //itemSwapped.Position = selectedItemCurrentPosition;
            selectedItem.MoveToPosition(itemSwapped.Position, () => hasSelectedArrivedAtSlot = true);
            itemSwapped.MoveToPosition(selectedItemCurrentPosition, () => hasItemSwappedArrivedAtSlot = true);

            itemSearcher.SwapItems(selectedItem, itemSwapped);

            // set slot for both gems
            //slotGrid.GetItemByRowColumn(selectedItem.Row, selectedItem.Column)
            //        .ReceiveGem(selectedItem);
            //slotGrid.GetItemByRowColumn(itemSwapped.Row, itemSwapped.Column)
            //        .ReceiveGem(itemSwapped);
        }

        public void Reset()
        {
            if (selectedItem == null || itemSwapped == null)
                return;
            if (itemSwapped.Row == -1)
                return;
            if (selectedItem.Position == itemInitialPosition && itemSwapped.Position == itemSwappedInitialPosition)
                return;

            //selectedItem.Position = itemInitialPosition;
            //itemSwapped.Position = itemSwappedInitialPosition;
            selectedItem.MoveToPosition(itemInitialPosition);
            itemSwapped.MoveToPosition(itemSwappedInitialPosition);
            itemSearcher.SwapItems(selectedItem, itemSwapped);

            // set slot for both gems
            //slotGrid.GetItemByRowColumn(selectedItem.Row, selectedItem.Column)
            //        .ReceiveGem(selectedItem);
            //slotGrid.GetItemByRowColumn(itemSwapped.Row, itemSwapped.Column)
            //        .ReceiveGem(itemSwapped);
        }

        public void CompleteSwap()
        {
            if (!CanSwap())
            {
                Reset();
                return;
            }

            startCoroutine(StartScanning());
        }

        private IEnumerator StartScanning()
        {
            yield return waitUntilBothGemsArriveAtDestination;

            matchScannerTrigger.Scan();
        }
        
        private bool CanSwap()
        {
            if (itemSwapped == null || itemSwapped.Row == -1)
                return false;

            return HasHorizontalMatch() || HasVerticalMatch();
        }

        private bool HasHorizontalMatch()
        {
            return IsInHorizontalMiddle() || IsInHorizontalBorder();
        }

        private bool IsInHorizontalMiddle()
        {
            return IsItemInHorizontalMiddleOfEqualItem(selectedItem) || IsItemInHorizontalMiddleOfEqualItem(itemSwapped);
        }

        private bool IsItemInHorizontalMiddleOfEqualItem(Gem gem)
        {
            return (itemSearcher.GetItemLeft(gem).Equals(gem)
                            && itemSearcher.GetItemRight(gem).Equals(gem));
        }

        private bool IsInHorizontalBorder()
        {
            return IsMatchingToTheRight() || IsMatchingToTheLeft();
        }

        private bool IsMatchingToTheLeft()
        {
            return CheckMatchToTheLeftOf(selectedItem) || CheckMatchToTheLeftOf(itemSwapped);
        }

        private bool IsMatchingToTheRight()
        {
            return CheckMatchToTheRightOf(selectedItem) || CheckMatchToTheRightOf(itemSwapped);
        }

        private bool CheckMatchToTheRightOf(Gem gemToCheckEquality)
        {
            Gem currentGem = gemToCheckEquality;
            bool hasMatch = true;
            for (int i = 0; i < 2; i++)
            {
                currentGem = itemSearcher.GetItemRight(currentGem);

                if (!currentGem.Equals(gemToCheckEquality))
                {
                    hasMatch = false;
                    break;
                }
            }
            return hasMatch;
        }

        private bool CheckMatchToTheLeftOf(Gem gemToCheckEquality)
        {
            Gem currentGem = gemToCheckEquality;
            bool hasMatch = true;
            for (int i = 0; i < 2; i++)
            {
                currentGem = itemSearcher.GetItemLeft(currentGem);

                if (!currentGem.Equals(gemToCheckEquality))
                {
                    hasMatch = false;
                    break;
                }
            }
            return hasMatch;
        }

        private bool HasVerticalMatch()
        {
            return IsInVerticalMiddle() || IsInVerticalBorder();
        }

        private bool IsInVerticalMiddle()
        {
            return IsItemInVerticalMiddleOfEqualItem(selectedItem) || IsItemInVerticalMiddleOfEqualItem(itemSwapped);
        }

        private bool IsItemInVerticalMiddleOfEqualItem(Gem gem)
        {
            return (itemSearcher.GetItemAbove(gem).Equals(gem)
                            && itemSearcher.GetItemUnder(gem).Equals(gem));
        }

        private bool IsInVerticalBorder()
        {
            return CheckMatchAboveOf(selectedItem) || CheckMatchUnderOf(selectedItem)
                || CheckMatchAboveOf(itemSwapped) || CheckMatchUnderOf(itemSwapped);
        }

        private bool CheckMatchAboveOf(Gem gemToCheckEquality)
        {
            Gem currentGem = gemToCheckEquality;
            bool hasMatch = true;
            for (int i = 0; i < 2; i++)
            {
                currentGem = itemSearcher.GetItemAbove(currentGem);

                if (!currentGem.Equals(gemToCheckEquality))
                {
                    hasMatch = false;
                    break;
                }
            }
            return hasMatch;
        }

        private bool CheckMatchUnderOf(Gem gemToCheckEquality)
        {
            Gem currentGem = gemToCheckEquality;
            bool hasMatch = true;
            for (int i = 0; i < 2; i++)
            {
                currentGem = itemSearcher.GetItemUnder(currentGem);

                if (!currentGem.Equals(gemToCheckEquality))
                {
                    hasMatch = false;
                    break;
                }
            }
            return hasMatch;
        }
    }
}