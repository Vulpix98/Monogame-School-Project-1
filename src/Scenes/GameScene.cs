using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheHorde;

public class GameScene : IScene
{
    #region Event related
    public delegate void SceneChange(SceneType sceneType);
    public static event SceneChange SceneChangeEvent;
    #endregion

    #region Fields
    public EntityManager Entities;
    public SpawnManager Spawner;
    public CollisionManager Collision;
    
    private ScoreManager m_Score;
    private bool m_IsPaused;
    private string m_PauseText, m_ToMenuText;

    private KeyboardState m_CurrentState, m_PreviousState;
    #endregion

    #region Constructor
    public GameScene(ScoreManager score)
    {
        Entities = new EntityManager();
        Spawner = new SpawnManager(Entities, new Vector2(64.0f, 0.0f));
        Collision = new CollisionManager();

        m_Score = score;
        
        m_IsPaused = false;
        m_PauseText = "PAUSED";
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

        // Pauses the game
        if(m_CurrentState.IsKeyDown(Keys.P) && m_PreviousState.IsKeyUp(Keys.P))
            m_IsPaused = !m_IsPaused;
        // From Game to Menu
        else if(m_CurrentState.IsKeyDown(Keys.M) && m_PreviousState.IsKeyUp(Keys.M))
            SceneChangeEvent?.Invoke(SceneType.Menu);

        // Updates the below managers when the game is not paused
        if(m_IsPaused) return;

        Entities.Update(gameTime);
        Spawner.Update();
        m_Score.Update();
    }
    
    public void Render(SpriteBatch spriteBatch)
    {
        // Entities render
        Entities.Render(spriteBatch);

        // Barricade health
        spriteBatch.DrawString(AssetManager.Instance().GetFont("Small"), "Barricade: " + Entities.BarricadeHealth, new Vector2(10.0f, 10.0f), Color.Black);

        // Score
        string scoreText = "Score: " + m_Score.Score;
        Vector2 midText = Game1.CenterText(AssetManager.Instance().GetFont("Small"), scoreText);
        spriteBatch.DrawString(AssetManager.Instance().GetFont("Small"), scoreText, new Vector2(midText.X + 145.0f, 10.0f), Color.Black);

        // Pause menu
        if(m_IsPaused) PauseMenu(spriteBatch);
    }

    private void PauseMenu(SpriteBatch spriteBatch)
    {
        SpriteFont largeFont = AssetManager.Instance().GetFont("Large");
        SpriteFont mediumFont = AssetManager.Instance().GetFont("Medium");

        // Pause title render
        spriteBatch.DrawString(largeFont, m_PauseText, Game1.CenterText(largeFont, m_PauseText) - new Vector2(0.0f, 50.0f), Color.CadetBlue);

        // To menu text render
        spriteBatch.DrawString(mediumFont, m_ToMenuText, Game1.CenterText(mediumFont, m_ToMenuText), Color.CadetBlue);
    }
    #endregion
}