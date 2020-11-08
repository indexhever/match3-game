using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Math3Game.View
{
    public class Mover : MonoBehaviour
    {
        private const float STOPPING_DISTANCE = 0.01f;

        private Vector2 destinationPosition;
        private Vector2 remainingPath;
        private Vector2 currentPosition;
        private Vector2 newPosition;

        [SerializeField]
        private Rigidbody2D objectRigidBody;
        [SerializeField]
        private float speed;
        [SerializeField]
        private UnityEvent OnArrivedAtDestination;

        public void MoveToPosition(Vector2 destinationPosition)
        {
            this.destinationPosition = destinationPosition;
            currentPosition = transform.localPosition;
            newPosition = transform.localPosition;

            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            while (HasNotArrivedAtDestination())
            {
                currentPosition = transform.localPosition;
                remainingPath = destinationPosition - currentPosition;
                newPosition = currentPosition + remainingPath * speed * Time.deltaTime;
                objectRigidBody.MovePosition(newPosition);
                yield return null;
            }
            objectRigidBody.MovePosition(destinationPosition);

            OnArrivedAtDestination?.Invoke();
        }

        private bool HasNotArrivedAtDestination()
        {
            return Vector2.Distance(currentPosition, destinationPosition) > STOPPING_DISTANCE;
        }
    }
}