using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    public class Character : MonoBehaviour
    {
      public AnimState PlayerAnimState
        {
            set
            {
                _playerAnimState = value;
                if (_animator != null)
                {
                    _animator.SetInteger("AnimState", (int)_playerAnimState);
                }
            }
            get { return _playerAnimState; }
        }

        public bool isDie;

        protected NavMeshAgent _agent;
        private AnimState _playerAnimState;
        private Animator _animator;


        private void Start()
        {
            Initilize();
        }

        public virtual void Initilize()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        // Движение к точке через PathFinding;
        public void MoveToTarget(Vector3 targetPos)
        {
            if (_agent != null)
            {
                _agent.SetDestination(targetPos);
            }
        }

        public void UpdateSpeed(float speed)
        {
            _agent.speed = speed;
        }

    }

    public enum AnimState
    {
        Idel,
        Walk,
        Run,
        Attack,
        Find,
        Die
    }
}