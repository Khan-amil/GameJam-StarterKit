using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.StarterKit_Plugins.UIManager
{
    public class ShakeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Tween _shakeTween;
        private bool _shaking;
        public void OnMouseEnter()
        {
            StartShake();
        }

        private void StartShake()
        {
            _shaking = true;
            _shakeTween= DOTween.Shake(() => transform.position, 
                x => transform.position = x, 
                0.2f, 
                3,
                10, 
                90, fadeOut:false).OnComplete(()=>
                {
                    if (_shaking)
                        _shakeTween.Restart(false);
                });
        }

        public void OnMouseExit()
        {
            StopShake();
        }

        private void StopShake()
        {
            _shaking = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            StartShake();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopShake();
        }
    }
}