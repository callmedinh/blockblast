namespace Data
{
    public class GameData
    {
        public int BestScore;
        
        //Game setting
        public float MusicVolume;
        public float SfxVolume;

        public GameData()
        {
            BestScore = 0;
            MusicVolume = 1f;
            SfxVolume = 1f;
        }
    }
}