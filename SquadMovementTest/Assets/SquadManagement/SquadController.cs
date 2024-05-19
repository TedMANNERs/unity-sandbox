using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SquadController : MonoBehaviour
{
    private NavMeshAgent[] _navMeshAgents;
    private Vector3 _destination;

    private void Awake()
    {
        _navMeshAgents = GetComponentsInChildren<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_destination != transform.position)
        {
            foreach (var agent in _navMeshAgents)
            {
                agent.destination = _destination;
            }
        }
    }

    private void OnLookAround(InputValue value)
    {
        Debug.Log("Looking around");
    }
    
    private void OnMoveForward(InputValue value)
    {
        Debug.Log("Move Forward");
        Vector3 direction = transform.forward;
        _destination = transform.position + direction;
    }
    
    private void OnMoveLeft(InputValue value)
    {
        
    }
    
    private void OnMoveBackward(InputValue value)
    {
        
    }
    
    private void OnMoveRight(InputValue value)
    {
        
    }
}
