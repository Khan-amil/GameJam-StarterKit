namespace Core.Scripts.Utils
{
    public interface IPoolable
    {
        void OnSpawned();
        void OnDespawned();
    }
}