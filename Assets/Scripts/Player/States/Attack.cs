using UnityEngine;

public class Attack : State
{
    private PlayerController controller;
    public Attack(PlayerController controller) : base("Attack")
    {
        this.controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        controller.swordHitbox.SetActive(true);
        controller.characterAnimator.SetBool("bAttack", true);

        
    }

    public override void Exit()
    {
        base.Exit();

       
       controller.characterAnimator.SetBool("bAttack", false);
       controller.swordHitbox.SetActive(false); 
       
    }

    public override void Update()
    {
        base.Update();
        if (!controller.Attack())
        {
            controller.waitALittle();
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
