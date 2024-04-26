using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{

    public static ExplosionSpawner Instance { get; private set; }

    [SerializeField] private ParticleSystem explosionFx1;
    [SerializeField] private ParticleSystem explosionFx2;
    [SerializeField] private ParticleSystem explosionFx3;
    [SerializeField] private ParticleSystem enemyExplosion;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayExplosionSmall(Vector3 position)
    {
        ParticleSystem explosionFx = Instantiate(explosionFx1, position, Quaternion.identity);
        explosionFx.Play();
    }

    public void PlayExplosionMedium(Vector3 position)
    {
        ParticleSystem explosionFx = Instantiate(explosionFx2, position, Quaternion.identity);
        explosionFx.Play();
    }

    public void PlayExplosionLarge(Vector3 position)
    {
        ParticleSystem explosionFx = Instantiate(explosionFx3, position, Quaternion.identity);
        explosionFx.Play();
    }

    public void PlayEnemyExplosion(Vector3 position)
    {
        ParticleSystem explosionFx = Instantiate(enemyExplosion, position, Quaternion.identity);
        explosionFx.Play();
    }

    public void PlayPlayerExplosion(Vector3 position)
    {
        ParticleSystem explosionFx = Instantiate(enemyExplosion, position, Quaternion.identity);
        explosionFx.Play();
    }

    public void PlayEnemySkillExposion(EnemySkill.SizeType sizeType, Vector3 position)
    {
        switch (sizeType)
        {
            case EnemySkill.SizeType.Small:
                PlayExplosionSmall(position);
                break;
            case EnemySkill.SizeType.Medium:
                PlayExplosionMedium(position);
                break;
            case EnemySkill.SizeType.Large:
                PlayExplosionLarge(position);
                break;
        }
    }

    public void PlayPlayerSkillExposion(PlayerSkillBase.SizeType sizeType, Vector3 position)
    {
        switch (sizeType)
        {
            case PlayerSkillBase.SizeType.Small:
                PlayExplosionSmall(position);
                break;
            case PlayerSkillBase.SizeType.Medium:
                PlayExplosionMedium(position);
                break;
            case PlayerSkillBase.SizeType.Large:
                PlayExplosionLarge(position);
                break;
        }
    }


}
