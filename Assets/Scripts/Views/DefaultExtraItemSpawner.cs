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
        private GameGrid grid;
        private ExtraGemFactory extraGemFactory;
        private int[] amountExtraItemsToCreatePerColumn;
        private BoardUpdater boardUpdater;
        private float previousYForSpawning;

        [SerializeField]
        private float amountSecondsWaitToSpawnAnother = .3f;

        [Inject]
        private void Construct(GameGrid grid, ExtraGemFactory extraGemFactory, BoardUpdater boardUpdater)
        {
            this.grid = grid;
            this.extraGemFactory = extraGemFactory;
            this.boardUpdater = boardUpdater;
            amountExtraItemsToCreatePerColumn = new int[grid.Columns];
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
            for(int column = 0; column < amountExtraItemsToCreatePerColumn.Length; column++)
            {
                while(amountExtraItemsToCreatePerColumn[column] > 0)
                {
                    SpawnItemAtColumn(column);
                    amountExtraItemsToCreatePerColumn[column]--;                  
                }
                yield return null;
                previousYForSpawning = 0;
            }
            boardUpdater.Stop();
        }

        private void SpawnItemAtColumn(int column)
        {
            Vector2 newPosition = GetPositionForColumn(column);
            Gem newGem = extraGemFactory.Create(newPosition);
            newGem.OnBoardUpdate();
        }

        private Vector2 GetPositionForColumn(int column)
        {
            Vector2 firstItemPositionOfGridColumn = grid.GetItemByRowColumn(0, column).Position;
            float newX = firstItemPositionOfGridColumn.x;
            float newY = extraGemFactory.MeasuresInUnit.y + firstItemPositionOfGridColumn.y + previousYForSpawning;
            previousYForSpawning += extraGemFactory.MeasuresInUnit.y;

            return new Vector2(newX, newY);
        }
    }
}