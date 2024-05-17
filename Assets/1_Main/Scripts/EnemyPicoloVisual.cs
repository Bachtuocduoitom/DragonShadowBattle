using Spine.Unity;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPicoloVisual : MonoBehaviour
{

    private const string ATTACK_1 = "attack_1";
    private const string ATTACK_2 = "attack_2";
    private const string ATTACK_3 = "attack_3";

    [SerializeField] private Enemy enemy;

    [SerializeField] private AnimationReferenceAsset idle;
    [SerializeField] private AnimationReferenceAsset skill0;
    [SerializeField] private AnimationReferenceAsset skill1;
    [SerializeField] private AnimationReferenceAsset skill2;

    [SerializeField] private ParticleSystem lightning1;
    [SerializeField] private ParticleSystem lightning2;

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
    private TrackEntry trayAnimation;
    private State state;

    private void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;

        skeletonAnimation.state.Event += SkeletonAnimation_Event;

        state = State.Idle;
        enemy.OnUseSkill += Enemy_OnUseSkill;
    }

    private void SkeletonAnimation_Event(TrackEntry trackEntry, Spine.Event e)
    {
        switch (e.Data.Name)
        {
            case "s_attack1":
            case "s_attack2":
            case "s_attack3":
                if (lightning1 != null)
                    lightning1.gameObject.SetActive(true);
                if (lightning2 != null)
                    lightning2.gameObject.SetActive(true);
                break;
            case ATTACK_1: 
                enemy.UseSkill();
                break;
            case ATTACK_2: 
                enemy.UseSkill();
                break;
            case ATTACK_3 : 
                enemy.UseSkill();
                break;
            case "to_idle_1":
            case "to_idle_2":
            case "to_idle_3":
                enemy.CompleteUsingSkill();
                lightning1.gameObject.SetActive(false);
                lightning2.gameObject.SetActive(false);
                break;
        }
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
                break;
            case State.UseSkill2:
                SetAnimation(skill1, false);
                break;
            case State.UseSkill3:
                SetAnimation(skill2, false);
                break;
        }

        if (trayAnimation.IsComplete && state != State.Idle)
        {
            state = State.Idle;
        }
    }
    
    private void Enemy_OnUseSkill(Enemy.EnemySkillTypes skill)
    {
        switch (skill)
        {
            case Enemy.EnemySkillTypes.Skill1:
                state = State.UseSkill1;
                break;
            case Enemy.EnemySkillTypes.Skill2:
                state = State.UseSkill2; 
                break;
            case Enemy.EnemySkillTypes.Skill3:
                state = State.UseSkill3;
                break;
        }
    }

}
