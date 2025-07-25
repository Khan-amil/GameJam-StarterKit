using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

namespace Core.StarterKit_Plugins.UIManager
{
    public class Menu : MonoBehaviour
    {
        private Stack<UIPanel> _pageStack = new Stack<UIPanel>();
        
        public void OpenPage(UIPanel page)
        {
            page.ShowPage();
            
            // Reset the stack to only contain the current page
            _pageStack.Clear();
            _pageStack.Push(page);
        }
        
        public void NextPage(UIPanel page, bool chainEffects = true)
        {
            Sequence sequence = DOTween.Sequence();
            
            if (_pageStack.Count > 0)
            {
                var current = _pageStack.Peek();
                sequence.Append(current.HidePage());
            }
            if (chainEffects)
            {
                sequence.OnComplete(() => page.ShowPage().Play());
            }
            else
            {
                sequence.Join(page.ShowPage());
            }
            
            _pageStack.Push(page);
        }

        public void Back()
        {
            if (_pageStack.Count <= 1) 
                return;

            var current = _pageStack.Pop();
            current.HidePage();

            var previous = _pageStack.Peek();
            previous.ShowPage();
        }

        public void Close()
        {
            if (_pageStack.Count < 1) 
                return;

            var current = _pageStack.Pop();
            current.HidePage();

            _pageStack.Clear();
        }
    }
}