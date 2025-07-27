using System;
using UnityEngine;
using System.Collections.Generic;
using Core.Scripts.StateManagement;
using UnityEngine.UI;

namespace Core.StarterKit_Plugins.UIManager
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private List<UIPanel> _panels;
        
        private int _currentIndex = 0;
        
        private bool _isOpen = false;

        [SerializeField] private Button _openButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _closeButton;

        [SerializeField] private GameState _gameStateWhenOpen;
        private GameState _previousGameState;
        
        
        private void Awake()
        {
            Close();

            foreach (var panel in _panels)
            {
                panel.gameObject.SetActive(false);
            }
            
            if (_openButton)
            {
                _openButton.onClick.AddListener(Open);
            }
            if (_nextButton)
            {
                _nextButton.onClick.AddListener(NextPage);
            }

            if (_previousButton)
            {
                _previousButton.onClick.AddListener(PreviousPage);
            }
                
            if (_closeButton)
            {
                _closeButton.onClick.AddListener(Close);
            }
            
            // Disable control buttons initially
            if (_nextButton) _nextButton.gameObject.SetActive(false);
            if (_previousButton) _previousButton.gameObject.SetActive(false);
            if (_closeButton) _closeButton.gameObject.SetActive(false);
        }

        public void Open()
        {
            if (_isOpen) 
                return;

            if (_gameStateWhenOpen)
            {
                _previousGameState = GameStateManager.Instance.CurrentState;
                GameStateManager.Instance.SetStateAsync(_gameStateWhenOpen);
            }
            
            // Enable control buttons
            if (_nextButton) _nextButton.gameObject.SetActive(true);
            if (_previousButton) _previousButton.gameObject.SetActive(true);
            if (_closeButton) _closeButton.gameObject.SetActive(true);
            
            UIManager.Instance.OpenMenu(_panels[0]);
            _currentIndex = 0;
            _isOpen = true;
        }
        
        public void NextPage()
        {
            _currentIndex++;

            if (_currentIndex > _panels.Count - 1)
            {
                _currentIndex = 0; // Loop back to the first panel
            }

            UIManager.Instance.StackPage(_panels[_currentIndex]);
        }

        public void PreviousPage()
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = _panels.Count - 1; // Loop back to the last panel
            }

            UIManager.Instance.StackPage(_panels[_currentIndex]);
        }

        public void Close()
        {
            if (!_isOpen) 
                return;

            if (_previousGameState)
            {
                GameStateManager.Instance.SetStateAsync(_previousGameState);
                _previousGameState = null;
            }
            
            // Disable control buttons
            if (_nextButton) _nextButton.gameObject.SetActive(false);
            if (_previousButton) _previousButton.gameObject.SetActive(false);
            if (_closeButton) _closeButton.gameObject.SetActive(false);
            
            UIManager.Instance.Close();
            _isOpen = false;
            _currentIndex = 0; // Reset index when closing
        }
    }
}