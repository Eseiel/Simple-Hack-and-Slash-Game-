using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterController playerCont;
    [SerializeField] Animator playerAnim;
    [SerializeField] GameObject WeaponHolstered, WeaponUnsheathed;
    [SerializeField] float walkSpeed;
    [SerializeField] Camera deadCam;

    [SerializeField] Button Up, Down, Left, Right, Atk1, Atk2, Roll, LC, LR;

    bool checkJump, dead;
    Vector3 Movement;

    [SerializeField] GameObject HPM, DeadUi;
    float jump, movX, movZ, rotation;
    public int hp;

    // Start is called before the first frame update
    void Start()
    {

        Movement = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
        hp = 500;
        checkJump = false;
        dead = false; 
        deadCam.enabled = false;

        WeaponHolstered.SetActive(true);
    }

    public void UpdateSystem()
    {
        HPM.GetComponent<Slider>().value = hp;
        if (GameObject.Find("Boss") == null)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void LeftCamPress()
    {
        rotation = -1f;
    }
    public void RightCamPress()
    {
        rotation = 1f;
    }

    public void CamRelease()
    {
        rotation = 0f;
    }

    public void LightAttackPress()
    {
        playerAnim.SetTrigger("Attack");
    }
    public void HeavyAttackPress()
    {
        playerAnim.SetTrigger("Attack 2");
    }
    public void DodgePress()
    {
        playerAnim.SetTrigger("Dodge");
    }
    public void JumpPress()
    {
        if (!checkJump)
        {
            playerAnim.SetBool("Jump", true);
            jump = 2.5f;
            checkJump = true;
        }
    }

    public void ForwardPress()
    {
        movZ = 1f;
    }
    public void BackwardPress()
    {
        movZ = -1f;
    }
    public void FBRelease()
    {
        movZ = 0f;
    }
    public void RightPress()
    {
        movX = 1f;
    }
    public void LeftPress()
    {
        movX = -1f;
    }
    public void LRRelease()
    {
        movX = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        UpdateSystem();
        if (!dead)
        {

            //float rotation = Input.GetAxis("Mouse X");

            playerAnim.SetFloat("Strafe", movX);
            playerAnim.SetFloat("Run", movZ);

            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RunForward") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("StrafeLeft") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("StrafeRight") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RunBackward") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("JumpWhileRunning"))
            {
                playerCont.Move(transform.TransformDirection(new Vector3(movX, jump, movZ)) * walkSpeed);
            }

            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_TwoHanded"))
            {
                transform.Rotate(0, rotation, 0);
            }
            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_TwoHanded") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_OneHanded") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("IdleCombat")|| 
                (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("GetHit") && playerAnim.GetBool("Attacking")))
            {
                WeaponHolstered.SetActive(false);
                WeaponUnsheathed.SetActive(true);
            }
            else
            {
                WeaponHolstered.SetActive(true);
                WeaponUnsheathed.SetActive(false);
            }

            if (!playerCont.isGrounded)
            {
                jump -= 0.05f;
            }
            else
            {
                jump = 0;
                checkJump = false;
                playerAnim.SetBool("Jump", false);
            }

            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RollForward") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RollLeft") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RollBackward") ||
                playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RollRight"))
            {
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                playerAnim.SetBool("Dodging", true);
                if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RollForward"))
                {
                    Movement = new Vector3(0, 0, 0.01f);
                    Movement = transform.TransformDirection(Movement);
                    playerCont.Move(Movement);
                }
                if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RollBackward"))
                {
                    Movement = new Vector3(0, 0, -0.01f);
                    Movement = transform.TransformDirection(Movement);
                    playerCont.Move(Movement);
                }
                if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RollLeft"))
                {
                    Movement = new Vector3(-0.01f, 0, 0);
                    Movement = transform.TransformDirection(Movement);
                    playerCont.Move(Movement);
                }
                if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RollRight"))
                {
                    Movement = new Vector3(0.01f, 0, 0);
                    Movement = transform.TransformDirection(Movement);
                    playerCont.Move(Movement);
                }
            }
            else
            {
                gameObject.GetComponent<CapsuleCollider>().enabled = true;
                playerAnim.SetBool("Dodging", false);
            }

            if (hp <= 0)
            {
                dead = true;
            }
        }
        else
        {
            playerAnim.SetBool("Dead", true);
            Destroy(this.gameObject, 5f);
            deadCam.enabled = true;
            DeadUi.SetActive(true);
            this.gameObject.GetComponent<Camera>().enabled = false;
        }
    }
}
