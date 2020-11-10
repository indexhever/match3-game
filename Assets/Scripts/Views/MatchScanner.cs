using GridFramework;
using Math3Game.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Math3Game.View
{
    public class MatchScanner : MonoBehaviour, MatchScannerTrigger
    {
        private GameGrid<Gem> grid;
        private SwappingInputSwitch swappingInputSwitch;
        private BoardUpdater boardUpdater;
        private MatchSoundController matchSoundController;
        private Stack<Gem> rowItemMatcheds;
        private List<Stack<Gem>> columnItemMatcheds;
        private List<Gem> itemsToBeDisposed;
        private bool thereWasAMatch;

        [Inject]
        private void Construct(
            GameGrid<Gem> grid, 
            SwappingInputSwitch swappingInputSwitch, 
            BoardUpdater boardUpdater, 
            MatchSoundController matchSoundController)
        {
            this.grid = grid;
            this.swappingInputSwitch = swappingInputSwitch;
            this.boardUpdater = boardUpdater;
            this.matchSoundController = matchSoundController;
            itemsToBeDisposed = new List<Gem>();
        }

        private void Start()
        {
            rowItemMatcheds = new Stack<Gem>();
            // initialize column stacks
            columnItemMatcheds = new List<Stack<Gem>>();
            for(int i = 0; i < grid.Columns; i++)
            {
                columnItemMatcheds.Add(new Stack<Gem>());
            }
            boardUpdater.SignOnUpdateComplete(OnBoardUpdateComplete);
        }

        public void Scan()
        {
            StartCoroutine(ScanCoroutine());
        }

        private IEnumerator ScanCoroutine()
        {
            StopSwappingInput();
            Gem currentItem;
            thereWasAMatch = false;
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int column = 0; column < grid.Columns; column++)
                {
                    currentItem = grid.GetItemByRowColumn(row, column);
                    ScanItemStackWithItem(rowItemMatcheds, currentItem);
                    ScanItemStackWithItem(columnItemMatcheds[column], currentItem);
                    if (row == grid.Rows - 1)
                        CleanStack(columnItemMatcheds[column]);
                    yield return null;
                }
                CleanStack(rowItemMatcheds);
                yield return null;
            }

            if (thereWasAMatch)
            {
                yield return DisposeItems();
            }
            yield return CleanAllStacks();
            OnScanningEnd();
        }

        private void OnScanningEnd()
        {
            if (thereWasAMatch)
                RunGridUpdater();
            else
                ReturnSwappingInput();
        }

        private void ScanItemStackWithItem(Stack<Gem> previousMatchingItems, Gem newItem)
        {
            if (previousMatchingItems.Count == 0 || IsNewItemStillMatchingPreviousOnes(newItem, previousMatchingItems))
            {
                previousMatchingItems.Push(newItem);
                return;
            }

            CleanStack(previousMatchingItems);
            previousMatchingItems.Push(newItem);            
        }

        private static bool IsNewItemStillMatchingPreviousOnes(Gem item, Stack<Gem> previousMatchingItems)
        {
            return previousMatchingItems.Peek().Equals(item);
        }

        private void CleanStack(Stack<Gem> itemMatchedStack)
        {
            if (IsThereAMatchInStack(itemMatchedStack))
            {
                thereWasAMatch = true;
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
            foreach (Gem itemToDispose in itemsToBeDisposed)
            {
                itemToDispose.Dispose();
                yield return null;
            }
            itemsToBeDisposed.Clear();
        }

        private void SetDisposeItemsFromStack(Stack<Gem> itemMatchedStack)
        {
            int amountOfItemsToDespawn = itemMatchedStack.Count;
            Gem currentItem;
            for (int i = 0; i < amountOfItemsToDespawn; i++)
            {
                currentItem = itemMatchedStack.Pop();
                AddItemToBeDisposed(currentItem);
            }
        }

        private void AddItemToBeDisposed(Gem currentItem)
        {
            itemsToBeDisposed.Add(currentItem);
        }

        private static bool IsThereAMatchInStack(Stack<Gem> itemMatchedStack)
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

        public void OnBoardUpdateComplete()
        {
            Scan();
        }
    }
}