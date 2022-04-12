using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [SerializeField] Animator enemyAnim;
    [SerializeField] GameObject enemy;
    Enemy enemyScript;

    private void Start()
    {
        enemyScript = enemy.GetComponent<Enemy>();
    }

    private void Update()
    {
        if (!enemyScript.dead)
        {
            if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_OneHanded"))
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<Player>().hp > 0)
            {
                if (other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_TwoHanded"))
                {
                    other.GetComponent<Player>().hp -= 20;
                }
                other.GetComponent<Player>().hp -= 10;
                other.GetComponent<Animator>().SetTrigger("Hit");
            }
        }
    }
}
