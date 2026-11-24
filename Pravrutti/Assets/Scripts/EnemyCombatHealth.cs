
using UnityEngine;

public class EnemyCombatHealth : MonoBehaviour
{
    [Header("Enemy Health")]
    public int maxHealth = 100;
    public int currentHealth = 100;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Ghost Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Ghost defeated!");

        // Add ghost death animation here.
        // Can also trigger reward system here.
    }
}

