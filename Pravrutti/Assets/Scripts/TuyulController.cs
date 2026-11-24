using UnityEngine;
public class TuyulController : MonoBehaviour
{
    public GameObject Tuyul;
    private void Start()
    {
        Tuyul.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Tuyul.SetActive(true);
    }   
}
