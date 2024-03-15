using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine) : base(player, playerData, playerStateMachine)
    {
    }


    
    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        Debug.Log("Entered Idle");
        player.rb.drag = playerData.IdleDrag;

        player.cc.size = playerData.NormalSize;
        player.cc.offset = playerData.NormalOffset;

        base.Enter(); 
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
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
            player.rb.drag = playerData.IdleDrag;
            player.rb.gravityScale = playerData.GroundGravity;
        }




        // ---------------- State Changers --------------------- \\
        if (xInput != 0)
        {
            playerStateMachine.ChangeState(player.movingState);
            if (player.inputHandler.holdingSprint)
            {
                playerStateMachine.ChangeState(player.sprintingState);
            }
        }

        if(crouching)
        {
            if (xInput != 0)
                playerStateMachine.ChangeState(player.crouchMoveState);
            else
                playerStateMachine.ChangeState(player.crouchIdleState);
        }

        


    }
}