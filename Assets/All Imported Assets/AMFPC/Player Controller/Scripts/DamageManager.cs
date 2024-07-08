using All_Imported_Assets.AMFPC.Scripts;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Player_Controller.Scripts
{
    public class DamageManager : MonoBehaviour
    {
        private PlayerController _playerController;
        private HealthManager healthManager;
        [Range(0, 100)] public int fallDamage, fallDamageHeightThreshold;
        private void Awake()
        {
            healthManager = GetComponent<HealthManager>();
            _playerController = GetComponent<PlayerController>();
        }
        void Start()
        {
            _playerController.collisions.onFall.AddListener(FallDamagePlayer);
        }
        public void FallDamagePlayer(float _fallHeight)
        {
            if (_fallHeight >= fallDamageHeightThreshold)
            {
                healthManager.Damage(fallDamage);
            }
        }

        public void TakeDamage(int damage) => healthManager.Damage(damage);
    }
}
