using UnityEngine;

public class SlidingState : State
{
    [SerializeField] private float _slidingRatio;

    public override void Enter(PlayerMover player)
    {
        base.Enter(player);
        Player.SetCanFlip(false);
    }

    private void Update()
    {
        Player.Move(Vector2.right * Player.SurfaceAngle * _slidingRatio);
    }

    public override void Exit()
    {
        base.Exit();
        Player.SetCanFlip(true);
    }
}