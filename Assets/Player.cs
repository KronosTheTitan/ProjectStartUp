    using System.Collections;
using System.Collections.Generic;
    using Mirror;
    using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    

    [SerializeField] private float speed = 1;
    [SerializeField] private float maxSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOwned)
        {
            Vector3 dir = new Vector3(1,0,1);
            MovePlayer(dir);
        }
    }

    [SerializeField] private float distanceSinceFloor = 0;
    
    [ServerCallback]
    void MovePlayer(Vector3 dir)
    {
        if (Physics.Raycast(transform.position,Vector3.down,.75f))
        {
            Debug.Log("Could not find floor");
            if (distanceSinceFloor > .05f)
            {
                rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
                return;
            }
            else
            {
                distanceSinceFloor += (rigidbody.velocity * Time.deltaTime).magnitude * Time.deltaTime;
                Debug.Log(distanceSinceFloor);
            }
        };
        Debug.Log("resetting distance since floor");

        distanceSinceFloor = 0;
        
        Vector3 actualDirection = Vector3.Lerp(dir,rigidbody.velocity.normalized,0.5f);
        rigidbody.velocity += actualDirection * speed;
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity,maxSpeed);
    }
}
