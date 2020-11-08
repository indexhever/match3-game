using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using GridFramework;
using System;

namespace Math3Game.View
{
    public class GridSetup : MonoBehaviour
    {
        private GameGrid<Gem> gemGrid;
        private GameGrid<Slot> slotGrid;
        private MatchScannerTrigger matchScannerTrigger;

        [Inject]
        private void Construct(GameGrid<Gem> gemGrid, GameGrid<Slot> slotGrid, MatchScannerTrigger matchScannerTrigger)
        {
            this.gemGrid = gemGrid;
            this.slotGrid = slotGrid;
            this.matchScannerTrigger = matchScannerTrigger;
        }

        private void Start()
        {
            slotGrid.GenerateItems();
            gemGrid.GenerateItems();

            StartCoroutine(SetGemForEachSlot());
        }

        private IEnumerator SetGemForEachSlot()
        {
            Slot currentSlot;
            Gem currentGem;
            for(int row = 0; row < slotGrid.Rows; row++)
            {
                for(int column = 0; column < slotGrid.Columns; column++)
                {
                    currentSlot = slotGrid.GetItemByRowColumn(row, column);
                    currentGem = gemGrid.GetItemByRowColumn(row, column);
                    currentSlot.ReceiveGem(currentGem);
                    yield return null;
                }
            }
        }
    }
}