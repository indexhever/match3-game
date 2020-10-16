using GridFramework;
using Math3Game.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class DefaultExtraItemSpawner : MonoBehaviour, ExtraItemSpawner
    {
        private GameGrid<Gem> gemGrid;
        private GameGrid<Slot> slotGrid;
        private ExtraGemFactory extraGemFactory;
        private ExtraItemsEntering extraItemsEntering;
        private int[] amountExtraItemsToCreatePerColumn;
        private BoardUpdater boardUpdater;
        private float previousYForSpawning;
        private int amountExtraItemsCreated;

        [SerializeField]
        private float amountSecondsWaitToSpawnAnother = .3f;        

        [Inject]
        private void Construct(
            GameGrid<Gem> gemGrid, 
            GameGrid<Slot> slotGrid,
            ExtraGemFactory extraGemFactory, 
            BoardUpdater boardUpdater,
            ExtraItemsEntering extraItemsEntering)
        {
            this.gemGrid = gemGrid;
            this.slotGrid = slotGrid;
            this.extraGemFactory = extraGemFactory;
            this.boardUpdater = boardUpdater;
            this.extraItemsEntering = extraItemsEntering;
            amountExtraItemsToCreatePerColumn = new int[gemGrid.Columns];
        }

        public void RequireGemPerColumn(int column)
        {
            amountExtraItemsToCreatePerColumn[column]++;
        }

        public void StartSpawning()
        {
            StartCoroutine(SpawnItemPerColumnCoroutine());
        }        

        private IEnumerator SpawnItemPerColumnCoroutine()
        {
            Stack<Gem> extraGemStack = new Stack<Gem>();
            bool hasEmptySlotInCurrentColumn = false;
            for (int column = 0; column < amountExtraItemsToCreatePerColumn.Length; column++)
            {
                while (amountExtraItemsToCreatePerColumn[column] > 0)
                {
                    hasEmptySlotInCurrentColumn = true;
                    extraGemStack.Push(SpawnItemAtColumn(column));
                    amountExtraItemsToCreatePerColumn[column]--;
                    amountExtraItemsCreated++;
                }
                if (!hasEmptySlotInCurrentColumn)
                    continue;
                yield return SetupStackOfItemsForColumn(column, extraGemStack);
                previousYForSpawning = 0;
                hasEmptySlotInCurrentColumn = false;
            }
            TellAmountItemsAreGoingToEnterGame();
            boardUpdater.Stop();
        }

        private void TellAmountItemsAreGoingToEnterGame()
        {
            extraItemsEntering.SetAmountItemsToEnter(amountExtraItemsCreated);
            amountExtraItemsCreated = 0;
        }

        /// <summary>
        /// Setup the stack of elements for a column.
        /// </summary>
        /// <param name="column"></param>
        private IEnumerator SetupStackOfItemsForColumn(int column, Stack<Gem> extraGems)
        {
            Slot currentSlot;
            Stack<Gem> gemsInNonEmptySlots = new Stack<Gem>();

            // Fill extra gems into gemsInNonEmptySlots
            while (extraGems.Count > 0)
            {
                gemsInNonEmptySlots.Push(extraGems.Pop());
            }

            // Fill empty slots and gemsInNonEmptySlots
            for (int row = 0; row < slotGrid.Rows; row++)
            {
                currentSlot = slotGrid.GetItemByRowColumn(row, column);
                if (!currentSlot.IsEmpty)
                {
                    gemsInNonEmptySlots.Push(currentSlot.Gem);
                }

                yield return null;
            }            

            // setup expected gem for each slot from column in descent order
            Gem currentGem;
            for (int row = slotGrid.Rows - 1; row > 0; row--)
            {
                currentSlot = slotGrid.GetItemByRowColumn(row, column);
                currentGem = gemsInNonEmptySlots.Pop();
                currentSlot.SetExpectedGem(currentGem);
                yield return null;
            }
        }

        private Gem SpawnItemAtColumn(int column)
        {
            Vector2 newPosition = GetPositionForColumn(column);
            Gem newGem = extraGemFactory.Create(newPosition);
            newGem.OnBoardUpdate();

            return newGem;
        }

        private Vector2 GetPositionForColumn(int column)
        {
            Vector2 firstItemPositionOfGridColumn = gemGrid.GetItemByRowColumn(0, column).Position;
            float newX = firstItemPositionOfGridColumn.x;
            float newY = extraGemFactory.MeasuresInUnit.y + firstItemPositionOfGridColumn.y + previousYForSpawning;
            previousYForSpawning += extraGemFactory.MeasuresInUnit.y;

            return new Vector2(newX, newY);
        }
    }
}