using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private DestroyBuggy destroyBuggy;
    public TextMeshProUGUI Speed;
    public AudioSource deathAudio;
    public AudioSource rockHitAudio;
    public AudioSource engineBoostAudio;
    public Animator animator;
    private CinemachineVirtualCamera vCam;
    public static float carSpeed = 15;
    public float turnSpeed;
    public float maxSpeed = 70;
    private float move;
    private Rigidbody rb;

    public GameObject EndgameMenuUI;

    private Vector3 moveDir;

    private void Awake()
    {
        destroyBuggy = GameObject.Find("Dune Buggy4").GetComponent<DestroyBuggy>();
        vCam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        rb = GetComponent<Rigidbody>();
        engineBoostAudio.Play();
        InvokeRepeating("ScaleSpeed", 2, 2);
    }
    

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")).normalized;
    }

    private void FixedUpdate()
    {
        move = Input.GetAxis("Vertical");
        rb.MovePosition(transform.position + (-transform.right) * carSpeed * Time.fixedDeltaTime);
        Speed.text = carSpeed.ToString("F0");
        animator.SetFloat("Is Turning", Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Horizontal") != 0)
        {
            turnSpeed = carSpeed + 30;
            rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * turnSpeed * Time.fixedDeltaTime * 2);

        }
        if (DestroyBuggy.isDead)
        {
            // I TRIED TO MANIPULATE SPEED HERE AFTER RESTARTING THE GAME
            // YOU CAN EXPERIMENT WITH IT SOMEHOW
            StartCoroutine(EndGameMenu());
            DestroyBuggy.isDead = false;
            carSpeed = 0;
        }

    }

    IEnumerator EndGameMenu()
    {
        yield return new WaitForSeconds(5);
        EndgameMenuUI.SetActive(true);

    }

    void ScaleSpeed()
    {
        if (!DestroyBuggy.isDead)
        {
            if (carSpeed < maxSpeed)
            {
                carSpeed = carSpeed * 1.2f;

            }
        }
        else
        {
            carSpeed = 0;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")

        {
            destroyBuggy.DestroyCar();
            vCam.LookAt = null;
            vCam.Follow = null;
            engineBoostAudio.Stop();
            deathAudio.Play();

        }

        if (other.tag == "Obstacle")
        {
            carSpeed = 15f;
            rockHitAudio.Play();
        }
    }
}
