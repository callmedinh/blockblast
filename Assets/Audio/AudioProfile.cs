using Audio;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Roots
{
    [CreateAssetMenu(fileName = "Audio", menuName = "Block blast/Audio Profile")]
    public class AudioProfile : ScriptableObject
    {
        public AudioType type;
        public AudioClip clip;
        public State state;
    }
}