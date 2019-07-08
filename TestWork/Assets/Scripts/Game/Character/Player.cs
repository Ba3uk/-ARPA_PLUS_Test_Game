using UnityEngine;

namespace Character
{
    public class Player : Character
    {
        [Header("Joystick")]
        [SerializeField] bl_Joystick _joystick = null;

        private float velocity;

        public override void Initilize()
        {
            base.Initilize();
        }
        void Start()
        {
            Initilize();
        }

        void FixedUpdate()
        {
            if (_joystick.inputAlignWithCameraDirection != Vector3.zero)
            {
                var targetPos = transform.position + new Vector3(_joystick.inputAlignWithCameraDirection.x / 5, 0.5f, _joystick.inputAlignWithCameraDirection.z / 5);
                MoveToTarget(targetPos);
            }

            velocity = _agent.velocity.magnitude / _agent.speed;

            if (velocity > 0.2f)
            {
                PlayerAnimState = AnimState.Run;
            }
            else if (!isDie)
            {
                PlayerAnimState = AnimState.Idel;
            }
        }

        public void Die()
        {
            isDie = true;
            PlayerAnimState = AnimState.Die;
            _agent.enabled = false;
        }
    }
}