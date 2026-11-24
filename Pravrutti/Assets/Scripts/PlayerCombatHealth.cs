
using UnityEngine;

public class PlayerCombatHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player defeated!");

        // Add game-over sequence here.
    }
}

