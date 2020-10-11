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
        [SerializeField]
        private SpriteRenderer spriteRenderer;

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
        public void Construct(Vector2 initialPosition, Sprite gemImage)
        {
            transform.position = initialPosition;
            spriteRenderer.sprite = gemImage;
        }

        public class Factory : PlaceholderFactory<Vector2, Sprite, Gem>
        {

        }
    }
}