using UnityEngine;

namespace Utilities
{
    public interface ITransition
    {
        public void Transition(float time,ref CanvasGroup canvasGroup,ref RectTransform rectTransform);
    }
}