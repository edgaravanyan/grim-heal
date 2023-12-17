namespace Assets.Scripts.Core.Utils.Pool
{
    public interface IObjectPool<T> where T : class
    {
        T Get();
        void Release(T pooledObject);
    }
}
