using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public Text phaseDisplayText;
    Touch playerTouch;
    float timetouchEnded;
    float dusplayTime = 0.5f;

    Vector2 beginPos, endPos;

    public void SwipeStart()
    {
        if (Input.touchCount > 0)
        {
            playerTouch = Input.GetTouch(0);

            if(playerTouch.phase == TouchPhase.Began)
            {
                beginPos = playerTouch.position;
            }
        } 
    }
}
