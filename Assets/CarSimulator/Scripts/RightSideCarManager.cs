using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace CarSimulator.Scripts
{
    public class RightSideCarManager : MonoBehaviour
    {
        public List<GameObject> carPrefabList = new List<GameObject>();
        public Transform[] waypoints;

        private void Start()
        {
            GenerateRandom();
            StartCoroutine(GenerateCar());
        }

        private IEnumerator GenerateCar()
        {
            while (true)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(3, 5));
                InitCar(0);
            }
        }


        private void GenerateRandom()
        {
            List<int> randomPoints = GetRandomPoints(10);
            for (int i = 0; i < 10; i++)
            {
                InitCar(randomPoints[i]);
            }
        }

        private void InitCar(int point)
        {
            GameObject car = Instantiate(carPrefabList[UnityEngine.Random.Range(0, carPrefabList.Count)],
                transform);
            WaypointCarController waypointCarController = car.AddComponent<WaypointCarController>();
            waypointCarController.waypoints = waypoints;
            car.transform.position = waypoints[point].position;

            waypointCarController.CurrentWaypointIndex = point;
            waypointCarController.IsMovementEnabled = true;
            waypointCarController.IsRightSide = true;
        }

        private List<int> GetRandomPoints(int length)
        {
            int minValue = 0;
            int maxValue = waypoints.Length - 3;

            Random random = new Random();
            List<int> uniqueValues = new List<int>();

            while (uniqueValues.Count < length)
            {
                int randomValue = random.Next(minValue, maxValue + 1);
                uniqueValues.Add(randomValue);
            }

            return uniqueValues;
        }
    }
}