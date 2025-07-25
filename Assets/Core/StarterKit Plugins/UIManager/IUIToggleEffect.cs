#nullable enable
using DG.Tweening;

namespace Core.StarterKit_Plugins.UIManager
{
    public interface IUIToggleEffect
    {
        Tween? OnToggleOn();
        Tween? OnToggleOff();
    }
}