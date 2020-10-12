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
        private GameGrid grid;
        private SwappingInputSwitch swappingInputSwitch;
        private Stack<Item> rowItemMatcheds;
        private List<Stack<Item>> columnItemMatcheds;

        [Inject]
        private void Construct(GameGrid grid, SwappingInputSwitch swappingInputSwitch)
        {
            this.grid = grid;
            this.swappingInputSwitch = swappingInputSwitch;
        }

        private void Start()
        {
            rowItemMatcheds = new Stack<Item>();
            // initialize column stacks
            columnItemMatcheds = new List<Stack<Item>>();
            for(int i = 0; i < grid.Columns; i++)
            {
                columnItemMatcheds.Add(new Stack<Item>());
            }
        }

        public void Scan()
        {
            StartCoroutine(ScanCoroutine());
        }

        private IEnumerator ScanCoroutine()
        {
            StopUIInput();
            Item currentItem;
            for(int row = 0; row < grid.Rows; row++)
            {
                for(int column = 0; column < grid.Columns; column++)
                {
                    currentItem = grid.GetItemByRowColumn(row, column);
                    ScanItemStackWithItem(rowItemMatcheds, currentItem);
                    ScanItemStackWithItem(columnItemMatcheds[column], currentItem);

                    yield return null;
                }
                yield return null;
            }

            yield return CleanAllStacks();
            ReturnUIInput();
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
                DisposeItemsFromStack(itemMatchedStack);
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

        private void DisposeItemsFromStack(Stack<Item> itemMatchedStack)
        {
            int amountOfItemsToDespawn = itemMatchedStack.Count;
            Item currentItem;
            for (int i = 0; i < amountOfItemsToDespawn; i++)
            {
                currentItem = itemMatchedStack.Pop();
                currentItem.Dispose();
            }
        }

        private static bool IsThereAMatchInStack(Stack<Item> itemMatchedStack)
        {
            return itemMatchedStack.Count >= 3;
        }

        private void StopUIInput()
        {
            swappingInputSwitch.TurnOff();
        }

        private void ReturnUIInput()
        {
            swappingInputSwitch.TurnOn();
        }
    }
}