using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GokuSpamSkill : PlayerSkillBase
{
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 pointC;
    private Vector3 pointD;

    private bool startMove = false;
    private float interpolationAmount = 0f;

    private float scaleFactor = 1.5f;
    private float biggerDuration = 0.4f;


    protected void Start()
    {
        transform.localScale = Vector3.one * 0.1f;
    }

    public override void HandleMovement()
    {
        if ( startMove)
        {
            interpolationAmount += Time.deltaTime * speed;
            transform.position = Evaluate(interpolationAmount);
            
            Vector3 direction = Evaluate(interpolationAmount + 0.001f) - transform.position;

            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out EnemySkill enemySkill))
        {
            ExplosionSpawner.Instance.PlayExplosionSmall(transform.position);
            Destroy(gameObject);
        }
    }

    public void SetUpCurvePosition(Vector3 pointBPosition)
    {
        pointA = new Vector3(transform.position.x, transform.position.y, 0);
        pointB = pointBPosition;
        pointC = pointA + Vector3.right * 5f;
        pointD = pointA + Vector3.right * 20f;

        startMove = true;

        GetBigger();
    }

    private Vector3 Evaluate(float t)
    {
        Vector3 ab_bc = QuadraticLerp(pointA, pointB, pointC, t);
        Vector3 bc_cd = QuadraticLerp(pointB, pointC, pointD, t);
        return Vector3.Lerp(ab_bc, bc_cd, t);
    }

    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(ab, bc, t);
    }

    private void GetBigger()
    {
        LeanTween.scale(gameObject, Vector3.one * scaleFactor, biggerDuration);
    }
}
