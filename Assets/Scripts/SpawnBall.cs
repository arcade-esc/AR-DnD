using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBall : MonoBehaviour
{
    public GameObject ballPrefab;
    public float spawnSpeed = 3;
    public InputActionProperty inputAction;

    // Update is called once per frame
    void Update()
    {
        if(inputAction.action.WasPerformedThisFrame())
        {
            GameObject thrownBall = Instantiate(ballPrefab, transform.position, transform.rotation);
            Rigidbody ballRigidBody = thrownBall.GetComponent<Rigidbody>();
            ballRigidBody.velocity = transform.forward * spawnSpeed;
        }
    }
}
