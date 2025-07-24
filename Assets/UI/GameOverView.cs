using Data;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class GameOverView : BaseUIView
    {
        [SerializeField] private Button playButon;
        [SerializeField] private Transform slotListParent;
        [SerializeField] private GameObject slotPrefab;
        private GameDataContainer _gameData;

        private void OnEnable()
        {
            playButon.onClick.AddListener(HandlePlayButtonClick);
            DisplayScoreHistory();
        }

        private void OnDisable() => playButon.onClick.RemoveListener(HandlePlayButtonClick);

        private void DisplayScoreHistory()
        {
            _gameData = SaveLoadSystem.Instance.LoadGameData();
            for (int i = 0; i < _gameData.gameDataList.Count; i++)
            {
                GameObject slot = Instantiate(slotPrefab, slotListParent);
                var data = _gameData.gameDataList[i];
                slot.GetComponentInChildren<TMP_Text>().text = $"{data.score} - {data.dateTime}";
            }
        }
        private void ClearOldScoreSlots()
        {
            foreach (Transform child in slotListParent)
            {
                Destroy(child.gameObject);
            }
        }

        private void HandlePlayButtonClick()
        {
            GameEvent.OnGameStarted?.Invoke();
            GameManager.Instance.ChangeState(GameState.InGameplay);
        }

        protected override void OnShow()
        {
            base.OnShow();
        }
    }
}