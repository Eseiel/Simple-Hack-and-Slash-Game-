using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator enemyAnim;
    [SerializeField] CharacterController enemyCont;
    [SerializeField] NavMeshAgent enemyAgent;
    [SerializeField] GameObject player, BossMeter;
    [SerializeField] Transform enemyHP;

    public bool dead;
    public int hp;
    float bar;

    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        if (gameObject.name == "Boss")
        {
            hp = 500;
        }
        else
        {
            hp = 100;
            BossMeter = null;
        }
        bar = 1;
    }

    // Update is called once per frame
    void Update()
    {
        bar = (float)hp / 100;
        enemyHP.localScale = new Vector3(bar, 0.15599f, 0.01271134f);
        if (hp <= 0)
        {
            enemyHP.localScale = new Vector3(0, 0, 0);
            dead = true;
        }
        if (!dead && gameObject.name != "Boss")
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 10)
            {
                enemyAnim.SetBool("Chase", true);
                enemyCont.Move(transform.TransformDirection(new Vector3(0, 0, 0.01f)));
                enemyAgent.SetDestination(player.transform.position);
                transform.LookAt(player.transform);
                if (Vector3.Distance(transform.position, player.transform.position) < 2)
                {
                    enemyAnim.SetTrigger("Attack");
                }
            }
            else
            {
                enemyAnim.SetBool("Chase", false);
            }
        }
        else if (!dead && gameObject.name == "Boss")
        {
            BossMeter.GetComponent<Slider>().value = hp;
            if (Vector3.Distance(transform.position, player.transform.position) < 20)
            {
                BossMeter.SetActive(true);
                enemyAnim.SetBool("Chase", true);
                enemyCont.Move(transform.TransformDirection(new Vector3(0, 0, 0.01f)));
                enemyAgent.SetDestination(player.transform.position);
                transform.LookAt(player.transform);
                if (Vector3.Distance(transform.position, player.transform.position) < 5)
                {
                    enemyAnim.SetTrigger("Attack");
                }
            }
            else
            {
                enemyAnim.SetBool("Chase", false);
                BossMeter.SetActive(false);
            }
        }
        else
        {
            enemyAnim.SetBool("Dead", true);
            Destroy(this.gameObject, 3f);
        }
        

    }

}
