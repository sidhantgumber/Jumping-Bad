using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpSpeed = 100f;
    private bool isDead;
    private State state;
    private bool hasGameStarted;
    public Animator anim;
    public Text startText;
    
    // parameters for special ability 1
    public float ability1CoolDownTime = 10f;
    private float ability1NextUseTime = 0f;
    public float ability1Duration = 5f;
    public Image ability1Image;

    // parameters for special ability 2
    public float ability2CoolDownTime = 10f;
    private float ability2NextUseTime = 0f;
    public float ability2Duration = 5f;
    public Image ability2Image;
    private bool isInvincible;

    private bool resetSpecialAbility1;
    private bool resetSpecialAbility2;
    private SpriteRenderer birdRenderer;
    

   

    public Animator animator;
    public enum State
    {
        WaitingToStart,
        Playing,
        Dead


    }


    private static Bird instance;

    public static Bird GetInstance()
    {
        return instance;
    }

    
    private void Awake()
    {
     
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
        birdRenderer = GetComponent<SpriteRenderer>();
     
     
    }

    private void Update()
    {

        switch (state)
        {
            case State.WaitingToStart:
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
                    {
                        rb.bodyType = RigidbodyType2D.Dynamic;
                        state = State.Playing;
                        Jump();
                        hasGameStarted = true;
                        if (isDead == false)
                        {
                            AudioManager.GetInstance().PlayJumpSound();
                        }

                    }
                }
                break;

            case State.Playing:
                {
                    startText.enabled = false;
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
                    {
                        Jump();
                        if (isDead == false)
                        {
                            AudioManager.GetInstance().PlayJumpSound();
                        }
                    }
                }
                break;

            case State.Dead: { hasGameStarted = false; }
                break;

        }
        SpecialAbilityBecomeSmall();
        if (resetSpecialAbility1)
        { 
            if (isDead == false)
            
                {
                    ability1Image.fillAmount += 1 / ability1CoolDownTime * Time.deltaTime;
                }
            }
        Invincibility();
        if (resetSpecialAbility2)
        {
            if (isDead == false)
            {
                ability2Image.fillAmount += 1 / ability2CoolDownTime * Time.deltaTime;

            }

        }
      


       
    }


    private void SpecialAbilityBecomeSmall()
    {
        if (Time.time > ability1NextUseTime)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                // Debug.Log("I key is pressed");

                
                    ability1Image.fillAmount = 0;
                    StartCoroutine(SpecialAbility1());
                    ability1NextUseTime = Time.time + ability1CoolDownTime;
                    Debug.Log(ability1NextUseTime);
                
                
                
            }
         


        }


    }

    private void Invincibility()
    {
        if (Time.time > ability2NextUseTime)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                // Debug.Log("I key is pressed");


                ability2Image.fillAmount = 0;
                StartCoroutine(SpecialAbility2());
                ability2NextUseTime = Time.time + ability2CoolDownTime;
                Debug.Log(ability2NextUseTime);



            }



        }

    }

    private IEnumerator SpecialAbility1()
    {
        resetSpecialAbility1 = true;
        
        gameObject.transform.localScale *= 0.5f;
        yield return new WaitForSeconds(ability1Duration);
        gameObject.transform.localScale *= 2f;


    }

    private IEnumerator SpecialAbility2()
    {
        resetSpecialAbility2 = true;
         birdRenderer.color = new Color(1, 1, 1, 0.5f);
        isInvincible = true;
        // birdCollider.enabled = false;
         yield return new WaitForSeconds(ability2Duration);
         birdRenderer.color = new Color(1, 1, 1, 1);
         isInvincible = false;
       //  birdCollider.enabled = true;
    }



    public bool HasGameStarted() {
        return hasGameStarted;
    }
    private void Jump()
    {

        rb.velocity = Vector2.up * jumpSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isInvincible == false)
        {
            isDead = true;
            hasGameStarted = false;
            Debug.Log("Dead");
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetBool("isDead", true);
            AudioManager.GetInstance().PlayDeathSound();
            StartCoroutine(DelayInDeathSound());
            // StartCoroutine(ReloadGame());
            GameOverWindow.GetInstance().ShowPopUpOnBirdDeath();
            Score.SetNewHighScore(LevelManager.GetInstance().GetCurrentScore());
        }

        if (isInvincible == true)
        {
            if (collision.gameObject.tag == "Ground")
            {
                isDead = true;
                hasGameStarted = false;
                Debug.Log("Dead");
                rb.bodyType = RigidbodyType2D.Static;
                anim.SetBool("isDead", true);
                AudioManager.GetInstance().PlayDeathSound();
                StartCoroutine(DelayInDeathSound());
                // StartCoroutine(ReloadGame());
                GameOverWindow.GetInstance().ShowPopUpOnBirdDeath();
                Score.SetNewHighScore(LevelManager.GetInstance().GetCurrentScore());

            }
        }


       
    }

    private IEnumerator DelayInDeathSound()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.GetInstance().PlayGameOverSound();

    }
    private IEnumerator ReloadGame()
    {
        if (isDead == true)
        {
            yield return new WaitForSeconds(1f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }


    }

    public bool isBirdDead()
    {
        return isDead;
    }

}
