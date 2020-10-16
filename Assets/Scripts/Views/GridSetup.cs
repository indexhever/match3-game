using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using GridFramework;

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
        }
    }
}