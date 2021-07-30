using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSizeController : MonoBehaviour
{
    public BoxCollider2D rightWall, leftWall, topWall, downWall;

    // Start is called before the first frame update
    void Start()
    {
        rightWall.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width + rightWall.size.x * 0.5f, 0));
        leftWall.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(-leftWall.size.x * 0.5f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
