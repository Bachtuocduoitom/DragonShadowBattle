using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private AnimationReferenceAsset idle;
    [SerializeField] private AnimationReferenceAsset skill0;
    [SerializeField] private AnimationReferenceAsset skill1;
    [SerializeField] private AnimationReferenceAsset skill2;
    [SerializeField] private AnimationReferenceAsset skill3;
    [SerializeField] private AnimationReferenceAsset skill4;
    [SerializeField] private AnimationReferenceAsset transformSaiya;
    [SerializeField] private AnimationReferenceAsset tuSkill0;
    [SerializeField] private AnimationReferenceAsset tuSkill1;

    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState spineAnimationState;

    private bool triggerToTransform = true;

    private Player.PlayerSkill currentSkill;

    private string currentAnimation;
    private TrackEntry trayCurrentAnimation;
    private TrackEntry trayAddedAnimation;
    private Player.State state;
    private bool triggerToUseSkill = true;

    private void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeletonAnimation.skeleton.SetSkin(Player.Instance.GetCurrentSkin().ToString());

        Player.Instance.OnChangeState += Player_OnChangeState;

        state = Player.State.Idle;
        NewSetAnimation(idle, true, 1);
        //currentAnimation = idle.name;
        //SetAnimation(idle, true);
    }


    private void Update()
    {
        SetAnimationOnUpdate();
    }

    private void SetAnimation(AnimationReferenceAsset animationAsset, bool loop)
    {
        string name = animationAsset.name;
        if (name == currentAnimation) return;

        trayCurrentAnimation = spineAnimationState.SetAnimation(0, name, loop);
        currentAnimation = name;             
    }

    private void NewSetAnimation(AnimationReferenceAsset animationAsset, bool loop, float timeScale)
    {
        string name = animationAsset.name;
        trayCurrentAnimation = spineAnimationState.SetAnimation(0, name, loop);
        trayCurrentAnimation.TimeScale = timeScale;
    }

    private void AddAnimation(AnimationReferenceAsset animationAsset, bool loop, float timeScale)
    {
        if (animationAsset == null) return;
        string name = animationAsset.name;
        trayAddedAnimation = spineAnimationState.AddAnimation(0, name, loop, 0);
        trayAddedAnimation.TimeScale = timeScale;
    }

    private void SetAnimationOnUpdate()
    {
        /*
        switch (state)
        {
            case Player.State.Idle:
                SetAnimation(idle, true);
                break;
            case Player.State.UseNormalSkill:
                SetAnimation(skill0, false);
                break;
            case Player.State.Transform:
                break;
            case Player.State.UseGatherSkill:
                SetAnimation(tuSkill0, false);
                break;
        }*/

        /*
        if (trayCurrentAnimation.IsComplete && state != Player.State.Idle)
        { 
            if (state == Player.State.UseGatherSkill)
            {
                SetAnimation(skill1, false);
            } else
            {
                player.BackToIdleState();
            }
        }
        */
        // Handle Transform animation
        if (state == Player.State.Transform && trayCurrentAnimation.trackTime > 0.7f && triggerToTransform)
        {
            skeletonAnimation.skeleton.SetSkin(Player.Instance.GetCurrentSkin().ToString());
            triggerToTransform = false;

            // Player finish transform
            Player.Instance.FinishTransform();
        }

        //Handle Normal Skill animation
        if (state == Player.State.UseNormalSkill)
        {
            if (triggerToUseSkill)
            {
                if (currentSkill == Player.PlayerSkill.SpamSKill)
                {
                    Player.Instance.SpawnSpamSkill();
                    triggerToUseSkill = false;
                }
                else if (currentSkill == Player.PlayerSkill.DonSkill)
                {
                    Player.Instance.SpawnDonSkill();
                    triggerToUseSkill = false;
                } 
                else if (currentSkill == Player.PlayerSkill.DragonSkill)
                {
                    Player.Instance.SpawnDragonSkill();
                    triggerToUseSkill = false;
                }
            }
        }

        
        if (trayCurrentAnimation.IsComplete && state != Player.State.Idle)
        {
            if (state == Player.State.UseNormalSkill)
            {
                Player.Instance.BackToIdleState();
            }
            else if (state == Player.State.Transform)
            {
                Player.Instance.BackToIdleState();

                triggerToTransform = true;
            }
            else if (state == Player.State.UseGatherSkill)
            {
                //Handle Gather Skill animation

                if (trayAddedAnimation == null) return;

                if (triggerToUseSkill)
                {
                    if (trayAddedAnimation.trackTime > 0.2f && currentSkill == Player.PlayerSkill.KamehaSkill)
                    {
                        Player.Instance.SpawnKamehaSkill();
                        triggerToUseSkill = false;
                    } else if (trayAddedAnimation.trackTime > 0.2f && currentSkill == Player.PlayerSkill.SpiritBoomSkill)
                    {
                        Player.Instance.SpawnSpiritBoomSkill();
                        triggerToUseSkill = false;
                    }

                }
                
                if (trayAddedAnimation.IsComplete)
                {
                    Player.Instance.BackToIdleState();
                    trayAddedAnimation = null;
                }               
            }
        }

    }

    private void Player_OnChangeState(Player.State playerState, Player.PlayerSkill skill)
    {
        switch (playerState)
        {
            case Player.State.Idle:
                NewSetAnimation(idle, true, 1);
                break;
            case Player.State.UseNormalSkill:
                SetUseNormalSkillAnimation(skill);
                break;
            case Player.State.UseGatherSkill: 
                SetUseGatherSkillAnimation(skill);
                break;
            case Player.State.Transform:
                NewSetAnimation(transformSaiya, false, 1);
                break;
        }
        state = playerState;
    }



    private void SetUseNormalSkillAnimation(Player.PlayerSkill skill)
    {
        currentSkill = skill;
        triggerToUseSkill = true;
        switch (skill)
        {
            case Player.PlayerSkill.SpamSKill:
                NewSetAnimation(skill1, false, 1);
                break;
            case Player.PlayerSkill.DonSkill:
                NewSetAnimation(skill2, false, 1);
                break;
            case Player.PlayerSkill.DragonSkill:
                NewSetAnimation(skill4, false, 1);
                break;
        }
    }

    private void SetUseGatherSkillAnimation(Player.PlayerSkill skill)
    {
        currentSkill = skill;
        triggerToUseSkill = true;
        switch (skill)
        {
            case Player.PlayerSkill.KamehaSkill:
                NewSetAnimation(tuSkill0, false, 1);
                AddAnimation(skill0, false, 1);
                break;
            case Player.PlayerSkill.SpiritBoomSkill:
                NewSetAnimation(tuSkill1, false, 1);
                AddAnimation(skill3, false, 1);
                break;
        }
    }
}
