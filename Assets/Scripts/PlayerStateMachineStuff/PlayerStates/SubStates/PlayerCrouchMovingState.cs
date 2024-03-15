using UnityEngine.XR;

public class PlayerCrouchMovingState : PlayerGroundedState
{
    public PlayerCrouchMovingState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }
    float xInput;
    float xInputRaw;
    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        UnityEngine.Debug.Log("Entered Crouch Moving State");
        player.rb.drag = playerData.GroundDrag;
        player.cc.size = playerData.CrouchSize;
        player.cc.offset = playerData.CrouchOffset;

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rb.AddForce(new UnityEngine.Vector2(xInputRaw * playerData.CrouchSpeed, 0));
    }

    public override void Update()
    {
        base.Update();

        // ----------------- Slope Shit ------------------- \\
        if (Slope)
        {
            player.rb.drag = playerData.SlopeDrag;
            player.rb.gravityScale = playerData.SlopeGravity;
        }
        else
        {
            player.rb.drag = playerData.GroundDrag;
            player.rb.gravityScale = playerData.GroundGravity;
        }

        xInput = player.inputHandler.moveDir.x;
        xInputRaw = player.inputHandler.moveDirRaw.x;

        if (!player.inputHandler.holdingCrouch)
        {
            if (xInput != 0)
                playerStateMachine.ChangeState(player.movingState);
            else
                playerStateMachine.ChangeState(player.idleState);
        }

        if(xInput == 0)
        {
            playerStateMachine.ChangeState(player.crouchIdleState);
        }
    }
}