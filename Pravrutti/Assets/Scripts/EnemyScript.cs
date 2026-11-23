using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static EnemyScript current;
    private void Awake()
        {
            current = this;
        }
    public event Action onAreaTriggerEnter;
    public void AreaTriggerEnter()
    {
        if (onAreaTriggerEnter != null)
        {
            onAreaTriggerEnter();
        }
    }
}
