using UnityEngine;
public class TuyulController : MonoBehaviour
{
    public GameObject Tuyul;
    private void Start()
    {
        EnemyScript.current.onAreaTriggerEnter += OnEnemySpawn;
    }

    private void OnEnemySpawn()
    {
        Tuyul.SetActive(true);
    }   
}
