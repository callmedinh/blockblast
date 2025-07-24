using System.Collections.Generic;

namespace Data
{
    [System.Serializable]
    public class GameDataEntry
    {
        public int score;
        public string dateTime;
        public GameDataEntry(int score, string dateTime)
        {
            this.score = score;
            this.dateTime = dateTime;
        }
    }
    
    [System.Serializable]
    public class GameDataContainer
    {
        public List<GameDataEntry> gameDataList = new();
    }
}