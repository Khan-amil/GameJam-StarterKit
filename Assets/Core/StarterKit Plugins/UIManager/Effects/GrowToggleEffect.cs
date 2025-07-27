using DG.Tweening;
using UnityEngine;

namespace Core.StarterKit_Plugins.UIManager
{
    public class GrowToggleEffect : MonoBehaviour, IUIToggleEffect
    {

        public Tween OnToggleOn()
        {
            transform.localScale = Vector3.zero;
            return transform.DOScale(Vector3.one, 0.25f);
        }

        public Tween OnToggleOff()
        {
            return transform.DOScale(Vector3.zero, 0.25f);
        }
    }
}