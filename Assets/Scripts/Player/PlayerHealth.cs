using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI HealthText;
        [SerializeField] private int MaxHealth;

        private int _currentHealth;

        private void Start()
        {
            ResetHealth();
        }

        [PunRPC]
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            UpdateText();
            
            if(_currentHealth <= 0)
                Destroy(gameObject);
        }

        public void ResetHealth()
        {
            _currentHealth = MaxHealth;
            UpdateText();
        }

        private void UpdateText()
        {
            HealthText.SetText(_currentHealth.ToString());
        }
    }
}
