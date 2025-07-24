using System.Collections.Generic;

namespace Core
{
    public class StateMachine<T>
    {
        private Dictionary<T, IGameState> _states = new();
        private IGameState _current;

        public void AddState(T key, IGameState state)
        {
            _states[key] = state;
        }

        public void ChangeState(T key)
        {
            _current?.Exit();
            _current = _states[key];
            _current?.Enter();
        }

        public void Update()
        {
            _current?.Update();
        }
        public T CurrentKey { get; private set; }
    }

}