using System;

namespace Assets.Scripts.Core.StateMachine
{
    public interface IStateRunner
    {
        void SetState(Type stateType);
        void UpdateCurrentState();
    }
}
