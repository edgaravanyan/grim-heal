using System;

namespace Core.StateMachine
{
    public interface IStateRunner
    {
        void SetState(Type stateType);
        void UpdateCurrentState();
    }
}
