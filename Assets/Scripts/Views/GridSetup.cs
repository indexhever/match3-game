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
        GameGrid<Slot> slotGrid;

        [Inject]
        private void Construct(GameGrid<Gem> gemGrid, GameGrid<Slot> slotGrid)
        {
            this.gemGrid = gemGrid;
            this.slotGrid = slotGrid;
        }

        private void Start()
        {
            slotGrid.GenerateItems();
            gemGrid.GenerateItems();
        }
    }
}