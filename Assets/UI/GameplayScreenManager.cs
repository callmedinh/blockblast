using System;
using DefaultNamespace;
using Events;
using UnityEngine;

namespace UI
{
    public class GameplayScreenManager : MonoBehaviour
    {
        [SerializeField] GameplayView gameplayView;
        [SerializeField] private ScoreManager scoreManager;
        private GameplayPresenter _gameplayPresenter;

        private void Awake()
        {
            _gameplayPresenter = new GameplayPresenter(gameplayView, scoreManager);
        }
    }
}