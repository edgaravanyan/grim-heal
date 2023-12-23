using Core.Contracts.Pool;

namespace Core.Contracts.Messages
{
    public interface IMessage : IPoolable
    {
        void Initialize(object data);
    }
}