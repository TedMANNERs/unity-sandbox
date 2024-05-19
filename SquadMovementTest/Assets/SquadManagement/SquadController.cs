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
    private PlayerInputActions _inputActions;
    [Header("FOR DEBUGGING")]
    [SerializeField] private GameObject _cube;

    public Vector2 NewLookDirection { get; private set; }

    private void Start()
    {
        NewLookDirection = transform.forward;
        _navMeshAgents = GetComponentsInChildren<NavMeshAgent>();
        _inputActions = new PlayerInputActions();
        _inputActions.ControlSquad.Enable();
        _inputActions.ControlSquad.LookAround.performed += OnLookAround;
        _cube.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    private void OnLookAround(InputAction.CallbackContext context)
    {
        NewLookDirection = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (_destination != transform.position)
        {
            foreach (var agent in _navMeshAgents)
            {
                agent.destination = _destination;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = _inputActions.ControlSquad.Move.ReadValue<Vector2>();
        var forward = Camera.main.transform.forward;
        var right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Debug.Log($"movement= {movement}");
        var newDestination = forward * movement.y + right * movement.x;
        Debug.Log($"newDestination= {newDestination}");
        _destination += newDestination * (3.5f * Time.deltaTime);
        _cube.transform.position = _destination;
    }
}
