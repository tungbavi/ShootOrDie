using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour,IDamgable
{
    private CharacterController controller;
    private Vector3 moveDirection;
    public float speed = 15f;
    public float jumpForce = 15f;
    private float gravity = 20.0f;
    public float health = 100f;
    private AudioManager audioManager;
    public float damaged = 5f;
    public LayerMask collisionLayer;
    public float sphereRadius = 1.5f;
    private int kills = 0;
    private Enermy enermy;
    public Slider slider;
    public Text healthText;
    public Text kill;
    public Text deathtext;
    public Button btnmenu;
    public int Kills { get => kills; set => kills = value; }
    // Start is called before the first frame update
    void Awake()
    {
        
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        deathtext.enabled = false;
        btnmenu.gameObject.SetActive(false);
       
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health + "/100";
        kill.text = "Kill: " + kills;
        slider.value = health;
        if (controller.isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            if (horizontalInput != 0 || verticalInput != 0)
            {
                if (!audioManager.IsPlaying("run"))
                {
                    audioManager.Play("run");
                }
            }
            else if(health<=0)
            {
                audioManager.Stop("run");
            }
            else
            {
                audioManager.Stop("run");
            }
            moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
                audioManager.Play("jump");
                audioManager.Stop("run");
            }
        }
        //ap dung trong luc
        moveDirection.y -= gravity * Time.deltaTime;
        //di chuyen
        controller.Move(moveDirection * Time.deltaTime);
        checkVar();

    }
    
    public void checkVar()
    {
        int numDirections = 8;

        // kiem tra 8 huong
        for (int i = 0; i < numDirections; i++)
        {
            // huong kiem tra
            float angle = (360.0f / numDirections) * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            // SphereCast
            if (Physics.SphereCast(transform.position, sphereRadius, direction, out RaycastHit hits, 1.2f, collisionLayer))
            {
                IDamgable enermy = hits.collider.GetComponent<IDamgable>();
                if (enermy != null)
                {
                    enermy.TakeDamage(10f);
                    TakeDamage(damaged);
                    Debug.Log("varr");
                 
                }
            }
        }
        
    }
    public void TakeDamage(float damage)
    {
        health -= damaged;
        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            audioManager.TurnOffAllSounds();
            Time.timeScale = 0;
            kill.enabled = false;
            deathtext.enabled = true;
            deathtext.text = "you died with " + kills + "kills";
            btnmenu.gameObject.SetActive(true);

        }
    }
    public void Menuu()
    {
        SceneManager.LoadScene("Menu");
    }
}
