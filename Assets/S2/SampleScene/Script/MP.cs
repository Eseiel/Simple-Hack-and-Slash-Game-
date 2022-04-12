using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP : MonoBehaviour
{
    bool move;
    Vector3 vmove;

    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("Detected");
        //if (collision.gameObject.tag == "Player")
        //{
        //    Debug.Log("Player");
        //    collision.transform. = gameObject.transform.position;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "MP1")
        {
            if (transform.position.x == 5)
            {
                move = true;
            }
            if (transform.position.x >= 25)
            {
                move = false;
            }
            if (move)
            {
                transform.Translate(0.01f, 0, 0);
            }
            else
            {
                transform.Translate(-0.01f, 0, 0);
            }
        }
        if (gameObject.name == "MP2")
        {
            if (transform.position.z == 25)
            {
                move = true;
            }
            if (transform.position.z >= 45)
            {
                move = false;
            }
            if (move)
            {
                transform.Translate(0, 0, 0.01f);
            }
            else
            {
                transform.Translate(0, 0, -0.01f);
            }
        }
        if (gameObject.name == "MP3")
        {
            if (transform.position.x == 25)
            {
                move = true;
            }
            if (transform.position.x <= 5)
            {
                move = false;
            }
            if (move)
            {
                transform.Translate(-0.01f, 0, 0);
            }
            else
            {
                transform.Translate(0.01f, 0, 0);
            }
        }

    }

}
