using Core.StarterKit_Plugins.UIManager;
using UnityEngine;
using UnityEngine.UI;

public class OpenPanelOnClick : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private UIPanel _panel;
    [SerializeField] private Menu _menu;
    [SerializeField] private bool _stackPage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (_stackPage)
        {
            UIManager.Instance.StackPage(_panel, true);
        }
        else
        {
            UIManager.Instance.OpenMenu(_panel);
        }
    }
    
    private void OnDestroy()
    {
        if (_button)
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}
