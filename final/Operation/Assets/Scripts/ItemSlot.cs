using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {
        //Debug.Log("OnDrop");
        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            //Debug.Log(eventData.pointerDrag.name);
            switch(this.name) {
                case "Brain_Slot":
                    // let Chunity know that brain has be added
                    Debug.Log("Brain");
                    break;
                
                case "Heart_Slot":
                    // let Chunity knonw that heart has been added
                    Debug.Log("Heart");
                    break;
                
                case "Lungs_Slot":
                    // let Chunity know that lungs have been added
                    Debug.Log("Lungs");
                    break;
                
                default:
                    // let Chunity know that eventData.pointerDrag.name should be REMOVED from music (it is in trash)
                    Debug.Log(eventData.pointerDrag.name);
                    break;
            }
        }
    }

}
