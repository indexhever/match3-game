using GridFramework;
using Math3Game.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class GemComponent : MonoBehaviour, Gem
    {
        private Slot currentSlot;
        private Scorer scorer;
        private Action OnArriveAtSlotRoutine;

        [SerializeField]
        private Rigidbody2D gemRigidbody;
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Mover mover;

        public virtual Vector2 Position
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

        public virtual int Row { get; set; }

        public virtual int Column { get; set; }
        public virtual Sprite Image { get => spriteRenderer.sprite; }

        [Inject]
        public void Construct(Vector2 initialPosition, Sprite gemImage, Scorer scorer)
        {
            transform.position = initialPosition;
            spriteRenderer.sprite = gemImage;
            this.scorer = scorer;
        }

        public virtual bool Equals(Gem other)
        {

            return other.Image == Image;
        }

        public void EnterSlot(Slot slot)
        {
            this.currentSlot = slot;
        }

        public virtual void Dispose()
        {
            if (!gameObject.activeInHierarchy)
                return;

            scorer.IncreaseScore();
            currentSlot.CleanGem();
            gameObject.SetActive(false);
        }

        public void MoveToPosition(Vector2 newPosition, Action OnArrive = null)
        {
            OnArriveAtSlotRoutine = OnArrive;
            mover.MoveToPosition(newPosition);
        }

        public void OnArriveAtSlot()
        {
            OnArriveAtSlotRoutine?.Invoke();
        }

        public class Factory : PlaceholderFactory<Vector2, Sprite, GemComponent>
        {

        }
    }
}