using UnityEngine;

namespace Pravrutti.CustomCursor
{
    public class CursorControllerComplex : MonoBehaviour
    {
        public static CursorControllerComplex Instance { get; private set; }

        [SerializeField] private Texture2D cursorTextureDefault;
        [SerializeField] private Texture2D cursorTextureInspect;

        [SerializeField] private Vector2 clickPosition = Vector2.zero;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
        void Start()
        {
            Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
        }

        public void SetToMode(ModeofCursor modeOfCursor)
        {
            switch (modeOfCursor)
            {
                case ModeofCursor.Default:
                    Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
                    break;

            }
        }
        public enum ModeofCursor
        {
            Default,
            Inspect

        }


    }

}

