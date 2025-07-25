using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.StarterKit_Plugins.UIManager
{
    public class UIManager : MonoBehaviour
    {
        public Button openButton;
        public Button nextButton;
        public Button previousButton;
        public Button closeButton;
        
        public Menu uiBlock;
        
        public List<UIPanel> uiPanels;
        
        public bool chainEffects = true;
        
        int _currentPanelIndex = 0;
        
        private void Start()
        {
            if (openButton != null)
            {
                openButton.onClick.AddListener(OnOpenButtonClick);
            }
            if (nextButton != null)
            {
                nextButton.onClick.AddListener(OnNextButtonClick);
            }

            if (previousButton != null)
            {
                previousButton.onClick.AddListener(OnPreviousButtonClick);
            }
            
            if (closeButton != null)
            {
                closeButton.onClick.AddListener(OnCloseButtonClick);
            }

            foreach (var uiPanel in uiPanels)
            {
                uiPanel.HidePage();
            }
        }

        private void OnNextButtonClick()
        {
            if (uiPanels.Count == 0) return;

            _currentPanelIndex++;
            if (_currentPanelIndex >= uiPanels.Count)
            {
                _currentPanelIndex = 0; // Loop back to the first panel
            }

            var nextPanel = uiPanels[_currentPanelIndex];
            uiBlock.NextPage(nextPanel, chainEffects);
        }
        
        private void OnPreviousButtonClick()
        {
            if (uiPanels.Count == 0) return;

            _currentPanelIndex--;
            if (_currentPanelIndex < 0)
            {
                _currentPanelIndex = uiPanels.Count - 1; // Loop back to the last panel
            }

            var previousPanel = uiPanels[_currentPanelIndex];
            uiBlock.NextPage(previousPanel, chainEffects);
        }
        
        private void OnCloseButtonClick()
        {
            uiBlock.Close();
        }

        private void OnOpenButtonClick()
        {
            uiBlock.OpenPage(uiPanels[_currentPanelIndex]);
        }
    }
}