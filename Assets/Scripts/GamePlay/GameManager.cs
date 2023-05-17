using Core;
using UnityEngine;

namespace GamePlay
{
    public class GameManager : Singleton<GameManager>
    {
        private int _highScore = 0;

        public int ShowHighScore()
        {
            _highScore = PlayerPrefs.GetInt(Constants.HIGH_SCORE,0);
            return _highScore;
        }
        
    }
}
