using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxHp = 10;
    float currentHP;
    [SerializeField] float movementStiffness = 1;
    [SerializeField] float movementDamping = 1;
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float cameraFollowSpeed = 1;
    private float prevMovementStiffness;
    private float prevMovementDamping;

    private Vector3 desiredDirection;
    private Vector3 currentDirection;
    private Vector3 currentVelocity;
    private SpringDamper damper;

    private bool breathingFire = false;
    private bool canBreathFire = true;
    private float breathingStart;
    private float breathingEnd;

    private Vector3 initialCamOffset;

    private Image healthbar;
    private Image firebar;


    void Start()
    {
        
        initialCamOffset = Camera.main.transform.position - transform.position;
        prevMovementStiffness = movementStiffness;
        prevMovementDamping = movementDamping;
        damper = new SpringDamper(movementStiffness, movementDamping);
        
        currentHP = maxHp;
        healthbar = GameObject.FindGameObjectWithTag("UI").transform.Find("Healthbar").Find("Foreground").GetComponent<Image>();
        firebar = GameObject.FindGameObjectWithTag("UI").transform.Find("Firebar").Find("Foreground").GetComponent<Image>();
        UpdateHealthUI();
    }

   
    void Update()
    {
        ManageInputs();
        UpdateValues();

        if (breathingFire)
        {
            float t = 1 - (Time.time - breathingStart) / 8.0f;
            firebar.fillAmount = t;
        }
        else
        {
            float t = Mathf.Min((Time.time - breathingEnd) / 5.0f, 1.0f);
            firebar.fillAmount = t;
        }
        if (breathingFire && Time.time - breathingStart > 8.0f)
        {
            BreathFire(false);
        }
        if (!canBreathFire && Time.time - breathingEnd > 5.0f)
        {
            canBreathFire = true;
        }

        Camera.main.transform.position += desiredDirection * movementSpeed;
        Camera.main.transform.LookAt(Camera.main.transform.position + currentDirection * cameraFollowSpeed, Vector3.up);
        transform.position = Camera.main.transform.TransformPoint(-initialCamOffset);
        float mouseCenterOffsetX = 0.5f - Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        float mouseCenterOffsetY = 0.5f - Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
        Quaternion roll = Quaternion.AngleAxis(mouseCenterOffsetX * 90, transform.forward);
        Quaternion pitch = Quaternion.AngleAxis(mouseCenterOffsetY * 90, Camera.main.transform.right);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, roll * Vector3.up) * pitch;  
    }

    private void UpdateValues()
    {
        if (movementStiffness!= prevMovementStiffness)
        {
            damper.Stiffness = movementStiffness;
            prevMovementStiffness = movementStiffness;
        }
        if (movementDamping != prevMovementDamping)
        {
            damper.Damping = movementDamping;
            prevMovementDamping = movementDamping;
        }

        damper.Dampen(currentDirection, currentVelocity, desiredDirection, Time.deltaTime, out currentDirection, out currentVelocity);

    }

    private void ManageInputs()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        desiredDirection = (Camera.main.ScreenToWorldPoint(mousePos) - Camera.main.transform.position).normalized;

        if (Input.GetMouseButtonDown(0) && !breathingFire && canBreathFire)
        {
            BreathFire(true);
        }
        else if (Input.GetMouseButtonUp(0) && breathingFire)
        {
            BreathFire(false);
        }
    }

    private void BreathFire(bool state)
    {
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        AudioSource audioSource = GetComponentsInChildren<AudioSource>().Where(audioSource => audioSource.clip.name == "Fire").First();
        if (state)
        {
            particleSystem.Play();
            audioSource.Play();
            breathingFire = true;
            canBreathFire = false;
            breathingStart = Time.time;
        }
        else
        {
            particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            audioSource.Stop();
            breathingFire = false;
            breathingEnd = Time.time;
        }
    }

    public void Damage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            die();
        }
        UpdateHealthUI();
    }

    private void die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateHealthUI()
    {
        healthbar.fillAmount = currentHP / maxHp;
        firebar.fillAmount = 1;
    }

}
