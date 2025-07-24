using Audio;
using Map;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Block
{
    public class BlockDragHandler : MonoBehaviour
    {
        public InputActionAsset inputActions;

        public bool isDragging;
        private InputAction _clickAction;
        private InputAction _pointAction;

        private Vector2 _pointerPosition;

        private void Update()
        {
            _pointerPosition = Camera.main.ScreenToWorldPoint(_pointAction.ReadValue<Vector2>());
            if (_clickAction.WasPressedThisFrame())
                if (IsPointerOverThisBlock())
                {
                    isDragging = true;
                    SoundManager.Instance.Play(State.Drag);
                }

            if (_clickAction.IsPressed() && isDragging)
            {
                transform.position = _pointerPosition;
                MapManager.Instance.ShowGhostPreview(this);
            }

            if (_clickAction.WasReleasedThisFrame())
            {
                isDragging = false;
                MapManager.Instance.TryPlaceBlock(this);
            }
        }


        private void OnEnable()
        {
            var gameplayMap = inputActions.FindActionMap("Gameplay");
            if (gameplayMap == null)
            {
                Debug.LogError("Gameplay action map not found!");
                return;
            }

            _pointAction = gameplayMap.FindAction("Point");
            _clickAction = gameplayMap.FindAction("Click");

            if (_pointAction == null || _clickAction == null)
            {
                Debug.LogError("Point or Click action not found!");
                return;
            }

            _pointAction.Enable();
            _clickAction.Enable();
        }

        private bool IsPointerOverThisBlock()
        {
            var hit = Physics2D.Raycast(_pointerPosition, Vector2.zero);
            return hit.collider != null && hit.transform == transform;
        }
    }
}