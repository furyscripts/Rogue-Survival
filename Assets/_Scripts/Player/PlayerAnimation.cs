using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement playerMovement;
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, move;
    public string currentState;
    public string currentAnimation;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        currentState = "Idle";
        SetCharacterState(currentState);
    }

    private void Update()
    {
        ChangeAnimation();
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation)) return;
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    public void SetCharacterState(string state)
    {
        if (state.Equals("Idle")) SetAnimation(idle, true, 1f);
        else if (state.Equals("Move")) SetAnimation(move, true, 2f);
    }

    void ChangeAnimation()
    {
        if (playerMovement.moveDir.x != 0 || playerMovement.moveDir.y != 0)
        {
            SetCharacterState("Move");
            Flip();
        }
        else SetCharacterState("Idle");
    }

    void Flip()
    {
        if (playerMovement.moveDir.x > 0) transform.parent.localScale = new Vector2(1f, 1f);
        else transform.parent.localScale = new Vector2(-1f, 1f);
    }
}
