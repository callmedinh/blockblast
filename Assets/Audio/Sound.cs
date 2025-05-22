using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu (fileName = "Sound", menuName = "Blockblast/Sound")]
    public class Sound : ScriptableObject
    {
        public AudioClip[] audioClips;
        [Range(0, 1)] public float volume;

        public void Play(AudioSource source)
        {
            AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
            source.PlayOneShot(clip, volume);
        }
    }
}