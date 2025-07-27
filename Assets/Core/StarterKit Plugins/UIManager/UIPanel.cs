using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

namespace Core.StarterKit_Plugins.UIManager
{
    public class UIPanel : MonoBehaviour
    {
        public List<UIBlock> elements = new List<UIBlock>();
        public Ease ease = Ease.OutQuad;
        
        [SerializeField] 
        private IUIToggleEffect[] _toggleEffects = Array.Empty<IUIToggleEffect>();

        public void Open(bool stackPage)
        {
            if (stackPage)
            {
                UIManager.Instance.StackPage(this);
            }
            else
            {
                UIManager.Instance.OpenMenu(this);
            }
        }
        
        public virtual Tween ShowPage()
        {
            Sequence seq = DOTween.Sequence();
            Sequence pageEffect = DOTween.Sequence();
            foreach (var uiToggleEffect in _toggleEffects)
            {
                pageEffect.Join(uiToggleEffect.OnToggleOn());
            }
            Sequence blocks = DOTween.Sequence();            
            
            foreach (var el in elements)
            {
                blocks.Join(el.Show());
            }
            
            seq.Append(pageEffect);
            seq.Append(blocks);
            seq.OnStart(()=> gameObject.SetActive(true));
            
            return seq.SetEase(ease);
        }

        public virtual Tween HidePage()
        {
            Sequence seq = DOTween.Sequence();
            
            Sequence pageEffect = DOTween.Sequence();
            foreach (var uiToggleEffect in _toggleEffects)
            {
                pageEffect.Join(uiToggleEffect.OnToggleOff());
            }
            
            Sequence blocks = DOTween.Sequence();  
            foreach (var el in elements)
            {
                blocks.Join(el.Hide());
            }
            
            seq.Append(blocks);
            seq.Append(pageEffect);
            
            seq.OnComplete(() => gameObject.SetActive(false));
            return seq.SetEase(ease);
        }

        private void OnValidate()
        {
            _toggleEffects = GetComponents<IUIToggleEffect>();
        }
    }
}