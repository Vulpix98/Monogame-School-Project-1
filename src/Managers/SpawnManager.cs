using Microsoft.Xna.Framework;

using System.Collections.Generic;

namespace TheHorde;

public class SpawnManager
{
    #region Conts
    // Basic zombie consts
    private const int BASIC_HEALTH = 40;
    private const int BASIC_DAMAGE = 12;
    private const float BASIC_SPEED = 75.0f;
    
    // Brute zombie consts
    private const int BRUTE_HEALTH = 80;
    private const int BRUTE_DAMAGE = 40;
    private const float BRUTE_SPEED = 40.0f;
    
    // Denizen zombie consts
    private const int DENIZEN_HEALTH = 20;
    private const int DENIZEN_DAMAGE = 8;
    private const float DENIZEN_SPEED = 125.0f;
    #endregion

    #region Fields
    public Vector2 Position {get; set;}
    private List<Vector2> m_SpawnPoints = new List<Vector2>();
    private EntityManager m_EntityManager;
    private int m_Timer;
    private int m_MaxTime;
    private int m_DifficultyTimer;
    private int m_SpawnCounter;
    #endregion

    #region Constructor
    public SpawnManager(EntityManager entityManager, Vector2 position)
    {
        Position = position;

        // Adding spawn points
        for(int i = 0; i < 6; i++)
        {
            m_SpawnPoints.Add(new Vector2(i * 64.0f, 0.0f));
        }

        m_EntityManager = entityManager;

        m_Timer = 0;
        m_MaxTime = 150;
        m_DifficultyTimer = 0;
        m_SpawnCounter = 1;
    }
    #endregion

    #region Methods
    public void Update()
    {
        m_Timer++;

        // This timer will define the difficulty of the game.
        // Once this timer is passed a certain threshold, the zombies will begin to spawn more frequently.
        m_DifficultyTimer++;

        // The max time will decrease by 10 every 1000 ticks
        if(m_DifficultyTimer % 1000 == 0)
            m_MaxTime -= 10;

        if(m_Timer >= m_MaxTime)
        {
            // Adding a zombie
            if(m_SpawnCounter % 10 == 0) SpawnEntity(ZombieType.Brute);
            else if(m_SpawnCounter % 5 == 0) SpawnEntity(ZombieType.Denizen);
            else SpawnEntity(ZombieType.Basic);
            
            // Reseting the position of the spawner to a new random position
            Position = m_SpawnPoints[Game1.Random.Next(0, m_SpawnPoints.Count - 1)];

            // Reseting the timer
            m_Timer = 0;

            // Everytime a zombie spawns this counter will go up.
            // This will help with selecting which zombie to spawn next turn.
            // For example, if every 5  there is a brute zombie, and every 3 there is a denizen zombie.
            m_SpawnCounter++;
        }
    }

    // Spawns an entity based on the given type
    public void SpawnEntity(ZombieType type)
    {
        switch(type)
        {
            case ZombieType.Basic:
                m_EntityManager.Entities.Add(new Zombie(Position, 
                                                        AssetManager.Instance().GetSprite("BasicZombie"),  
                                                        BASIC_HEALTH, 
                                                        BASIC_DAMAGE, 
                                                        BASIC_SPEED));
                break;
            case ZombieType.Brute:
                m_EntityManager.Entities.Add(new Zombie(Position, 
                                                        AssetManager.Instance().GetSprite("BruteZombie"),  
                                                        BRUTE_HEALTH, 
                                                        BRUTE_DAMAGE, 
                                                        BRUTE_SPEED));
                break;
            case ZombieType.Denizen:
                m_EntityManager.Entities.Add(new Zombie(Position, 
                                                        AssetManager.Instance().GetSprite("DenizenZombie"),  
                                                        DENIZEN_HEALTH, 
                                                        DENIZEN_DAMAGE, 
                                                        DENIZEN_SPEED));
                break;
        }
    }
    #endregion
}