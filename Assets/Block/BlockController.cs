using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class BlockController : MonoBehaviour
    { 
        /*
        [SerializeField] private int minSpeed;
        [SerializeField] private int maxSpeed;

        private Vector2 _moveValue;
        private Rigidbody2D _rigidbody;

        [SerializeField] private Block block;

        private void Start()
        {
            _rigidbody = this.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (MapManager.Instance.IsInsideMap(transform.position.x, transform.position.y))
            {
                _rigidbody.linearVelocity = Vector3.down * minSpeed * Time.fixedDeltaTime;   
            }
            else
            {
                _rigidbody.linearVelocity = Vector2.zero;
            }

        }
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _moveValue = context.ReadValue<Vector2>();
                int direction = Mathf.RoundToInt(_moveValue.x);
                if (direction != 0 && MapManager.Instance.IsInsideMap(direction, transform.position.y))
                {
                    MoveHorizontally(direction);   
                }
            }
        }

        void MoveHorizontally(int direction)
        {
            transform.position += Vector3.right * direction * this.minSpeed * Time.deltaTime;
        }

        public Block GetBlockPosition()
        {
            return block;
        }
        */
    }
}