using UnityEngine;
using UnityEngine.Audio;
using Utilities;

namespace UI
{
    public class AudioSettingPresenter
    {
        private AudioMixer _mixer;
        public AudioSettingPresenter(SettingView settingView, AudioMixer mixer)
        {
            settingView.OnMusicChanged += MusicSliderHandler;
            settingView.OnSfxChanged += SfxSliderHandler;
            settingView.OnClosedButtonClicked += OnCloseButtonHandler;
            _mixer = mixer;
        }

        void OnCloseButtonHandler()
        {
            UIManager.Instance.ShowScreen(GameConstants.UIScreenGameplay);
        }
        public void MusicSliderHandler(float value)
        {
            _mixer.SetFloat(GameConstants.MusicMixer, Mathf.Log10(value) * 20);
        }

        void SfxSliderHandler(float value)
        {
            _mixer.SetFloat(GameConstants.SfxMixer, Mathf.Log10(value) * 20);
        }
    }
}