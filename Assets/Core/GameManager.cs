using System;
using Audio;
using Block;
using Core;
using Data;
using DefaultNamespace;
using Events;
using Map;
using Roots;
using UI;
using UnityEngine;
using Utilities;

public enum GameState
{
    GameOver, InGameplay, Setting
}
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private MapInfo mapInfo;
    [SerializeField] private Pool pool;
    private BlockSystem _blockSystem;
    private StateMachine<GameState> _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine<GameState>();
        _blockSystem = new BlockSystem(pool);
        _stateMachine.AddState(GameState.InGameplay, new GameplayState(mapInfo, _blockSystem));
        _stateMachine.AddState(GameState.GameOver, new GameOverState(_blockSystem));
        _stateMachine.AddState(GameState.Setting, new GamePauseState(_blockSystem));
    }

    private void Start()
    {
        ChangeState(GameState.InGameplay);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void ChangeState(GameState state)
    {
        _stateMachine.ChangeState(state);
    }

    public GameState CurrentState => _stateMachine.CurrentKey;
}