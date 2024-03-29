﻿using System.Collections.Generic;
using UnityEngine;

namespace GridFramework
{
    public class GameGrid
    {
        private readonly NullItem NULL_ITEM = new NullItem();

        private ItemFactory itemFactory;
        private Vector2 origin;
        private Item[] items;
        private float offsetBetweenItens;
        private Vector2 itemMeasuresInUnit;
        
        public GameGrid(int rows, int columns, ItemFactory itemFactory, Vector2 origin, float offsetBetweenItens)
        {
            Rows = rows;
            Columns = columns;
            this.itemFactory = itemFactory;
            this.origin = origin;
            items = new Item[Rows * Columns];
            this.offsetBetweenItens = offsetBetweenItens;
            this.itemMeasuresInUnit = itemFactory.MeasuresInUnit;
        }

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public void GenerateItems()
        {
            Vector2 newItemPosition;
            Item newItem;
            for (int row = 0; row < Rows; row++)
            {
                for(int column = 0; column < Columns; column++)
                {
                    newItemPosition = CreateItemPositionByRowAndColum(row, column);
                    newItem = itemFactory.Create(newItemPosition);
                    newItem.Row = row;
                    newItem.Column = column;
                    items[GetPositionFromRowColumn(row, column)] = newItem;
                }                
            }

            LogGrid();
        }

        public Item GetItemByRowColumn(int row, int column)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                return NULL_ITEM;
            return items[GetPositionFromRowColumn(row, column)];
        }

        public void SwapItems(Item firstItem, Item secondItem)
        {
            int firstItemRow = firstItem.Row;
            int firstItemColumn = firstItem.Column;

            var itemToSwap = items[GetPositionFromRowColumn(secondItem.Row, secondItem.Column)];
            items[GetPositionFromRowColumn(secondItem.Row, secondItem.Column)] = firstItem;
            items[GetPositionFromRowColumn(firstItemRow, firstItemColumn)] = itemToSwap;

            firstItem.Row = secondItem.Row;
            firstItem.Column = secondItem.Column;
            secondItem.Row = firstItemRow;
            secondItem.Column = firstItemColumn;
        }

        private Vector2 CreateItemPositionByRowAndColum(int row, int column)
        {
            Vector2 firstGridItemPosition = CreateFirstGridItemPosition();
            float xTranslation = (itemMeasuresInUnit.x + offsetBetweenItens) * column;
            float yTranslation = (itemMeasuresInUnit.y + offsetBetweenItens) * row;
            float gridItemXPosition = firstGridItemPosition.x + xTranslation;
            float gridItemYPosition = firstGridItemPosition.y - yTranslation;

            return new Vector2(gridItemXPosition, gridItemYPosition);
        }

        private Vector2 CreateFirstGridItemPosition()
        {
            int amountOfGridItemOnTheLeft = Columns / 2;
            int amountOfGridItemOnTheTop = Rows / 2;
            float amountOfColumnsOffsets = Columns - 1;
            float amountOfRowOffsets = Rows - 1;
            float amountOfColumnOffsetsOnTheLeft = amountOfColumnsOffsets / 2;
            float amountOfRowOffsetsOnTheTop = amountOfRowOffsets / 2;

            float firstGridItemXPosition = origin.x - ((itemMeasuresInUnit.x * amountOfGridItemOnTheLeft) + (offsetBetweenItens * amountOfColumnOffsetsOnTheLeft));
            float firstGridItemYPosition = origin.y + ((itemMeasuresInUnit.y * amountOfGridItemOnTheTop) + (offsetBetweenItens * amountOfRowOffsetsOnTheTop));

            float finalPositionX = firstGridItemXPosition;
            float finalPositionY = firstGridItemYPosition;
            if (Columns % 2 == 0)
                finalPositionX = firstGridItemXPosition + itemMeasuresInUnit.x / 2;
            if (Rows % 2 == 0)
                finalPositionY = firstGridItemYPosition - itemMeasuresInUnit.y / 2;

            return new Vector2(finalPositionX, finalPositionY);
        }

        private int GetPositionFromRowColumn(int row, int column)
        {
            return row * Columns + column;
        }

        //after each match draw, we need to update items position of the grid
        public void UpdateGrid(Item extraNewItem) {
            int pos = GetPositionFromRowColumn(extraNewItem.Row, extraNewItem.Column);
            items[pos] = null;
            items[pos] = extraNewItem;
        }

        public void UpdateRows(List<Item> itemsToBeDisposed) {
            for (int i = 0; i < itemsToBeDisposed.Count; i++) {
                var itemToDispose = itemsToBeDisposed[i];
                if (itemToDispose.Row > 0) {
                    for (int j = 0; j < items.Length; j++) {
                        var currentItem = items[j];
                        if (currentItem.Column == itemToDispose.Column && currentItem.Row < itemToDispose.Row) {
                            currentItem.Row++;
                        }
                    }
                }
            }
        }

        public void LogGrid() {
            string grid="";
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    string id = TranslateGem(items[GetPositionFromRowColumn(i, j)].Image.name);
                    grid += id +  ",";
                }

                grid += '\n';
            }
            
            Debug.Log(grid);
        }

        public static string TranslateGem(string name) {
            string result = name.Substring(name.Length - 1, 1);
            string id = "";
            switch (result) {
                case "1": id = "MILK_";
                    break;
                case "2": id = "APPLE";
                    break;
                case "3": id = "ORANG";
                    break;
                case "4": id = "BREAD";
                    break;
                case "5": id = "VEGIE";
                    break;
                case "6": id = "COCO_";
                    break;
                case "7": id = "STAR_";
                    break;
            }

            return id;
        }
    }
}