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

    [SerializeField] private ParticleSystem lightning1;
    [SerializeField] private ParticleSystem lightning2;

    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState spineAnimationState;

    private TrackEntry trayCurrentAnimation;
    private TrackEntry trayAddedAnimation;
    private Player.State state;
    private Player.PlayerSkill currentSkill;

    private string currentAnimation;

    private bool triggerToUseSkill = true;
    private bool triggerToTransform = true;

    private void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeletonAnimation.skeleton.SetSkin(Player.Instance.GetCurrentSkin().ToString());

        skeletonAnimation.state.Event += SkeletonAnimation_Event;

        Player.Instance.OnChangeState += Player_OnChangeState;

        state = Player.State.Idle;
        NewSetAnimation(idle, true, 1);
        //currentAnimation = idle.name;
        //SetAnimation(idle, true);
    }

    private void SkeletonAnimation_Event(TrackEntry trackEntry, Spine.Event e)
    {
        switch (e.Data.Name)
        {
            case "to_idle":
            case "to_idle_p":
                lightning1.gameObject.SetActive(false);
                lightning2.gameObject.SetActive(false);
                Player.Instance.BackToIdleState();
                triggerToTransform = true;
                break;
            case "attack":
                Player.Instance.SpawnKamehaSkill();
                break;
            case "attack_3":
                Player.Instance.SpawnSpiritBoomSkill();
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

                // Play particle effect
                if (lightning1 != null)
                    lightning1.gameObject.SetActive(true);
                if (lightning2 != null)
                    lightning2.gameObject.SetActive(true);
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

        // Play particle effect
        if (lightning1 != null)
            lightning1.gameObject.SetActive(true);
        if (lightning2 != null)
            lightning2.gameObject.SetActive(true);
    }

    private void SetUseGatherSkillAnimation(Player.PlayerSkill skill)
    {
        currentSkill = skill;
        triggerToUseSkill = true;
        switch (skill)
        {
            case Player.PlayerSkill.KamehaSkill:
                NewSetAnimation(tuSkill0, false, 1);
                LeanTween.delayedCall(2.2f, () =>
                {
                    NewSetAnimation(skill0, false, 1);
                });
                //AddAnimation(skill0, false, 1);
                break;
            case Player.PlayerSkill.SpiritBoomSkill:
                NewSetAnimation(tuSkill1, false, 1);
                LeanTween.delayedCall(2.2f, () =>
                {
                    NewSetAnimation(skill3, false, 1);
                });
                break;
        }

        // Play particle effect
        if (lightning1 != null)
            lightning1.gameObject.SetActive(true);
        if (lightning2 != null)
            lightning2.gameObject.SetActive(true);
    }
}
