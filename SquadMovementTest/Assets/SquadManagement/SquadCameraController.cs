using System;
using UnityEngine;

namespace SquadManagement
{
    [RequireComponent(typeof(Camera))]
    public class SquadCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _squad;

        private void Update()
        {
            transform.position = _squad.transform.position + new Vector3(0, 1, -5);
        }
    }
}