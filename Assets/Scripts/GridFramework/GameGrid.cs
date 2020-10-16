using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridFramework
{
    public class GameGrid<ItemType> where ItemType : Item
    {
        private readonly ItemType NULL_ITEM;
        private ItemFactory<ItemType> itemFactory;
        private Vector2 origin;
        private ItemType[] items;
        private float offsetBetweenItens;
        private Vector2 itemMeasuresInUnit;        

        public GameGrid(int rows, int columns, ItemFactory<ItemType> itemFactory, Vector2 origin, float offsetBetweenItens)
        {
            Rows = rows;
            Columns = columns;
            this.itemFactory = itemFactory;
            this.origin = origin;
            items = new ItemType[Rows * Columns];
            NULL_ITEM = itemFactory.CreateNull();
            this.offsetBetweenItens = offsetBetweenItens;
            this.itemMeasuresInUnit = itemFactory.MeasuresInUnit;
        }

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public void GenerateItems()
        {
            Vector2 newItemPosition;
            ItemType newItem;
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
        }

        public void SetItemAtRowAndColum(ItemType item, int row, int column)
        {
            items[GetPositionFromRowColumn(row, column)] = item;
        }

        public ItemType GetItemByRowColumn(int row, int column)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                return NULL_ITEM;
            return items[GetPositionFromRowColumn(row, column)];
        }

        public void SwapItems(ItemType firstItem, ItemType secondItem)
        {
            int firstItemRow = firstItem.Row;
            int firstItemColumn = firstItem.Column;

            items[GetPositionFromRowColumn(secondItem.Row, secondItem.Column)] = firstItem;
            items[GetPositionFromRowColumn(firstItemRow, firstItemColumn)] = secondItem;

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
    }
}