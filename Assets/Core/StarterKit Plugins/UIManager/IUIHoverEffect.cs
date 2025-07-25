using DG.Tweening;

namespace Core.StarterKit_Plugins.UIManager
{
    public interface IUIHoverEffect
    {
        Tween OnHoverEnter();

		Tween OnHoverExit();
    }
}