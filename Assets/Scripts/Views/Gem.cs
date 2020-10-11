using GridFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class Gem : MonoBehaviour, Item
    {
        [SerializeField]
        private Rigidbody2D gemRigidbody;
        RigidbodyConstraints2D currentMovementConstraint;

        public Vector2 Position 
        { 
            get
            {
                return transform.localPosition;
            }
            set
            {
                gemRigidbody.MovePosition(value);
            }
        }

        public int Row { get; set; }

        public int Column { get; set; }

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