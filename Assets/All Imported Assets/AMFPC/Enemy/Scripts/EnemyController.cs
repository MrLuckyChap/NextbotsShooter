using All_Imported_Assets.AMFPC.Scripts;
using CodeBase;
using UnityEngine;
using UnityEngine.AI;

namespace All_Imported_Assets.AMFPC.Enemy.Scripts
{
  public class EnemyController : MonoBehaviour
    {
        public Animator animator;
        public float respawnTime;
        private float _currentRespawnTimer;
        private NavMeshAgent _agent;
        private Vector3 _distination;
        private bool generatedPoint, _distinationSet;
        private float _targetDistance;
        [HideInInspector] public bool dead;
        private EnemyStats enemyStats;
        private HealthManager healthManager;
        private Rigidbody[] rigidBodies;
        private Collider[] colliders;
        private AISpawner _aiSpawner;
        private Transform _playerTransform;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private GameObject _objectRotateToPlayer;
        [SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;
        [SerializeField] private Aggro _aggro;

        private void Awake()
        {
            rigidBodies = animator.gameObject.GetComponentsInChildren<Rigidbody>();
            colliders = animator.gameObject.GetComponentsInChildren<Collider>();

            _agentMoveToPlayer.PlayerCaught += OnPlayerCaught;
            _aggro.PlayerEscaped += OnPlayerCaught;
            // SetRagdoll(false);
        }

        public void Init(AISpawner aiSpawner, Transform playerTransform)
        {
            _aiSpawner = aiSpawner;
            _playerTransform = playerTransform;
            _agentMoveToPlayer.Init(playerTransform);
        }

        private void Start()
        {
            enemyStats = GetComponent<EnemyStats>();
            healthManager = GetComponent<HealthManager>();
            _agent = GetComponent<NavMeshAgent>();
            _currentRespawnTimer = respawnTime;
        }

        private void Update()
        {
            if (_aggro.HasAggroTarget)
            {
              LookAtPlayer();
              return;
            }

            if(!dead)
            {
              Chase();
              LookAtPlayer();
            }

            RespawnTimer();
        }

        public void Chase()
        {
            if(generatedPoint)
            {
                _targetDistance = Vector3.Distance(_distination, transform.position);
                if (!_distinationSet)
                {
                    _agent.destination = _distination;
                    _distinationSet = true;
                }
                if (_targetDistance < 2)
                {
                    generatedPoint = false;
                }
            }
            else
            {
                GenerateDestinationPoint();
            }
        }

        public void GenerateDestinationPoint()
        {
            if(!generatedPoint)
            {
                _distination = _aiSpawner.GetPointForInstantiate();
                generatedPoint = true;
                _distinationSet = false;
            }
        }

        public void KillEnemy()
        {
            _agent.destination = transform.position;
            dead = true;
            gameObject.SetActive(false);
            // GetComponent<CapsuleCollider>().enabled = false;
            SetRagdoll(true);
            animator.enabled = false;
        }

        public void RespawnEnemy()
        {
            animator.enabled = true;
            gameObject.SetActive(true);
            // SetRagdoll(false);
            dead = false;
            healthManager.RestoreHealth();
            generatedPoint = false;
            transform.position = _aiSpawner.GetPointForInstantiate();
            // GetComponent<CapsuleCollider>().enabled = true;
            healthManager.RestoreHealth();
            enemyStats.UpdateEnemyHealthUI();
        }

        private void LookAtPlayer()
        {
          Vector3 directionToPlayer = _playerTransform.position - _objectRotateToPlayer.transform.position;
          float angle = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
          _objectRotateToPlayer.transform.rotation = Quaternion.Slerp(_objectRotateToPlayer.transform.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * _rotationSpeed);
        }

        private void OnPlayerCaught()
        {
          _distinationSet = false;
        }

        private void RespawnTimer()
        {
          if(dead)
          {
            _currentRespawnTimer -= Time.deltaTime;
            if (_currentRespawnTimer <= 0)
            {
              RespawnEnemy();
              _currentRespawnTimer = respawnTime;
            }
          }
        }

        private void SetRagdoll(bool value)
        {
            foreach (Rigidbody rb in rigidBodies)
            {
                if (value)
                {
                    rb.isKinematic = false;
                }
                else
                {
                    rb.isKinematic = true;
                }
            }
            foreach (Collider col in colliders)
            {
                col.enabled = value;
            }
        }
    }
}
