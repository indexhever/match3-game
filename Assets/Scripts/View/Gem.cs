using GridFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class Gem : MonoBehaviour, Item
    {
        [Inject]
        public void Construct(Vector2 initialPosition)
        {
            transform.position = initialPosition;
        }

        public class Factory : PlaceholderFactory<Vector2, Gem>
        {

        }
    }
}