using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private DucklingManager ducklingManager;
    public float MovementSpeed;
    public float RotationSpeed;
    [HideInInspector] public bool IsMoving = true;
    public GameObject QuackGraphic;
    private AudioSource quack;
    private bool IsQuacking;
    private new Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        ducklingManager = GetComponent<DucklingManager>();

        IsQuacking = false;

        if (QuackGraphic)
        {
            QuackGraphic.SetActive(IsQuacking);
        }

        rigidbody = GetComponent<Rigidbody2D>();
        quack = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
      
    }

    // Update is called once per frame
    void Update()
    {



        if(Input.GetButtonDown("Jump") && IsQuacking == false)
        {
            ducklingManager.SetQuack(true);
            quack.Play();
            IsQuacking = true;
        }

        if(IsQuacking)
        {
            if(quack.isPlaying == false)
            {
                IsQuacking = false;
                ducklingManager.SetQuack(false);
            }

            if(QuackGraphic)
            {
                QuackGraphic.SetActive(IsQuacking);
            }            
        }

        if (IsMoving)
        {
            float value = Input.GetAxis("Horizontal");
            if (value != 0)
            {
                rigidbody.transform.Rotate(new Vector3(0.0f, 0.0f, -value * RotationSpeed * Time.deltaTime));
            }

            rigidbody.transform.Translate(Vector3.up * MovementSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
