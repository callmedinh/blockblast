using TMPro;

namespace UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;

    public class AnimatedToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private RectTransform handle;
        [SerializeField] private Vector2 onPosition = new Vector2(100, 0);
        [SerializeField] private Vector2 offPosition = new Vector2(-100, 0);
        [SerializeField] private Sprite handleOnSprite;
        [SerializeField] private Sprite handleOffSprite;
        [SerializeField] private Image handleImage;
        [SerializeField] private TMP_Text text;
        
        [Header("Transition")]
        [SerializeField] private float moveDuration = 0.2f;
        [SerializeField] private Ease easing = Ease.OutBack;

        private void Awake()
        {
            if (toggle == null) toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        private void Start()
        {
            handle.anchoredPosition = toggle.isOn ? onPosition : offPosition;
        }

        private void OnToggleChanged(bool isOn)
        {
            handle.DOAnchorPos(isOn ? onPosition : offPosition, moveDuration).SetEase(easing);
            if (handleImage != null)
            {
                handleImage.sprite = isOn ? handleOnSprite : handleOffSprite;
            }

            text.text = isOn ? "ON" : "OFF";
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
    }

}