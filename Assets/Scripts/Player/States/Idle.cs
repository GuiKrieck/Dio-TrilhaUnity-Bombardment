using UnityEngine;

public class Idle : State
{
    private PlayerController controller;
    public Idle(PlayerController controller) : base("Idle")
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

        // Switch to walking state
        if (controller.hasJumpInput)
        {
            controller.stateMachine.ChangeState(controller.jumpState);
            return;
        }

        // Switch to walking state
        if (!controller.movementVector.IsZero())
        {
            controller.stateMachine.ChangeState(controller.walkingState);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }
}
