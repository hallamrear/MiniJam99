using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DucklingManager : MonoBehaviour
{
    public GameObject Nest;
    [HideInInspector] public DucklingBehaviour[] Ducklings;
    DucklingBehaviour lastDuckling;
    private bool HasFinished = false;
    [HideInInspector] public int CurrentFollowing = 0;

    public float CollectionRadius;

    // Start is called before the first frame update
    void Start()
    {
        Ducklings = FindObjectsOfType<DucklingBehaviour>();
        lastDuckling = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetQuack(bool isQuack)
    {
        foreach(DucklingBehaviour duck in Ducklings)
        {
            if(isQuack)
            {
                duck.StartQuack();
            }
            else
            {
                duck.EndQuack();
            }
        }
    }


    public Transform GetTransformForDucklingToFollow(DucklingBehaviour duckling)
    {
        Transform toReturn = null;

        if (CurrentFollowing == 0)
        {
            toReturn = transform;
        }
        else
        {  
            CurrentFollowing++;
            toReturn = lastDuckling.transform;
        }

        lastDuckling = duckling;
        CurrentFollowing++;
        return toReturn;
    }

    private void FixedUpdate()
    {
        if (CurrentFollowing == Ducklings.Length)
        {
            HasFinished = true;
        }

        if (HasFinished == false)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, CollectionRadius, Vector2.zero);

            if (hits.Length > 0)
            {
                foreach (RaycastHit2D hit in hits)
                {

                    DucklingBehaviour behaviour = null;

                    if (hit.rigidbody)
                    {
                        behaviour = hit.rigidbody.gameObject.GetComponent<DucklingBehaviour>();

                    }
                    else if (hit.collider)
                    {
                        behaviour = hit.collider.gameObject.GetComponent<DucklingBehaviour>();
                    }

                    if (behaviour)
                    {
                        if (behaviour.IsFollowing == false)
                        {
                            Debug.DrawLine(transform.position, hit.point);
                            behaviour.IsFollowing = true;
                            behaviour.PlaySound();
                        }
                    }
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (HasFinished)
        {
            if (collision.collider.gameObject == Nest)
            {
                SceneManager.LoadScene("EndScene");
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.25f);
        Gizmos.DrawSphere(transform.position, CollectionRadius);

        if (lastDuckling != null)
        {
            Gizmos.DrawLine(transform.position, lastDuckling.transform.position);
        }
    }
}
