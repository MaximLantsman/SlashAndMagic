using UnityEngine;
using UnityEngine.Events;

namespace Health
{
    public class Health : MonoBehaviour
    {
        public UnityEvent OnHit;
        [SerializeField]private int maxHealth = 100;
        [SerializeField]private FloatEventChannel playerHealthChannel;
        
        int currentHealth;
        
        private const int ZeroHealth = 0;
        
        public bool IsDead => currentHealth <= 0;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        private void Start()
        {
            PublishHealthPercentage();
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log($"health: {currentHealth} bool: {IsDead}");
            
            OnHit.Invoke();
            PublishHealthPercentage();
            
        }

 

        private void PublishHealthPercentage()
        {
            if(playerHealthChannel != null)
                playerHealthChannel.Invoke(currentHealth/ (float)maxHealth);
        }
    }
}