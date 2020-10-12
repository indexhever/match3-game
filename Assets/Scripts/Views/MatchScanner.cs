using GridFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class MatchScanner : MonoBehaviour
    {
        private GameGrid grid;
        private Stack<Item> rowItemMatcheds;
        private List<Stack<Item>> columnItemMatcheds;

        [Inject]
        private void Construct(GameGrid grid)
        {
            this.grid = grid;
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

            Scan();
        }

        public void Scan()
        {
            StartCoroutine(ScanCoroutine());
        }

        private IEnumerator ScanCoroutine()
        {
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
    }
}