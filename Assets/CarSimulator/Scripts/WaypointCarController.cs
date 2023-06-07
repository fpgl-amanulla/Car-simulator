using UnityEngine;

namespace CarSimulator.Scripts
{
    public class WaypointCarController : MonoBehaviour
    {
        public Transform[] waypoints;
        public float speed = 5f;
        private float rotationSpeed = 25;

        public int CurrentWaypointIndex { get; set; } = 0;
        private bool _isDestinationReached = false;
        public bool IsMovementEnabled { get; set; } = false;
        public bool IsRightSide { get; set; } = false;


        private void Update()
        {
            if (_isDestinationReached || !IsMovementEnabled) return;

            if (waypoints.Length == 0)
            {
                Debug.LogWarning("No waypoints assigned!");
                return;
            }

            Vector3 targetPosition = waypoints[CurrentWaypointIndex].position;
            Vector3 direction = targetPosition - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                CurrentWaypointIndex++;
                if (CurrentWaypointIndex >= waypoints.Length)
                {
                    if (IsRightSide)
                    {
                        Destroy(gameObject);
                        return;
                    }

                    _isDestinationReached = true;
                    //_currentWaypointIndex = 0;
                }
            }
        }
    }
}