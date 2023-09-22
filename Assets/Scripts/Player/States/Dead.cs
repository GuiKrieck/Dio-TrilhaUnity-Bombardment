using UnityEngine;

public class Dead : State
{

    private PlayerController controller;
    public Dead(PlayerController controller) : base("Dead")
    {
        this.controller = controller;

    }

    public override void Enter()
    {
        base.Enter();
        controller.characterAnimator.SetTrigger("tGameOver");
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

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
