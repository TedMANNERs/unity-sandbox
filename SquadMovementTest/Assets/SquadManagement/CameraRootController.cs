using System;
using System.Linq;
using UnityEngine;

namespace SquadManagement
{
    public class CameraRootController : MonoBehaviour
    {
        private CharacterController[] _squadMembers;

        private void Start()
        {
            _squadMembers = transform.parent.GetComponentsInChildren<CharacterController>();
        }

        private void Update()
        {
            var squadCenter = _squadMembers
                .Select(x => x.transform.position)
                .Aggregate((a, b) => a + b) / _squadMembers.Length;
            transform.position = squadCenter;
        }
    }
}