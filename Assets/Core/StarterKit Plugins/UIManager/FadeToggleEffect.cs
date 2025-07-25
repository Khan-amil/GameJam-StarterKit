using DG.Tweening;
using UnityEngine;

namespace Core.StarterKit_Plugins.UIManager
{
    public class FadeToggleEffect : MonoBehaviour, IUIToggleEffect
    {

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.25f;

        public Tween OnToggleOn()
        {
            _canvasGroup.alpha = 0;
            return _canvasGroup.DOFade(1, _fadeDuration);
        }

        public Tween OnToggleOff()
        {
            return _canvasGroup.DOFade(0, _fadeDuration);
        }
    }
}