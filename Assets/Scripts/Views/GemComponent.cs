using GridFramework;
using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game.View
{
    public class GemComponent : MonoBehaviour, Gem
    {
        private Slot currentSlot;
        private BoardUpdater boardUpdater;
        private Scorer scorer;

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
        public void Construct(Vector2 initialPosition, Sprite gemImage, BoardUpdater boardUpdater, Scorer scorer)
        {
            transform.position = initialPosition;
            spriteRenderer.sprite = gemImage;
            this.boardUpdater = boardUpdater;
            this.scorer = scorer;
            boardUpdater.SignOnUpdate(OnBoardUpdate);
            boardUpdater.SignOnUpdateComplete(OnBoardComplete);
        }

        public virtual bool Equals(Item other)
        {

            return other.Image == Image;
        }

        public void OnBoardUpdate()
        {
            //gemRigidbody.isKinematic = false;
        }

        public void OnBoardComplete()
        {
            //gemRigidbody.isKinematic = true;
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
            boardUpdater.UnsignOnUpdate(OnBoardUpdate);
            boardUpdater.UnSignOnUpdateComplete(OnBoardComplete);
            gameObject.SetActive(false);
        }

        public void MoveToPosition(Vector2 newPosition)
        {
            mover.MoveToPosition(newPosition);
        }

        public class Factory : PlaceholderFactory<Vector2, Sprite, GemComponent>
        {

        }
    }
}