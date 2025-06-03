using System;
using DefaultNamespace;
using UnityEngine;

namespace UI
{
    public class GameoverScreenManager : MonoBehaviour
    {
        [SerializeField] private GameOverView gameOverView;
        [SerializeField] private ScoreManager scoreManager;

        private void Awake()
        {
            GameOverPresenter gameOverPresenter = new GameOverPresenter(gameOverView, scoreManager);
        }
    }
}