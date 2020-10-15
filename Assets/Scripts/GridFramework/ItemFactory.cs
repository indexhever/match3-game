using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridFramework
{
    public interface ItemFactory<ItemType>
    {
        Vector2 MeasuresInUnit { get; }

        ItemType Create(Vector2 newItemPosition);
        ItemType CreateNull();
    }
}