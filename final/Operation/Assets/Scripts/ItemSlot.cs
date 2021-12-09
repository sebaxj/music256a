using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler {

    public Animator anim;

    // ChucK Global Variables
    private int HEART, LUNGS, BRAIN, STOMACH, KIDNEY_L, KIDNEY_R, INTESTINE, EPI, ONE, TWO, THREE, FOUR, EASTER1, EASTER2;

    // Save location of objects on table
    private Vector3 HEART_LOCATION, LUNGS_LOCATION, BRAIN_LOCATION, STOMACH_LOCATION, KIDNEY_L_LOCATION, KIDNEY_R_LOCATION, INTESTINE_LOCATION, EPI_LOCATION;

    // Text for Vital Monitor
    public static Text HR, SPO2, BP, TEMP;

    // Sliders for Vital Monitor
    public static Slider HR_Slider, SPO2_Slider, BP_Slider, TEMP_Slider;

    void Start() {
        GetComponent<ChuckSubInstance>().RunFile("ChuckScript.ck", true);   

        HEART = 0; LUNGS = 0; BRAIN = 0; STOMACH = 0; KIDNEY_L = 0; KIDNEY_R = 0; INTESTINE = 0; EPI = 0; ONE = 0; TWO = 0; THREE = 0; FOUR = 0; EASTER1 = 0; EASTER2 = 0;

        HEART_LOCATION = new Vector3(-128, 201, 0);
        LUNGS_LOCATION = new Vector3(-128, 120, 0);
        BRAIN_LOCATION = new Vector3(-285, 83, 0);
        STOMACH_LOCATION = new Vector3(-179, 164, 0);
        KIDNEY_L_LOCATION = new Vector3(-63, 176, 0);
        KIDNEY_R_LOCATION = new Vector3(-17, 232, 0);
        INTESTINE_LOCATION = new Vector3(-227, 140, 0);
        EPI_LOCATION = new Vector3(63, -288, 0);

        // Text
        HR = GameObject.Find("HR").GetComponent<Text>();
        HR.text = "HR: --";

        SPO2 = GameObject.Find("SPO2").GetComponent<Text>();
        SPO2.text = "SPO2: --";

        BP = GameObject.Find("BP").GetComponent<Text>();
        BP.text = "BP: --";

        TEMP = GameObject.Find("TEMP").GetComponent<Text>();
        TEMP.text = "TEMP: --";

        // Slider
        HR_Slider = GameObject.Find("HR_Slider").GetComponent<Slider>();

        SPO2_Slider = GameObject.Find("SPO2_Slider").GetComponent<Slider>();

        BP_Slider = GameObject.Find("BP_Slider").GetComponent<Slider>();

        TEMP_Slider = GameObject.Find("TEMP_Slider").GetComponent<Slider>();
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

                        anim.SetFloat("Trigger", 2);
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(231, 201, 0);

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
                        STOMACH = 1;

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
                        KIDNEY_L = 1;

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
                        KIDNEY_R = 1;

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
                        INTESTINE = 1;

                        anim.SetFloat("Trigger", 1);
                        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(245, 184, 0);

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

                case "Epi_Slot":

                    // if the organ matches the right slot, its position will be anchored
                    if(eventData.pointerDrag.name == "EPI") {
                        // let Chunity know that lungs have been added
                        Debug.Log("EPI");
                        EPI = 1;

                        HR_Slider.value += 180;
                        SPO2_Slider.value += 10;
                        BP_Slider.value += 180;

                        // send edit to ChucK
                        GetComponent<ChuckSubInstance>().SetInt("EPI", EPI);
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
                        } else if(eventData.pointerDrag.name == "EPI") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = EPI_LOCATION;
                        }
                    }
                    
                    break;
                
                case "Easter_Egg_1":
                    // if the organ matches the right slot, its position will be anchored
                    if(eventData.pointerDrag.name == "BRAIN") {
                        // let Chunity know that lungs have been added
                        Debug.Log("EASTER1");
                        EASTER1 = 1;


                        // send edit to ChucK
                        GetComponent<ChuckSubInstance>().SetInt("EASTER_EGG1", EASTER1);
                        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
                    } else if(eventData.pointerDrag.name == "HEART") {
                        // let Chunity know that lungs have been added
                        Debug.Log("EASTER2");
                        EASTER2 = 1;


                        // send edit to ChucK
                        GetComponent<ChuckSubInstance>().SetInt("EASTER_EGG2", EASTER2);
                        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
                    } else {
                        // else,
                        // move organ back to table
                        if(eventData.pointerDrag.name == "INTESTINE") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = INTESTINE_LOCATION;
                        } else if(eventData.pointerDrag.name == "LUNGS") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = LUNGS_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_L") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_L_LOCATION;
                        } else if(eventData.pointerDrag.name == "KIDNEY_R") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_R_LOCATION;
                        } else if(eventData.pointerDrag.name == "STOMACH") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = STOMACH_LOCATION;
                        } else if(eventData.pointerDrag.name == "EPI") {
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = EPI_LOCATION;
                        }
                    }
                    
                    break;

                
                default:
                    // let Chunity know that eventData.pointerDrag.name should be REMOVED from music (it is in trash)
                    Debug.Log(eventData.pointerDrag.name);

                    switch(eventData.pointerDrag.name) {
                        case "HEART":

                            HEART = 0;
                            anim.SetFloat("Trigger", 0);

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

                            STOMACH = 0;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("STOMACH", STOMACH);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = STOMACH_LOCATION;

                            break; 

                        case "KIDNEY_L":

                            KIDNEY_L = 0;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("KIDNEY_L", KIDNEY_L);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_L_LOCATION;

                            break; 

                        case "KIDNEY_R":

                            KIDNEY_R = 0;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("KIDNEY_R", KIDNEY_R);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = KIDNEY_R_LOCATION;

                            break; 

                        case "INTESTINE":

                            INTESTINE = 0;
                            anim.SetFloat("Trigger", 0);

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("INTESTINE", INTESTINE);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = INTESTINE_LOCATION;

                            break; 

                        case "EPI":

                            EPI = 0;

                            HR_Slider.value -= 60;
                            SPO2_Slider.value -= 10;
                            BP_Slider.value -= 40;

                            // send edit to ChucK
                            GetComponent<ChuckSubInstance>().SetInt("EPI", EPI);
                            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

                            // move organ back to table
                            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = EPI_LOCATION;

                            break;

                        default:
                            break;
                    }

                    break;
            }
        }
    }

    public void Update() {

        // cap vitals
        if(HR_Slider.value > 220) HR_Slider.value = 220;
        if(SPO2_Slider.value > 100) SPO2_Slider.value = 100;
        if(TEMP_Slider.value > 110) TEMP_Slider.value = 110;
        if(BP_Slider.value > 180) BP_Slider.value = 180;
        if(HR_Slider.value < 0) HR_Slider.value = 0;
        if(SPO2_Slider.value < 0) SPO2_Slider.value = 0;
        if(TEMP_Slider.value < 0) TEMP_Slider.value = 0;
        if(BP_Slider.value < 0) BP_Slider.value = 0;

        // adjust vital monitor from UI slider
        HR.text = "HR: " + HR_Slider.value;
        SPO2.text = "SPO2: " + SPO2_Slider.value;
        TEMP.text = "TEMP: " + TEMP_Slider.value;

        float BP_sliderValue = BP_Slider.value;

        if(BP_sliderValue >= 0 && BP_sliderValue < 60) {
            BP.text = "BP: " + BP_Slider.value + " / --";
        } else if(BP_sliderValue >= 60 && BP_sliderValue < 100) {
            BP.text = "BP: " + BP_Slider.value + " / 20";
        } else if(BP_sliderValue >= 100 && BP_sliderValue < 120) {
            BP.text = "BP: " + BP_Slider.value + " / 50";
        } else if(BP_sliderValue >= 120 && BP_sliderValue < 140) {
            BP.text = "BP: " + BP_Slider.value + " / 70";
        } else if(BP_sliderValue >= 140) {
             BP.text = "BP: " + BP_Slider.value + " / 100";
        }

        if((((HR_Slider.value + SPO2_Slider.value + TEMP_Slider.value + BP_Slider.value) / 610) * 100) > 1 && (((HR_Slider.value + SPO2_Slider.value + TEMP_Slider.value + BP_Slider.value) / 610) * 100) <= 25) {
            Debug.Log("25%");
            //anim.SetFloat("Trigger", 0);
            ONE = 1; TWO = 0; THREE = 0; FOUR = 0;
            GetComponent<ChuckSubInstance>().SetInt("ONE", ONE);
            GetComponent<ChuckSubInstance>().SetInt("TWO", TWO);
            GetComponent<ChuckSubInstance>().SetInt("THREE", THREE);
            GetComponent<ChuckSubInstance>().SetInt("FOUR", FOUR);
            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
            
        } else if((((HR_Slider.value + SPO2_Slider.value + TEMP_Slider.value + BP_Slider.value) / 610) * 100) > 25 && (((HR_Slider.value + SPO2_Slider.value + TEMP_Slider.value + BP_Slider.value) / 610) * 100) <= 50) {
            Debug.Log("50%");
            //anim.SetFloat("Trigger", 3);
            ONE = 0; TWO = 1; THREE = 0; FOUR = 0;
            GetComponent<ChuckSubInstance>().SetInt("ONE", ONE);
            GetComponent<ChuckSubInstance>().SetInt("TWO", TWO);
            GetComponent<ChuckSubInstance>().SetInt("THREE", THREE);
            GetComponent<ChuckSubInstance>().SetInt("FOUR", FOUR);
            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

        } else if((((HR_Slider.value + SPO2_Slider.value + TEMP_Slider.value + BP_Slider.value) / 610) * 100) > 50 && (((HR_Slider.value + SPO2_Slider.value + TEMP_Slider.value + BP_Slider.value) / 610) * 100) <= 75) {
            Debug.Log("75%");
            ONE = 0; TWO = 0; THREE = 1; FOUR = 0;
            GetComponent<ChuckSubInstance>().SetInt("ONE", ONE);
            GetComponent<ChuckSubInstance>().SetInt("TWO", TWO);
            GetComponent<ChuckSubInstance>().SetInt("THREE", THREE);
            GetComponent<ChuckSubInstance>().SetInt("FOUR", FOUR);
            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

        } else if((((HR_Slider.value + SPO2_Slider.value + TEMP_Slider.value + BP_Slider.value) / 610) * 100) > 75 && (((HR_Slider.value + SPO2_Slider.value + TEMP_Slider.value + BP_Slider.value) / 610) * 100) <= 100) {
            Debug.Log("100%");
            ONE = 0; TWO = 0; THREE = 0; FOUR = 1;
            GetComponent<ChuckSubInstance>().SetInt("ONE", ONE);
            GetComponent<ChuckSubInstance>().SetInt("TWO", TWO);
            GetComponent<ChuckSubInstance>().SetInt("THREE", THREE);
            GetComponent<ChuckSubInstance>().SetInt("FOUR", FOUR);
            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 

        } else if((((HR_Slider.value + SPO2_Slider.value + TEMP_Slider.value + BP_Slider.value) / 610) * 100) < 1) {
            Debug.Log("0%");
            ONE = 0; TWO = 0; THREE = 0; FOUR = 0;
            GetComponent<ChuckSubInstance>().SetInt("ONE", ONE);
            GetComponent<ChuckSubInstance>().SetInt("TWO", TWO);
            GetComponent<ChuckSubInstance>().SetInt("THREE", THREE);
            GetComponent<ChuckSubInstance>().SetInt("FOUR", FOUR);
            GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened"); 
        }
    }
}
