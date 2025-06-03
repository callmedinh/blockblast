using UnityEngine;
using UnityEngine.Audio;

namespace UI
{
    public class SettingScreenManager : MonoBehaviour
    {
        [SerializeField] private SettingView settingView;
        [SerializeField] private AudioMixer audioMixer;
        AudioSettingPresenter _audioSettingPresenter;

        private void Awake()
        {
            _audioSettingPresenter = new AudioSettingPresenter(settingView, audioMixer);
        }
    }
}