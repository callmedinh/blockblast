using System;
using System.Runtime.InteropServices;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class SettingView : BaseUIView
    {
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Button closeButton;

        [SerializeField] private float _fadeTime = 1f;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        
        //event
        public event Action<float> OnSfxChanged;
        public event Action<float> OnMusicChanged;
        public event Action OnClosedButtonClicked;

        private void Awake()
        {
            sfxSlider.onValueChanged.AddListener(OnSfxSliderChange);
            musicSlider.onValueChanged.AddListener(OnMusicSliderChange);
            closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        protected override void OnShow()
        {
            base.OnShow();
            PanelFadeIn();
        }

        protected override void OnHide()
        {
            PanelFadeOut();
        }

        public void OnCloseButtonClick()
        {
            OnClosedButtonClicked?.Invoke();
        }
        public void OnMusicSliderChange(float value)
        {
            OnMusicChanged?.Invoke(value);
        }

        public void OnSfxSliderChange(float value)
        {
            OnSfxChanged?.Invoke(value);
        }

        public void PanelFadeIn()
        {
            _canvasGroup.alpha = 0;
            _rectTransform.transform.localPosition = new Vector3(0, -1000, 0);
            _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutElastic);
            _canvasGroup.DOFade(1, _fadeTime);
        }
        public void PanelFadeOut()
        {
            _canvasGroup.alpha = 0;
            _rectTransform.transform.localPosition = new Vector3(0, -1000, 0);
            _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutElastic);
            _canvasGroup.DOFade(1, _fadeTime);
        }
    }
}