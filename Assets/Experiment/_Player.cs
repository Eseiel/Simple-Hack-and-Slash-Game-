using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Player : MonoBehaviour
{
    [SerializeField] CharacterController playerCont;
    [SerializeField] Animator playerAnim;
    [SerializeField] GameObject WeaponHolstered, WeaponUnsheathed;
    [SerializeField] float walkSpeed;
    [SerializeField] Camera deadCam;

    bool checkJump, dead;
    Vector3 Movement;

    [SerializeField] GameObject DeadUi;
    [SerializeField] Slider HPM;
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

        DeadUi.SetActive(false);
        WeaponHolstered.SetActive(true);
    }

    protected void UpdateSystem()
    {
        HPM.GetComponent<Slider>().value = hp;
    }

    protected void LightAttack()
    {
        playerAnim.SetTrigger("Attack");
    }
    protected void HeavyAttack()
    {
        playerAnim.SetTrigger("Attack 2");
    }
    protected void Dodge()
    {
        playerAnim.SetTrigger("Dodge");
    }
    protected void Jump()
    {
        if (!checkJump)
        {
            playerAnim.SetBool("Jump", true);
            jump = 2.5f;
            checkJump = true;
        }
    }

    public void Forward()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RunForward"))
        {
            playerCont.Move(transform.TransformDirection(new Vector3(0, 0, 1 * walkSpeed)));
        }
    }
    public void Backward()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RunBackward"))
        {
            playerCont.Move(transform.TransformDirection(new Vector3(0, 0, -1 * walkSpeed)));
        }
    }
    public void StrafeLeft()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("StrafeLeft"))
        {
            playerCont.Move(transform.TransformDirection(new Vector3(-1 * 0.03f, 0, 0)) );
        }
    }
    public void StrafeRight()
    {
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("StrafeRight"))
        {
            playerCont.Move(transform.TransformDirection(new Vector3(1 * 0.03f, 0, 0)));
        }
    }

    public void FWLR()
    {
        Forward();
        Backward();

        StrafeLeft();
        StrafeRight();
    }

    protected void AllMovement()
    {
        float movX = Input.GetAxis("Horizontal");
        float movZ = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Mouse X");

        playerAnim.SetFloat("Strafe", movX);
        playerAnim.SetFloat("Run", movZ);

        FWLR();
        /*
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RunForward") ||
            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("StrafeLeft") ||
            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("StrafeRight") ||
            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RunBackward") ||
            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("JumpWhileRunning"))
        {
            playerCont.Move(transform.TransformDirection(new Vector3(movX, jump, movZ)) * walkSpeed);
        }
        */
        if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_TwoHanded"))
        {
            transform.Rotate(0, rotation, 0);
        }
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_TwoHanded") ||
            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_OneHanded") ||
            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("IdleCombat") ||
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
    }

    protected void HealthMonitor()
    {
        if (hp <= 0)
        {
            dead = true;
        }
        HPM.value = hp;
    }

    protected void OnDeath()
    {
        playerAnim.SetBool("Dead", true);
        Destroy(this.gameObject, 5f);
        deadCam.enabled = true;
        DeadUi.SetActive(true);
        this.gameObject.GetComponent<Camera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSystem();
        if (!dead)
        {
            AllMovement();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                LightAttack();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                HeavyAttack();
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Dodge();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }


            
        }
        else
        {
            OnDeath();
        }
    }
}
