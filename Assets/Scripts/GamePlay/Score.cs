using Core;
using TMPro;
using UnityEngine;

namespace GamePlay
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text highScoreText;

        private void Start()
        {
            GetTopScore();
        }

        private void GetTopScore()
        {
            highScoreText.text = GameManager.Instance().ShowHighScore().ToString();
        }

        public int GetCurrentScore()
        {
            return Constants.CurrentScore;
        }
    }
}