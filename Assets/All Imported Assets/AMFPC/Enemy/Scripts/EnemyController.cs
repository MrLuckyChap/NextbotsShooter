using System;
using All_Imported_Assets.AMFPC.Camera.Scripts;
using All_Imported_Assets.AMFPC.Scripts;
using CodeBase;
using CodeBase.Infrastructure.PoolObject;
using UnityEngine;
using UnityEngine.AI;

namespace All_Imported_Assets.AMFPC.Enemy.Scripts
{
  public class EnemyController : MonoBehaviour
  { 
        public event Action<EnemyController> EnemyDied;

        public Animator animator;
        public float respawnTime;
        private float _currentRespawnTimer;
        [HideInInspector] public NavMeshAgent _agent;
        private Vector3 _destination;
        private bool generatedPoint, _distinationSet;
        private float _targetDistance;
        [HideInInspector] public bool dead;
        private EnemyStats enemyStats;
        private HealthManager healthManager;
        private Rigidbody[] rigidBodies;
        private Collider[] colliders;
        private AISpawner _aiSpawner;
        private Transform _playerTransform;
        private bool _isDevModeSpawn;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private GameObject _objectRotateToPlayer;
        [SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;
        [SerializeField] private Aggro _aggro;
        [SerializeField] private BoxCollider _collisionCollider;
        private MonoBehaviourPool<EnemyController> _enemyPool;

        private float idleTime = 0f; // Время простоя
        private float maxIdleTime = 5f; // Максимальное время простоя до смены цели
        private float movementThreshold = 0.1f; // Порог скорости, чтобы считать, что агент движется

        private Vector3 lastPosition;
        
        private void Awake()
        {
            rigidBodies = animator.gameObject.GetComponentsInChildren<Rigidbody>();
            colliders = animator.gameObject.GetComponentsInChildren<Collider>();

            _agentMoveToPlayer.PlayerCaught += OnPlayerCaught;
            _aggro.PlayerEscaped += OnPlayerCaught;
            // SetRagdoll(false);
            
            enemyStats = GetComponent<EnemyStats>();
            healthManager = GetComponent<HealthManager>();
            _agent = GetComponent<NavMeshAgent>();
            _currentRespawnTimer = respawnTime;
        }

        public void Init(AISpawner aiSpawner, Transform playerTransform)
        {
            _aiSpawner = aiSpawner;
            _playerTransform = playerTransform;
            _agentMoveToPlayer.Init(playerTransform);
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
            // Проверяем, движется ли агент
            if (IsAgentMoving())
            {
              // Если агент движется, сбрасываем idleTime
              idleTime = 0f;
            }
            else
            {
              // Если агент не движется, увеличиваем idleTime
              idleTime += Time.deltaTime;
            
              // Если агент стоит более 5 секунд, сменить цель
              if (idleTime >= maxIdleTime)
              {
                Debug.Log("Агент стоял на месте слишком долго. Меняем цель.");
                generatedPoint = false;
                idleTime = 0f; // Сбрасываем таймер
              }
            }
            RespawnTimer();
            lastPosition = transform.position;
        }
        private bool IsAgentMoving()
        {
          // Проверяем, изменяется ли позиция агента
          float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        
          // Если движение больше порога, агент движется
          return distanceMoved > movementThreshold;
        }
        public void Chase()
        {
            if(generatedPoint)
            {
                _targetDistance = Vector3.Distance(_destination, transform.position);
                if (!_distinationSet)
                {
                    _agent.destination = _destination;
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

        public void KillEnemy()
        {
          if (_isDevModeSpawn)
          {
            _enemyPool.Release(this);
          }
          else
          {
            dead = true;
            gameObject.SetActive(false);
          }

          _agent.destination = transform.position;
          // GetComponent<CapsuleCollider>().enabled = false;
          SetRagdoll(true);
          animator.enabled = false;
        }

        public void SetValuesFromUISpawn(Vector3 colliderSize, MonoBehaviourPool<EnemyController> enemyPool,
          AISpawner aiSpawner, DevModeSpawnPosition devModeSpawnPosition, Transform playerTransform)
        {
          _isDevModeSpawn = true;
          _collisionCollider.size = colliderSize;
          _enemyPool = enemyPool;
          _agent.Warp(devModeSpawnPosition.GetDevModeAgentSpawnPoint());
          Init(aiSpawner, playerTransform);
        }

        private void GenerateDestinationPoint()
        {
          if(!generatedPoint)
          {
            _destination = _aiSpawner.GetNavMeshRandomPoint();
            generatedPoint = true;
            _distinationSet = false;
          }
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

        private void RespawnEnemy()
        {
          animator.enabled = true;
          gameObject.SetActive(true);
          // SetRagdoll(false);
          dead = false;
          healthManager.RestoreHealth();
          generatedPoint = false;
          transform.position = _aiSpawner.GetNavMeshRandomPoint();
          // GetComponent<CapsuleCollider>().enabled = true;
          healthManager.RestoreHealth();
          enemyStats.UpdateEnemyHealthUI();
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
