using Events;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class SettingView : BaseUIView
    {
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button saveButton;


        [SerializeField] private float fadeTime = 1f;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;
        private readonly ITransition _fadeInTransition = new FadeIn();
        private readonly ITransition _fadeOutTransition = new FadeOut();

        [SerializeField] private AudioMixer mixer;
        private void OnEnable()
        {
            exitButton.onClick.AddListener(HandleExitButtonClick);
            soundToggle.onValueChanged.AddListener(ApplySoundChanged);
            musicToggle.onValueChanged.AddListener(ApplyMusicChanged);
            saveButton.onClick.AddListener(HandleSaveButtonClick);
        }

        private void OnDisable()
        {
            exitButton.onClick.RemoveListener(HandleExitButtonClick);
            saveButton.onClick.RemoveListener(HandleSaveButtonClick);
        }

        protected override void OnShow()
        {
            base.OnShow();
            _fadeInTransition.Transition(fadeTime, ref canvasGroup, ref rectTransform);
        }

        protected override void OnHide()
        {
            base.OnHide();
            _fadeOutTransition.Transition(fadeTime, ref canvasGroup, ref rectTransform);
        }

        private void HandleExitButtonClick()
        {
            GameManager.Instance.ChangeState(GameState.InGameplay);
        }

        private void HandleSaveButtonClick()
        {
            GameManager.Instance.ChangeState(GameState.InGameplay);
        }
        void ApplyMusicChanged(bool isOn)
        {
            mixer.SetFloat(GameConstants.MusicMixer, isOn ? 0f : -80f);
        }
        void ApplySoundChanged(bool isOn)
        {
            mixer.SetFloat(GameConstants.SfxMixer, isOn ? 0f : -80f);
        }
    }
}