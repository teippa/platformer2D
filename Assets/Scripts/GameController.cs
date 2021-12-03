using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameController
{
    //public float HighScore { get; private set; }
    public float PlayerScore { get; private set; }

    private List<float> _highScores = new List<float>();



    #region Singleton
    private static readonly GameController _instance = new GameController();

    private GameController()
    {
        float score;
        for (int i = 0; i < 3; i++)
        {
            score = PlayerPrefs.GetFloat("highScore" + i, 0f);
            _highScores.Add(item: score);
        }
    }
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion


    public void IncreasePlayerScore(float increaseAmount)
    {
        PlayerScore += increaseAmount;
    }

    public void ResetPlayerScore()
    {
        PlayerScore = 0;
    }

    public List<float> HighScores
    {
        get
        {
            _highScores.Sort((float x, float y) => y.CompareTo(x));
            return _highScores;
        }
    }

    public bool UpdateHighScores()
    {
        bool newHighScore = PlayerScore > HighScores[0];
        _highScores.Add(item:  PlayerScore );
        _highScores.Sort((float x, float y) => y.CompareTo(x));
        _highScores.Remove(_highScores[3]);

        //Debug.Log("HS: " + newHighScore + "  Score: " + _highScores[0]);

        for (int i=0; i<3; i++)
            PlayerPrefs.SetFloat("highScore"+i, _highScores[i]);

        return newHighScore;
    }

}
