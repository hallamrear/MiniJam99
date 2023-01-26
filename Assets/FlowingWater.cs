using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingWater : MonoBehaviour
{
    public Transform Target;
    public int MovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            collision.gameObject.transform.position = Vector3.MoveTowards(collision.gameObject.transform.position, Target.position, Time.deltaTime * MovementSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>())
        {
            collision.gameObject.GetComponent<PlayerMovement>().IsMoving = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            collision.gameObject.GetComponent<PlayerMovement>().IsMoving = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (Target)
        {
            Gizmos.DrawWireSphere(Target.position, 0.1f);
            Gizmos.DrawLine(transform.position, Target.position);
        }
    }
}
