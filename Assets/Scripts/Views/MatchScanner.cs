using GridFramework;
using Math3Game.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Math3Game.View
{
    public class MatchScanner : MonoBehaviour, MatchScannerTrigger
    {
        private GameGrid grid;
        private SwappingInputSwitch swappingInputSwitch;
        private BoardUpdater boardUpdater;
        private MatchSoundController matchSoundController;
        private readonly Stack<Item> rowItemMatcheds = new Stack<Item>();
        private readonly List<Stack<Item>> columnItemMatcheds = new List<Stack<Item>>();
        private List<Item> itemsToBeDisposed;
        private bool thereWasAMatch;

        [Inject]
        private void Construct(
            GameGrid grid, 
            SwappingInputSwitch swappingInputSwitch, 
            BoardUpdater boardUpdater, 
            MatchSoundController matchSoundController)
        {
            this.grid = grid;
            this.swappingInputSwitch = swappingInputSwitch;
            this.boardUpdater = boardUpdater;
            this.matchSoundController = matchSoundController;
            itemsToBeDisposed = new List<Item>();
        }

        private void Start()
        {
            for(int i = 0; i < grid.Columns; i++)
            {
                columnItemMatcheds.Add(new Stack<Item>());
            }
            boardUpdater.SignOnUpdateComplete(OnBoardUpdateComplete);
        }

        public void Scan()
        {
            StartCoroutine(ScanCoroutine());
        }

        private IEnumerator ScanCoroutine()
        {
            yield return CleanAllStacks();
            
            StopSwappingInput();
            Item currentItem = null;
            thereWasAMatch = false;
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int column = 0; column < grid.Columns; column++)
                {
                    currentItem = grid.GetItemByRowColumn(row, column);
                    if (currentItem is NullItem) {
                        yield return null;
                    } else {
                        ScanItemStackWithItem(rowItemMatcheds, currentItem);
                        ScanItemStackWithItem(columnItemMatcheds[column], currentItem);
                        yield return null;
                    }
                }
                yield return null;
            }

            yield return DisposeItems();
            //after get all disposable items, we need to update rows for affected gems exists on the top 
            grid.UpdateRows(itemsToBeDisposed);
            OnScanningEnd();
        }

        private void OnScanningEnd()
        {
            if (thereWasAMatch)
                RunGridUpdater();
            else
                ReturnSwappingInput();
        }

        private void ScanItemStackWithItem(Stack<Item> previousMatchingItems, Item newItem)
        {
            if (previousMatchingItems.Count == 0 || IsNewItemStillMatchingPreviousOnes(newItem, previousMatchingItems))
            {
                previousMatchingItems.Push(newItem);
                return;
            }

            CleanStack(previousMatchingItems);
            previousMatchingItems.Push(newItem);            
        }

        private static bool IsNewItemStillMatchingPreviousOnes(Item item, Stack<Item> previousMatchingItems)
        {
            return previousMatchingItems.Peek().Equals(item);
        }

        private void CleanStack(Stack<Item> itemMatchedStack)
        {
            if (IsThereAMatchInStack(itemMatchedStack))
            {
                SetDisposeItemsFromStack(itemMatchedStack);
            }
            else
            {
                itemMatchedStack.Clear();                
            }            
        }

        private IEnumerator CleanAllStacks()
        {
            rowItemMatcheds.Clear();
            for (int i = 0; i < grid.Columns; i++)
            {
                columnItemMatcheds[i].Clear();
                yield return null;
            }
        }

        private IEnumerator DisposeItems()
        {
            matchSoundController.PlayMatchSound();
            foreach (Item itemToDispose in itemsToBeDisposed)
            {
                itemToDispose.Dispose();
                yield return null;
            }
            //itemsToBeDisposed.Clear();
        }

        private void SetDisposeItemsFromStack(Stack<Item> itemMatchedStack)
        {
            thereWasAMatch = true;
            int amountOfItemsToDespawn = itemMatchedStack.Count;
            Item currentItem;
            for (int i = 0; i < amountOfItemsToDespawn; i++)
            {
                currentItem = itemMatchedStack.Pop();
                AddItemToBeDisposed(currentItem);
            }
        }

        private void AddItemToBeDisposed(Item currentItem)
        {
            itemsToBeDisposed.Add(currentItem);
        }

        private static bool IsThereAMatchInStack(Stack<Item> itemMatchedStack)
        {
            return itemMatchedStack.Count >= 3;
        }

        private void StopSwappingInput()
        {
            swappingInputSwitch.TurnOff();
        }

        private void ReturnSwappingInput()
        {
            swappingInputSwitch.TurnOn();
        }

        private void RunGridUpdater()
        {
            boardUpdater.Run();
        }

        private void OnBoardUpdateComplete()
        {
            StopAllCoroutines();
            DestroyOldItems();
            grid.LogGrid();
            Scan();
        }

        private void DestroyOldItems() {
            foreach (Item itemToDispose in itemsToBeDisposed)
            {
                itemToDispose.Destroy();
            }
            itemsToBeDisposed.Clear();
        }
    }
}