using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FMS;

namespace Character
{
    public class Bot : Character
    {
        [Header("Список точек патрулирования")]
        [SerializeField] private List<Transform> _patrulPoints = new List<Transform>();

        [Header("Дистанция смены таргета")]
        [SerializeField] private float _distanceToChangeTarget = 0;

        [Header("Скорость партрулирования")]
        public float SpeedPatrulMove;

        [Header("Скорость приследования")]
        public float SpeedPursuitMove;

        [Header("Время на поиск игрока")]
        public float TimeToSearchPlayer;

        [Header("Дистанция удара")]
        public float DistanceHit = 3.5f;

        [Space]

        [Header("* * * Область видимости * * *", order = 0)]
        [Header("Длина вектора" , order = 1)]
        public float viewRadius;

        [Header("Угол видимости")]
        [Range(0, 360)] public float viewAngle;

        [HideInInspector]
        public Vector3 LastTargetPos;

        private int targetPoint;

        private Player _player;

        public Player Player
        {
            get
            {
                if (_player == null)
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                return _player;
            }
        }

        private IState _currentState;

        private void Start()
        {
            Initilize();
            ChangeState(new Patrol());
        }

        public void Update()
        {
            if (_currentState != null)
                _currentState.Execute(this);
        }

        #region Patrul_State
        public void MoveToNextTarget()
        {
            if(Vector3.Distance(transform.position , _patrulPoints[targetPoint].position) < _distanceToChangeTarget)
            {
               targetPoint = (targetPoint + 1 >= _patrulPoints.Count) ? 0: targetPoint+ 1;
            }
            MoveToTarget(_patrulPoints[targetPoint].position);
        }

        public void FindNearestTarget()
        {
            int posMinDinstance = 0;
            float minDistance = 0;
            float newDistance;
            for (int i = 0; i < _patrulPoints.Count; i++)
            {
                newDistance = Vector3.Distance(transform.position, _patrulPoints[i].position);

                if (i == 0)
                {
                    minDistance = newDistance;
                    continue;
                }

                if (minDistance > newDistance)
                {
                    minDistance = newDistance;
                    posMinDinstance = i;
                }
            }

            targetPoint = posMinDinstance;
        }
        #endregion

        #region Pursuit_state
        public void UpdateLastTargetPos()
        {
            LastTargetPos = Player.transform.position;
        }
        #endregion

        //Проверка видимости игрока
        public bool PlayerDetected()
        {
            if (Vector3.Distance(transform.position, Player.transform.position) < viewRadius)
            {
                var dirToTarget = (Player.transform.position - transform.position).normalized;
                if(Vector3.Angle(transform.forward , dirToTarget) < viewAngle / 2)
                {
                    NavMeshHit hit;
                    var CharHeight = transform.up * _agent.height / 2;

                    if (!_agent.Raycast( Player.transform.position + CharHeight , out hit))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Vector3 DirFromAnglee(float angleInDegrees , bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public void ChangeState(IState newState)
        {
            _currentState = newState;
            newState.Enter(this);
        }
       
        public void KillPlayer()
        {
            var _lastRadius = viewRadius;
            viewRadius = DistanceHit;
            if (PlayerDetected())
            {
                Debug.Log("Die");
                Player.Die();
                viewRadius = 0;
                ChangeState(new Observation());
                return;
            }

            viewRadius = _lastRadius;
        }

        public void OnEndFightAnim()
        {
            ChangeState(new Pursuit());
        }
    }
}