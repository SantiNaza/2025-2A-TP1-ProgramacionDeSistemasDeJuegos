using UnityEngine;

namespace Gameplay
{
    public interface IMovementState
    {
        void Enter();
        void Exit();
        void Update();
        void HandleInput(Vector2 direction);
        void HandleJump();
    }
}
