using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheHorde;

public class OverScene : IScene
{
    #region Event related
    public delegate void SceneChange(SceneType sceneType);
    public static event SceneChange SceneChangeEvent;
    #endregion

    #region Fields
    private ScoreManager m_Score;
    private string m_Title, m_ScoreText, m_HighScoreText, m_RetryText, m_ToMenuText;
    private KeyboardState m_CurrentState, m_PreviousState;
    #endregion

    #region Constructor
    public OverScene(ScoreManager score)
    {
        m_Score = score;

        m_Title = "GAME OVER!";
        m_ScoreText = "SCORE: " + m_Score.Score;
        m_HighScoreText = "HIGH SCORE: " + m_Score.HighScore;
        m_RetryText = "[R] RETRY";
        m_ToMenuText = "[M] MENU";

        m_CurrentState = Keyboard.GetState();
        m_PreviousState = m_CurrentState;
    }
    #endregion

    #region Methods
    public void Update(GameTime gameTime)
    {
        m_PreviousState = m_CurrentState;
        m_CurrentState = Keyboard.GetState();

        // Over to Game
        if(m_CurrentState.IsKeyDown(Keys.R) && m_PreviousState.IsKeyUp(Keys.R))
            SceneChangeEvent?.Invoke(SceneType.Game);
        // Over to Menu
        else if(m_CurrentState.IsKeyDown(Keys.M) && m_PreviousState.IsKeyUp(Keys.M))
            SceneChangeEvent?.Invoke(SceneType.Menu);
    }
    
    public void Render(SpriteBatch spriteBatch)
    {
        SpriteFont largeFont = AssetManager.Instance().GetFont("Large");
        SpriteFont mediumFont = AssetManager.Instance().GetFont("Medium");

        // Title render
        spriteBatch.DrawString(largeFont, m_Title, new Vector2(Game1.CenterText(largeFont, m_Title).X, 10.0f), Color.CadetBlue);

        // Score text render
        spriteBatch.DrawString(mediumFont, m_ScoreText, new Vector2(Game1.CenterText(mediumFont, m_ScoreText).X, 150.0f), Color.Blue);

        // High score text render
        spriteBatch.DrawString(mediumFont, m_HighScoreText, new Vector2(Game1.CenterText(mediumFont, m_HighScoreText).X, 200.0f), Color.Blue);

        // Retry text render
        spriteBatch.DrawString(mediumFont, m_RetryText, new Vector2(Game1.CenterText(mediumFont, m_RetryText).X, 300.0f), Color.CadetBlue);

        // To menu text render
        spriteBatch.DrawString(mediumFont, m_ToMenuText, new Vector2(Game1.CenterText(mediumFont, m_ToMenuText).X, 350.0f), Color.CadetBlue);
    }
    #endregion
}