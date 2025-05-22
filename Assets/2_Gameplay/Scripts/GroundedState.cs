using UnityEngine;

namespace Gameplay
{
    public class GroundedState : IMovementState
    {
        private readonly Character _character;
        private readonly PlayerController _controller;

        public GroundedState(Character character, PlayerController controller)
        {
            _character = character;
            _controller = controller;
        }

        public void Enter() { }
        public void Exit() { }

        public void Update() { }

        public void HandleInput(Vector2 input)
        {
            Vector3 direction = new Vector3(input.x, 0, input.y); 
            _character.SetDirection(direction);
        }
        public void HandleJump()
        {
            _controller.RunJumpCoroutine();
            _controller.ChangeState(new JumpingState(_character, _controller));
        }
    }

}
