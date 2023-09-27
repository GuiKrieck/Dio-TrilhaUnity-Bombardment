using UnityEngine;

public class Walking : State
{

    private PlayerController controller;
    public Walking(PlayerController controller) : base("Walking")
    {
        this.controller = controller;
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //switch to attack
        if (controller.Attack())
        {
            return;
        }

        //switch to Jump

        if (controller.hasJumpInput)
        {
            controller.stateMachine.ChangeState(controller.jumpState);
            return;
        }

        //switch to idle

        if (controller.movementVector.IsZero())
        {
            controller.stateMachine.ChangeState(controller.idleState);
            return;
        }


    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        Vector3 walkVector = new Vector3(controller.movementVector.x, 0, controller.movementVector.y);
        //this will generate a rotated new vector based on camera, so the character movement will be base on the camera and not on the world position anymore;
        walkVector = controller.getForward() * walkVector;
        walkVector *= controller.movementSpeed;

        //Apply  input to character
        controller.characterRigidbody.AddForce(walkVector, ForceMode.Force);
        //Rotate Character
        controller.RotateBodyToFaceInput();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }
}