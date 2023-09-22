using UnityEngine;

public class Jump : State
{
    public bool hasJumped;
    private float coolDown;

    private PlayerController controller;
    public Jump(PlayerController controller) : base("Jump")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        hasJumped = false;
        coolDown = 0.95f;

        //handle Animator
        controller.characterAnimator.SetBool("bJumping", true);
    }

    public override void Exit()
    {
        base.Exit();
        controller.characterAnimator.SetBool("bJumping", false);
    }

    public override void Update()
    {
        base.Update();

        coolDown -= Time.deltaTime;

        if(hasJumped  && coolDown <= 0) 
        {
            controller.stateMachine.ChangeState(controller.idleState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        //jump
        if (!hasJumped)
        {
            hasJumped = true;
            ApplyJumpImpulse();
        }

        Vector3 walkVector = new Vector3(controller.movementVector.x, 0, controller.movementVector.y);
        //this will generate a rotated new vector based on camera, so the character movement will be base on the camera and not on the world position anymore;
        walkVector = controller.getForward() * walkVector;
        walkVector *= controller.movementSpeed * controller.jumpMovementFactor;

        //Apply  input to character
        controller.characterRigidbody.AddForce(walkVector, ForceMode.Force);
        //Rotate Character
        controller.RotateBodyToFaceInput();

    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    private void ApplyJumpImpulse()
    {
        //Aply Impulse
        Vector3 forceVector = Vector3.up * controller.jumpPower;
        controller.characterRigidbody.AddForce(forceVector, ForceMode.Impulse);
    }
}
