using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJumpScript : MonoBehaviour
{
    public static PlayerJumpScript instance;

    private Rigidbody2D myBody;
    private Animator anim;

    [SerializeField]
    private float forceX, forceY;
    private float thresholdX = 7f;
    private float thresholdY = 14f;
    private const float MAX_FORCEX = 6.5f;
    private const float MAX_FORCEY = 13.5f;

    private bool setPower, didJump;

    private Slider powerBar;
    private float powerBarThreshold = 10f;
    private float powerBarValue = 0f;

    void Awake()
    {
        MakeInstance();
        Initialize();
    }

    void Update(){
        SetPower();
    }

    void Initialize(){
        powerBar = GameObject.Find("Power Bar").GetComponent<Slider>();
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        powerBar.minValue = 0f;
        powerBar.maxValue = 10f;
        powerBar.value = powerBarValue;
    }

    void MakeInstance(){
        if(instance == null){
            instance = this;
        }
    }

    void SetPower(){
        if(setPower){
            forceX += thresholdX * Time.deltaTime;
            forceY += thresholdY * Time.deltaTime;

            if(forceX > MAX_FORCEX){
                forceX = MAX_FORCEX;
            }
            if(forceY > MAX_FORCEY){
                forceY = MAX_FORCEY;
            }

            powerBarValue += powerBarThreshold * Time.deltaTime;
            powerBar.value = powerBarValue;
        }
    }

    public void SetPower(bool setPower){
        this.setPower = setPower;

        if(!setPower){
            Jump();
        }
    }
    void Jump(){
        myBody.velocity = new Vector2(forceX, forceY);
        forceX = forceY = 0f;
        didJump = true;

        anim.SetBool("Jump", didJump);

        powerBarValue = 0f;
        powerBar.value = powerBarValue;
    }

    void OnTriggerEnter2D(Collider2D target){

        if(didJump){
            didJump = false;

            anim.SetBool("Jump", didJump);

            if(target.tag == "Platform"){
                if(GameManager.instance != null){
                    GameManager.instance.CreateNewPlatformLerp(target.transform.position.x);
                }

                if(ScoreManager.instance != null){
                    ScoreManager.instance.IncrementScore();
                }
            }
        }

        if(target.tag == "Dead"){
            if(GameOverManager.instance != null){
                GameOverManager.instance.GameOverShowPanel();
            }
            Destroy(gameObject);
        }
    }
}
