using GridFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public interface ItemSearcher<ItemType> where ItemType : Item
    {
        ItemType GetItemAbove(ItemType givenItem);
        ItemType GetItemUnder(ItemType givenItem);
        ItemType GetItemRight(ItemType givenItem);
        ItemType GetItemLeft(ItemType givenItem);
        void SwapItems(ItemType selectedItem, ItemType itemSwapped);
    }
}