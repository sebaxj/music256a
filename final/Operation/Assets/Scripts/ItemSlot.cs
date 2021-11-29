using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {

    // ChucK Global Variables
    private int HEART, LUNGS, BRAIN, STOMACH, KIDNEY_L, KIDNEY_R, INTESTINE;

    // Save location of objects on table
    private Vector3 HEART_LOCATION, LUNGS_LOCATION, BRAIN_LOCATION, STOMACH_LOCATION, KIDNEY_L_LOCATION, KIDNEY_R_LOCATION, INTESTINE_LOCATION;

    void Start() {
        GetComponent<ChuckSubInstance>().RunFile("ChuckScript.ck", true);   

        HEART = 0; LUNGS = 0; BRAIN = 0; STOMACH = 0; KIDNEY_L = 0; KIDNEY_R = 0; INTESTINE = 0;

        HEART_LOCATION = new Vector3(-128, 201, 0);
        LUNGS_LOCATION = new Vector3(-128, 120, 0);
        BRAIN_LOCATION = new Vector3(-247, 74, 0);
        STOMACH_LOCATION = new Vector3(-179, 164, 0);
        KIDNEY_L_LOCATION = new Vector3(-63, 176, 0);
        KIDNEY_R_LOCATION = new Vector3(-17, 232, 0);
        INTESTINE_LOCATION = new Vector3(-227, 140, 0);
    }

    public void OnDrop(PointerEventData eventData) {
        //Debug.Log("OnDrop");
        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            //Debug.Log(eventData.pointerDrag.name);
            switch(this.name) {
                case "Brain_Slot":

                    // if the organ matches the right slot, its position will be anchored
                    if(eventData.pointerDrag.name == "BRAIN") {
                        // let Chunity know that brain has be added
                        Debug.Log(eventData.pointerDrag.name);
                        BRAIN = 1;

                        // send edit to ChucK
                        GetComponent<ChuckSubInstance>().SetInt("BRAIN", BRAIN);
                        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
                    } else {
                        // else,
                        // move organ back to table
                        if(eventData.pointerDrag.name == "STOMACH") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = STOMACH_LOCATION;
                        } else if(eventData.pointerDrag.name == "HEART") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = HEART_LOCATION;
                        } else if(eventData.pointerDrag.name == "LUNGS") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = LUNGS_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_L") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_L_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_R") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_R_LOCATION;
                        } else if(eventData.pointerDrag.name == "INTESTINE") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = INTESTINE_LOCATION;
                        }
                    }

                    break;
                
                case "Heart_Slot":

                    // if the organ matches the right slot, its position will be anchored
                    if(eventData.pointerDrag.name == "HEART") {
                        // let Chunity know that heart has been added
                        Debug.Log("Heart");
                        HEART = 1;

                        // send edit to ChucK
                        GetComponent<ChuckSubInstance>().SetInt("HEART", HEART);
                        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
                    } else {
                        // else,
                        // move organ back to table
                        if(eventData.pointerDrag.name == "BRAIN") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = BRAIN_LOCATION;
                        } else if(eventData.pointerDrag.name == "STOMACH") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = STOMACH_LOCATION;
                        } else if(eventData.pointerDrag.name == "LUNGS") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = LUNGS_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_L") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_L_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_R") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_R_LOCATION;
                        } else if(eventData.pointerDrag.name == "INTESTINE") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = INTESTINE_LOCATION;
                        }
                    }

                    break;
                
                case "Lungs_Slot":

                    // if the organ matches the right slot, its position will be anchored
                    if(eventData.pointerDrag.name == "LUNGS") {
                        // let Chunity know that lungs have been added
                        Debug.Log("Lungs");
                        LUNGS = 1;

                        // send edit to ChucK
                        GetComponent<ChuckSubInstance>().SetInt("LUNGS", LUNGS);
                        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
                    } else {
                        // else,
                        // move organ back to table
                        if(eventData.pointerDrag.name == "BRAIN") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = BRAIN_LOCATION;
                        } else if(eventData.pointerDrag.name == "HEART") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = HEART_LOCATION;
                        } else if(eventData.pointerDrag.name == "STOMACH") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = STOMACH_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_L") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_L_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_R") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_R_LOCATION;
                        } else if(eventData.pointerDrag.name == "INTESTINE") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = INTESTINE_LOCATION;
                        }
                    }

                    break;
                
                case "Stomach_Slot":

                    // if the organ matches the right slot, its position will be anchored
                    if(eventData.pointerDrag.name == "STOMACH") {
                        // let Chunity know that lungs have been added
                        Debug.Log("Stomach");
                        LUNGS = 1;

                        // send edit to ChucK
                        GetComponent<ChuckSubInstance>().SetInt("STOMACH", STOMACH);
                        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
                    } else {
                        // else,
                        // move organ back to table
                        if(eventData.pointerDrag.name == "BRAIN") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = BRAIN_LOCATION;
                        } else if(eventData.pointerDrag.name == "HEART") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = HEART_LOCATION;
                        } else if(eventData.pointerDrag.name == "LUNGS") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = LUNGS_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_L") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_L_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_R") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_R_LOCATION;
                        } else if(eventData.pointerDrag.name == "INTESTINE") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = INTESTINE_LOCATION;
                        }
                    }

                    break;

                case "Kidney_L_Slot":

                    // if the organ matches the right slot, its position will be anchored
                    if(eventData.pointerDrag.name == "KIDNEY_L") {
                        // let Chunity know that lungs have been added
                        Debug.Log("Kidney L");
                        LUNGS = 1;

                        // send edit to ChucK
                        GetComponent<ChuckSubInstance>().SetInt("KIDNEY_L", KIDNEY_L);
                        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
                    } else {
                        // else,
                        // move organ back to table
                        if(eventData.pointerDrag.name == "BRAIN") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = BRAIN_LOCATION;
                        } else if(eventData.pointerDrag.name == "HEART") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = HEART_LOCATION;
                        } else if(eventData.pointerDrag.name == "LUNGS") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = LUNGS_LOCATION;
                        } else if(eventData.pointerDrag.name == "STOMACH") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = STOMACH_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_R") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_R_LOCATION;
                        } else if(eventData.pointerDrag.name == "INTESTINE") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = INTESTINE_LOCATION;
                        }
                    }

                    break;

                case "Kidney_R_Slot":

                    // if the organ matches the right slot, its position will be anchored
                    if(eventData.pointerDrag.name == "KIDNEY_R") {
                        // let Chunity know that lungs have been added
                        Debug.Log("Kidney R");
                        LUNGS = 1;

                        // send edit to ChucK
                        GetComponent<ChuckSubInstance>().SetInt("KIDNEY_R", KIDNEY_R);
                        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
                    } else {
                        // else,
                        // move organ back to table
                        if(eventData.pointerDrag.name == "BRAIN") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = BRAIN_LOCATION;
                        } else if(eventData.pointerDrag.name == "HEART") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = HEART_LOCATION;
                        } else if(eventData.pointerDrag.name == "LUNGS") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = LUNGS_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_L") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_L_LOCATION;
                        } else if(eventData.pointerDrag.name == "STOMACH") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = STOMACH_LOCATION;
                        } else if(eventData.pointerDrag.name == "INTESTINE") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = INTESTINE_LOCATION;
                        }
                    }

                    break;

                case "Intestine_Slot":

                    // if the organ matches the right slot, its position will be anchored
                    if(eventData.pointerDrag.name == "INTESTINE") {
                        // let Chunity know that lungs have been added
                        Debug.Log("Intestine");
                        LUNGS = 1;

                        // send edit to ChucK
                        GetComponent<ChuckSubInstance>().SetInt("INTESTINE", INTESTINE);
                        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
                    } else {
                        // else,
                        // move organ back to table
                        if(eventData.pointerDrag.name == "BRAIN") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = BRAIN_LOCATION;
                        } else if(eventData.pointerDrag.name == "HEART") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = HEART_LOCATION;
                        } else if(eventData.pointerDrag.name == "LUNGS") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = LUNGS_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_L") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_L_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_R") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_R_LOCATION;
                        } else if(eventData.pointerDrag.name == "STOMACH") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = STOMACH_LOCATION;
                        }
                    }
                    
                    break;
                
                default:
                    // let Chunity know that eventData.pointerDrag.name should be REMOVED from music (it is in trash)
                    Debug.Log(eventData.pointerDrag.name);

                    switch(eventData.pointerDrag.name) {
                        case "HEART":

                            HEART = 0;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("HEART", HEART);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
                            
                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = HEART_LOCATION;

                            break;

                        case "LUNGS":

                            LUNGS = 0;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("LUNGS", LUNGS);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = LUNGS_LOCATION;

                            break;

                        case "BRAIN":

                            BRAIN = 0;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("BRAIN", BRAIN);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = BRAIN_LOCATION;

                            break; 
                        
                        case "STOMACH":

                            BRAIN = 0;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("STOMACH", STOMACH);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = STOMACH_LOCATION;

                            break; 

                        case "KIDNEY_L":

                            BRAIN = 0;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("KIDNEY_L", KIDNEY_L);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_L_LOCATION;

                            break; 

                        case "KIDNEY_R":

                            BRAIN = 0;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("KIDNEY_R", KIDNEY_R);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_R_LOCATION;

                            break; 

                        case "INTESTINE":

                            BRAIN = 0;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("INTESTINE", INTESTINE);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = INTESTINE_LOCATION;

                            break; 

                        default:
                            break;
                    }

                    break;
            }
        }
    }

}
