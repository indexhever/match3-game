using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridFramework
{
    public interface ItemFactory
    {
        Vector2 MeasuresInUnit { get; }

        Item Create(Vector2 newItemPosition);
    }
}