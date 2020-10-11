using GridFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Math3Game.Controller
{
    public interface ItemSearcher
    {
        Item GetItemAbove(Item givenItem);
        Item GetItemUnder(Item givenItem);
        Item GetItemRight(Item givenItem);
        Item GetItemLeft(Item givenItem);
    }
}