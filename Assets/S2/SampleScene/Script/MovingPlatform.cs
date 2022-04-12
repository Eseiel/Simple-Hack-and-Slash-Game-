using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    bool move;

    // Start is called before the first frame update
    void Start()
    {
        move = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            if (transform.position.y > 3.5)
            {
                move = false;
            }
            if (transform.position.y < 0)
            {
                move = true;
            }

            if (move)
            {
                transform.Translate(0, 0.01f, 0);
            }
            else
            {
                transform.Translate(0, -0.01f, 0);
            }
        }
    }
}
