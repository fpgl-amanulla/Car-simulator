using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarSimulator.Scripts
{
    public class TrafficLightController : MonoBehaviour
    {
        private const string Emission = "_EMISSION";

        [Space(20)] public Light lightPrefab;

        [Space(20)] public Renderer redLightRenderer;
        public Renderer yellowLightRenderer;
        public Renderer greenLightRender;

        [SerializeField] private WaypointCarController waypointCarController_1;
        [SerializeField] private WaypointCarController waypointCarController_2;

        private Light _redLight;
        private Light _yellowLight;
        private Light _greenLight;

        public void Start()
        {
            GenerateLight();

            _redLight.enabled = true;
            redLightRenderer.material.EnableKeyword(Emission);

            _yellowLight.enabled = false;
            _greenLight.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(DoTrafficSequence());
            }
        }

        private IEnumerator DoTrafficSequence()
        {
            yield return new WaitForSeconds(3.0f);
            redLightRenderer.material.DisableKeyword(Emission);
            yellowLightRenderer.material.EnableKeyword(Emission);

            _redLight.enabled = false;
            _yellowLight.enabled = true;

            yield return new WaitForSeconds(3.0f);
            yellowLightRenderer.material.DisableKeyword(Emission);
            greenLightRender.material.EnableKeyword(Emission);

            _yellowLight.enabled = false;
            _greenLight.enabled = true;

            if (waypointCarController_1 == null || waypointCarController_2 == null) yield break;

            waypointCarController_1.IsMovementEnabled = true;
            waypointCarController_2.IsMovementEnabled = true;
        }

        private void GenerateLight()
        {
            _redLight = InitLight(redLightRenderer.transform, Color.red);
            _yellowLight = InitLight(yellowLightRenderer.transform, Color.yellow);
            _greenLight = InitLight(greenLightRender.transform, Color.green);
        }

        private Light InitLight(Transform lightTr, Color color)
        {
            Light trafficLight = Instantiate(lightPrefab, lightTr);
            trafficLight.color = color;
            return trafficLight;
        }
    }
}