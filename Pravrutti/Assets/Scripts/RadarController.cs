
using UnityEngine;

public class RadarController : MonoBehaviour
{
    [Header("Radar UI")]
    public GameObject radarUI;

    public void ShowRadar()
    {
        radarUI.SetActive(true);

        Debug.Log("Radar Activated!");
    }

    public void HideRadar()
    {
        radarUI.SetActive(false);

        Debug.Log("Radar Deactivated!");
    }
}
