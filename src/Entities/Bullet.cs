using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public class Bullet : DynamicEntity
{
    #region Consts
    // Pistol consts
    private const int PISTOL_MAX_DIST = 125;
    private const int PISTOL_DAMAGE = 20;

    // Shotgun consts
    private const int SHOTGUN_MAX_DIST = 75;
    private const int SHOTGUN_DAMAGE = 20;
    
    private const int MAX_LIFETIME = 100;
    #endregion

    #region Fields
    // Public variables
    public int Damage {get; set;}
    public int MaxDist {get; set;}
    public BulletType Type;

    // Private variables
    private int m_LifeTime;
    private Vector2 m_OriginalPosition;
    #endregion

    #region Events
    public static event BulletCollision BulletCollisionEvent;
    #endregion

    #region Constructor
    public Bullet(Vector2 position, Texture2D texture, BulletType type)
        :base(position, texture, 1)
    {
        Velocity = new Vector2(0.0f, -400.0f);

        Type = type;

        // Defining the bullet's parametars depending on its type
        switch(Type)
        {
            case BulletType.Pistol:
                Damage = PISTOL_DAMAGE;
                MaxDist = PISTOL_MAX_DIST;
                break;
            case BulletType.Shootgun:
                Damage = SHOTGUN_DAMAGE;
                MaxDist = SHOTGUN_MAX_DIST;
                break;
        }

        m_OriginalPosition = position;
        m_LifeTime = MAX_LIFETIME;
    }
    #endregion

    #region Methods
    public override void Update(GameTime gameTime)
    {
        // Decreasing the lifetime
        m_LifeTime--;

        // Decreasing the damage once the bullet is out of it's effective range
        if(Vector2.Distance(Position, m_OriginalPosition) >= MaxDist)
            Damage /= 2;

        // Disabling the bullet once it's lifetime is 0, or it is outside the screen's borders
        if(m_LifeTime <= 0 || Position.Y < 0) Health = 0;

        base.Update(gameTime);
    }

    public override void CollisionUpdate(List<IEntity> entities)
    {
        foreach(var entity in entities)
        {
            if(entity is Zombie)
            {
                if(CollisionManager.OnPixelCollision(this, entity))
                    BulletCollisionEvent?.Invoke(this, entity as Zombie);
            }
        }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        if(IsActive)
            spriteBatch.Draw(Texture, Position, Color.White);
    }

    public override void Move(GameTime gameTime)
    {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    #endregion
}