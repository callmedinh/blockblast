using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public class FadeIn : ITransition
    {
        public void Transition(float time,ref CanvasGroup canvasGroup,ref RectTransform rectTransform)
        {
            canvasGroup.alpha = 0;
            rectTransform.transform.localPosition = new Vector3(0, -1000, 0);
            rectTransform.DOAnchorPos(new Vector2(0f, 0f), time, false).SetEase(Ease.OutElastic);
            canvasGroup.DOFade(1, time);
        }
    }
}