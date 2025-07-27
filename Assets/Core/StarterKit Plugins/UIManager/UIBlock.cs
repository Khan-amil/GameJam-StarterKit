#nullable enable
using System;
using System.Collections.Generic;
using Core.StarterKit_Plugins.UIManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBlock : MonoBehaviour
{
    [SerializeField] 
    private IUIToggleEffect[] _toggleEffects = Array.Empty<IUIToggleEffect>();
    
    [SerializeField] 
    private Image _icon;

    [SerializeField] private TMP_Text _text;

    public TMP_Text Text
    {
        get => _text;
        set => _text = value;
    }

    public Image Icon
    {
        get => _icon;
        set => _icon = value;
    }

    public virtual Tween Show()
    {
        gameObject.SetActive(true);
        Sequence seq = DOTween.Sequence();

        foreach (var effect in _toggleEffects)
        {
            var tween = effect.OnToggleOn();

            if (tween != null)
            {
                seq.Join(tween);
            }
        }
        return seq;
    }

    public virtual Tween Hide()
    {
        Sequence seq = DOTween.Sequence();

        foreach (var effect in _toggleEffects)
        {
            var tween = effect.OnToggleOff();

            if (tween != null)
            {
                seq.Join(tween);
            }
        }
        seq.OnComplete(() => gameObject.SetActive(false));
        return seq;
    }
    
    private void OnValidate()
    {
        _toggleEffects = GetComponents<IUIToggleEffect>();
    }
}
