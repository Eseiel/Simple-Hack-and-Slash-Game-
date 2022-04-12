using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCam : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - 5);
    }
}
