using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace TheHorde;

public class ScoreManager
{
    #region Fields
    public int Score;
    public int HighScore;
    #endregion

    #region Constructor
    public ScoreManager()
    {
        Score = 0;
        HighScore = 0;

        // Subscribing to events
        Zombie.ScoreIncreaseEvent += OnScoreIncrease;
    }
    #endregion

    #region Methods
    public void Update()
    {
        // Setting a new high score if the current score is higher
        HighScore = Score > HighScore ? Score : HighScore;
    }

    public void OnScoreIncrease(ZombieType zombieType)
    {
        switch(zombieType)
        {
            case ZombieType.Basic:
                Score += 5;
                break;
            case ZombieType.Brute:
                Score += 15;
                break;
            case ZombieType.Denizen:
                Score += 10;
                break;
        }
    }
    #endregion
}