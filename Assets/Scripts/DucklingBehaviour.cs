using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DucklingBehaviour : MonoBehaviour
{
    Vector3 last;
    AudioSource babyQuack;
    private new LineRenderer renderer;
    private DucklingManager manager;
    private Transform Target;
    public bool IsFollowing = false;
    private Vector3 startPosition;
    private Vector3 lastFramePosition;
    public float CircleRadius;
    private float DuckCircleRotationAngle;
    public float DuckCircleRotationSpeed;
    public float MovementSpeed;
    private bool IsMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        IsMoving = true;
        babyQuack = GetComponent<AudioSource>();
        renderer = GetComponent<LineRenderer>();
        renderer.startColor = Color.white;
        renderer.endColor = Color.red;
        renderer.startWidth = 0.0f;
        renderer.endWidth = 0.3f;
        renderer.enabled = false;
        renderer.SetPosition(0, Vector3.zero);
        renderer.SetPosition(1, Vector3.zero);

        manager = FindObjectOfType<DucklingManager>();
        Target = null;
        lastFramePosition = transform.position;
        startPosition = transform.position;
    }

    public void StartQuack()
    {
        if (!IsFollowing)
        {
            renderer.enabled = true;
            renderer.SetPosition(0, manager.gameObject.transform.position);
            renderer.SetPosition(1, transform.position);
        }
    }

    public void EndQuack()
    {
        renderer.enabled = false;
        renderer.SetPosition(0, Vector3.zero);
        renderer.SetPosition(1, Vector3.zero);
    }

    public void PlaySound()
    {
        babyQuack.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(IsFollowing)
        {
            if (IsMoving)
            {
                if (Target == null)
                {
                    Target = manager.GetTransformForDucklingToFollow(this);
                }
                else
                {
                    last = transform.position;
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    transform.position = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * MovementSpeed);
                    Debug.DrawLine(last, transform.position);
                    Debug.DrawLine(transform.position, Target.position);

                    Vector2 dir = new Vector2(last.x - transform.position.x, last.y - transform.position.y);
                    float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + 90.0f;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }
        else
        {
            lastFramePosition = transform.position;
            DuckCircleRotationAngle += Time.deltaTime * DuckCircleRotationSpeed;

            //transform.position = new Vector3(
            //startPosition.x + (CircleRadius * Mathf.Cos(DuckCircleRotationAngle)),
            //startPosition.y + (CircleRadius * Mathf.Sin(DuckCircleRotationAngle)),
            //0.0f);
            //
            //Vector3 diff = transform.position - lastFramePosition;
            //float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 270.0f;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            transform.rotation = Quaternion.Euler(0.0f, 0.0f, DuckCircleRotationAngle);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<DucklingManager>())
        {
            IsMoving = false;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DucklingManager>())
        {
            IsMoving = true;
        }
    }
}
