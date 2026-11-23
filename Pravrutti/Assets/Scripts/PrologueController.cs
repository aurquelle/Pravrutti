using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PrologueController : MonoBehaviour
{
    public string sceneName;

    void Update()
    {
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene(2);
        }
    }
}