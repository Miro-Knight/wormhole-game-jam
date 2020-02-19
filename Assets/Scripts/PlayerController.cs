using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private DestroyBuggy destroyBuggy;
    public Text Speed = null;
    public AudioSource deathAudio;
    public AudioSource rockHitAudio;
    public AudioSource engineBoostAudio;

    private CinemachineVirtualCamera vCam;
    public static float carSpeed = 15;
    public float maxSpeed = 70;

    private Rigidbody rb;

    private Vector3 moveDir;
    // Start is called before the first frame update
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
        // moveDir = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")).normalized;
        moveDir = new Vector3(0, 0, Input.GetAxis("Horizontal")).normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (-transform.right) * carSpeed * Time.fixedDeltaTime);
        Speed.text = carSpeed.ToString("F0");
        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * carSpeed * Time.fixedDeltaTime);

        }
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
            Debug.Log("enemy");
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
