using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPicoloVisual : MonoBehaviour
{

    [SerializeField] private Enemy enemy;

    [SerializeField] private AnimationReferenceAsset idle;
    [SerializeField] private AnimationReferenceAsset skill0;
    [SerializeField] private AnimationReferenceAsset skill1;
    [SerializeField] private AnimationReferenceAsset skill2;

    private EnemySO enemySO;
    private float skill1Tracktime;
    private float skill2Tracktime;
    private float skill3Tracktime;

    public enum State
    {
        Idle,
        UseSkill1,
        UseSkill2,
        UseSkill3
    }

    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState spineAnimationState;

    private string currentAnimation;
    private bool triggerToUseSkill = true;
    private TrackEntry trayAnimation;
    private State state;

    private void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;

        state = State.Idle;
        enemy.OnUseSkill += Enemy_OnUseSkill;

        enemySO = DataManager.Instance.GetCurrentEnemySO();
        skill1Tracktime = enemySO.skill1Tracktime;
        skill2Tracktime = enemySO.skill2Tracktime;
        skill3Tracktime = enemySO.skill3Tracktime;


    }

    private void Update()
    {
        SetAnimationOnUpdate();
    }

    private void SetAnimation(AnimationReferenceAsset animationAsset, bool loop)
    {
        string name = animationAsset.name;
        if (name == currentAnimation) return;

        trayAnimation = spineAnimationState.SetAnimation(0, name, loop);
        currentAnimation = name;
    }

    private void SetAnimationOnUpdate()
    {
        switch (state)
        {
            case State.Idle:
                SetAnimation(idle, true);
                break;
            case State.UseSkill1:
                SetAnimation(skill0, false);
                if (trayAnimation.trackTime > skill1Tracktime && triggerToUseSkill)
                {
                    enemy.useSkill();
                    triggerToUseSkill = false;
                }
                break;
            case State.UseSkill2:
                SetAnimation(skill1, false);
                if (trayAnimation.trackTime > skill2Tracktime && triggerToUseSkill)
                {
                    enemy.useSkill();
                    triggerToUseSkill = false;
                }
                break;
            case State.UseSkill3:
                SetAnimation(skill2, false);
                if (trayAnimation.trackTime > skill3Tracktime && triggerToUseSkill)
                {
                    enemy.useSkill();
                    triggerToUseSkill = false;
                }
                break;
        }

        if (trayAnimation.IsComplete && state != State.Idle)
        {
            state = State.Idle;
            triggerToUseSkill = true;
        }
     
    }
    

    private void Enemy_OnUseSkill(Enemy.EnemySkillypes skill)
    {
        switch (skill)
        {
            case Enemy.EnemySkillypes.Skill1:
                state = State.UseSkill1;
                break;
            case Enemy.EnemySkillypes.Skill2:
                state = State.UseSkill2; 
                break;
            case Enemy.EnemySkillypes.Skill3:
                state = State.UseSkill3;
                break;
        }
    }

}
