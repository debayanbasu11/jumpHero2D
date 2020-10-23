using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData data){
        //Debug.Log("We are Touching the Button");
        if(PlayerJumpScript.instance != null){
            PlayerJumpScript.instance.SetPower(true);
        }
    }

    public void OnPointerUp(PointerEventData data){
        //Debug.Log("We have released the Button");
        if(PlayerJumpScript.instance != null){
            PlayerJumpScript.instance.SetPower(false);
        }
    }
}
