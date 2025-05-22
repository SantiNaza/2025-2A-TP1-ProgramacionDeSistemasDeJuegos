using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private float airborneSpeedMultiplier = .5f;
        [SerializeField] private int maxJumps = 2;

        public int JumpsUsed { get; set; } = 0;
        public int MaxJumps => maxJumps;
        public float AirborneSpeedMultiplier => airborneSpeedMultiplier;

        private Character _character;
        private Coroutine _jumpCoroutine;
        private IMovementState _currentState;

        private void Awake()
            => _character = GetComponent<Character>();

        private void Start()
            => ChangeState(new GroundedState(_character, this));

        public void ChangeState(IMovementState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        private void OnEnable()
        {
            if (moveInput?.action != null)
                moveInput.action.performed += OnMove;

            if (jumpInput?.action != null)
                jumpInput.action.performed += OnJump;
        }

        private void OnDisable()
        {
            if (moveInput?.action != null)
                moveInput.action.performed -= OnMove;

            if (jumpInput?.action != null)
                jumpInput.action.performed -= OnJump;
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            _currentState.HandleInput(ctx.ReadValue<Vector2>());
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            _currentState.HandleJump();
        }

        private void OnCollisionEnter(Collision other)
        {
            foreach (var contact in other.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5)
                {
                    JumpsUsed = 0;
                    ChangeState(new GroundedState(_character, this));
                }
            }
        }

        public void RunJumpCoroutine()
        {
            if (_jumpCoroutine != null)
                StopCoroutine(_jumpCoroutine);

            _jumpCoroutine = StartCoroutine(_character.Jump());
        }
    }

}