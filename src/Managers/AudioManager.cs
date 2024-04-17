using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace TheHorde;

public class AudioManager
{
    #region Fields
    public float EffectsVolume;
    public float Pitch;
    #endregion

    #region Constructor
    public AudioManager()
    {
        EffectsVolume = 0.1f;
        Pitch = 0.0f;

        Zombie.ZombieGrowlAudioEvent += OnZombieGrowl;
        Zombie.ZombieDeathAudioEvent += OnZombieDeath;
        Zombie.BarricadeHitAudioEvent += OnBarricadeHit;
        Player.BulletShotAudioEvent += OnBulletShot;
    }
    #endregion

    #region Methods
    public void OnZombieDeath()
    {
        AssetManager.Instance().GetSound("ZombieDeath").Play(EffectsVolume, Pitch, 0.0f);
    }

    public void OnZombieGrowl(ZombieType zombieType)
    {
        switch(zombieType)
        {
            case ZombieType.Basic:
                AssetManager.Instance().GetSound("BasicGrowl").Play(EffectsVolume, Pitch, 0.0f);
                break;
            case ZombieType.Brute:
                AssetManager.Instance().GetSound("BruteGrowl").Play(EffectsVolume, Pitch, 0.0f);
                break;
            case ZombieType.Denizen:
                AssetManager.Instance().GetSound("DenizenGrowl").Play(EffectsVolume, Pitch, 0.0f);
                break;    
        }
    }

    public void OnBulletShot(BulletType bulletType)
    {
        switch(bulletType)
        {
            case BulletType.Pistol:
                AssetManager.Instance().GetSound("Pistol").Play(EffectsVolume, Pitch, 0.0f);
                break;
            case BulletType.Shootgun:
                AssetManager.Instance().GetSound("Shotgun").Play(EffectsVolume, Pitch, 0.0f);
                break;
        }
    }

    public void OnBarricadeHit()
    {
        AssetManager.Instance().GetSound("Barricade").Play(EffectsVolume, Pitch, 0.0f);
    }
    #endregion
}