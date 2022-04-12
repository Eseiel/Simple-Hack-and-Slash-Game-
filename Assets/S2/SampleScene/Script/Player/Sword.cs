using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] Animator playerAnim;

    private void Update()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_TwoHanded") || 
            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_OneHanded"))
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" ||
            other.gameObject.tag == "Enemy 2" ||
            other.gameObject.tag == "Enemy 3")
        {
            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_TwoHanded"))
            {
                other.GetComponent<Enemy>().hp -= 30;

                playerAnim.SetBool("Attacking", true);
            }
            else if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_OneHanded"))
            {
                other.GetComponent<Enemy>().hp -= 15;

                playerAnim.SetBool("Attacking", true);
            }
            else
            {
                playerAnim.SetBool("Attacking", false);
            }

            other.GetComponent<Animator>().SetTrigger("Hit");
        }
    }
}
