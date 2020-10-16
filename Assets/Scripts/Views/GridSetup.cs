using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using GridFramework;

namespace Math3Game.View
{
    public class GridSetup : MonoBehaviour
    {
        private GameGrid gameGrid;

        [Inject]
        private void Construct(GameGrid gameGrid)
        {
            this.gameGrid = gameGrid;
        }

        private void Start()
        {
            gameGrid.GenerateItems();
        }
    }
}