using UnityEngine;

namespace Gameplay
{
    public class JumpingState : IMovementState
    {
        private readonly Character _character;
        private readonly PlayerController _controller;

        public JumpingState(Character character, PlayerController controller)
        {
            _character = character;
            _controller = controller;
        }

        public void Enter() { }
        public void Exit() { }

        public void Update() { }

        public void HandleInput(Vector2 input)
        {
            Vector3 direction = new Vector3(input.x, 0, input.y) * _controller.AirborneSpeedMultiplier;
            _character.SetDirection(direction * _controller.AirborneSpeedMultiplier);
        }
        public void HandleJump()
        {
            if (_controller.JumpsUsed < _controller.MaxJumps)
            {
                _controller.RunJumpCoroutine();
                _controller.JumpsUsed++;
            }
        }
    }

}
