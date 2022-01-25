using System;
using Managers;
using UnityEngine;

namespace Logics.Props
{
    public class Obstacles :MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                print("xxdddxd");
                // todo: health
                UiManager.Instance.HealthSpawner.DecreaseHearths();
            }
        }
    }
}