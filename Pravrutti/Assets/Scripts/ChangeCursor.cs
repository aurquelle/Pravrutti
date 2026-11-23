using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static Pravrutti.CustomCursor.CursorControllerComplex;

namespace Pravrutti.CustomCursor
{
    public class ChangeCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ModeofCursor modeOfCursor;
        public void OnPointerEnter(PointerEventData eventData)
        {
            CursorControllerComplex.Instance.SetToMode(modeOfCursor);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CursorControllerComplex.Instance.SetToMode(ModeofCursor.Default);
        }
    }
}