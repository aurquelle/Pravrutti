using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            EnemyScript.current.AreaTriggerEnter();
        }
    }
    private void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            EnemyScript.current.AreaTriggerEnter();
        }
    }
}