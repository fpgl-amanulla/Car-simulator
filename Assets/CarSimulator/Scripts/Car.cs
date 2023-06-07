using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CarSimulator.Scripts
{
    public class Car : MonoBehaviour
    {
        public List<Transform> wheelList = new List<Transform>();
        private WaypointCarController _waypointCarController;

        private float _rotationSpeed = 1100;

        private bool _hasController;

        private void Start()
        {
            _hasController = TryGetComponent(out _waypointCarController);
        }

        private void Update()
        {
            if (!_hasController) return;
            if (!_waypointCarController.IsMovementEnabled) return;

            for (int i = 0; i < wheelList.Count; i++)
            {
                wheelList[i].Rotate(Vector3.left * (_rotationSpeed * Time.deltaTime));
            }
        }
    }
}