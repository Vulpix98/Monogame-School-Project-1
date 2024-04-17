using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheHorde;

public class CollisionManager
{
    #region Constructor
    public CollisionManager()
    {
        // Subscribing to events
        Bullet.BulletCollisionEvent += OnBulletCollision;
    }
    #endregion

    #region Collision methods
    // Calculates pixel-perfect collisions given two entities
    public static bool OnPixelCollision(IEntity entityA, IEntity entityB)
    {
        // This code is provided by the user "pek" in 
        // https://gamedev.stackexchange.com/questions/15191/is-there-a-good-way-to-get-pixel-perfect-collision-detection-in-xna
        // All credits and thanks go to him

        // Getting the raw color data from the textures
        Color[] entityARawData = new Color[entityA.Texture.Width * entityA.Texture.Height];
        entityA.Texture.GetData<Color>(entityARawData);
        Color[] entityBRawData = new Color[entityB.Texture.Width * entityB.Texture.Height];
        entityB.Texture.GetData<Color>(entityBRawData);

        // Calculating the intersecting rectangle
        // Could be calculated with "if statements" as well, but this is more consice
        int intersectingRec1X = MathHelper.Max(entityA.Collider.X, entityB.Collider.X); // Colliding on the left
        int intersectingRec2X = MathHelper.Min(entityA.Collider.X + entityA.Collider.Width, entityB.Collider.X + entityB.Collider.Width); // Colliding on the right

        int intersectingRec1Y = MathHelper.Max(entityA.Collider.Y, entityB.Collider.Y); // Colliding on the top
        int intersectingRec2Y = MathHelper.Min(entityA.Collider.Y + entityA.Collider.Height, entityB.Collider.Y + entityB.Collider.Height); // Colliding on the bottom

        // Looping through each intersecting pixel
        for(int i = intersectingRec1Y; i < intersectingRec2Y; i++)
        {
            for(int j = intersectingRec1X; j < intersectingRec2X; j++)
            {
                // Get the color of the current pixel(for each entity)
                Color entityAPixelColor = entityARawData[(j - entityA.Collider.X) + (i - entityA.Collider.Y) * entityA.Texture.Width];
                Color entityBPixelColor = entityBRawData[(j - entityB.Collider.X) + (i - entityB.Collider.Y) * entityB.Texture.Width];

                // If both of the current colors' alpha channel is not 0(not transparent),
                // then there is a collision between the entities  
                if(entityAPixelColor.A > 0 && entityBPixelColor.A > 0)
                    return true;
            }
        }

        // No collisions occured
        return false;
    }
    #endregion

    #region Event-related methods
    public void OnBulletCollision(Bullet bullet, IEntity entity)
    {
        entity.TakeDamage(bullet.Damage);
        bullet.Health = 0;
    }
    #endregion
}