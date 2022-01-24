using System;
using Controllers;
using JetBrains.Annotations;
using Managers;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Logics
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private Transform propSpawnTransform;

        private void Start()
        {
            SpawnRandomProps();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!RoadController.Instance.isEndRoadSpawned &&
                    RoadController.Instance.RoadList.Count >= RoadController.Instance.TotalRoadCount)
                {
                    RoadController.Instance.isEndRoadSpawned = true;
                    SpawnEndRoad();
                }
                else if (!RoadController.Instance.isEndRoadSpawned &&
                         RoadController.Instance.RoadList.Count <
                         RoadController.Instance.TotalRoadCount)
                {
                    SpawnNextRoad();
                }
            }
        }

        private void SpawnEndRoad()
        {
            ObjectPooler.Instance.SpawnFromPool(Extensions.EndRoadName,
                transform.position + Vector3.forward *
                (Extensions.RoadOffset * RoadController.Instance.InitialRoadCount), Quaternion.identity);
        }

        private void SpawnNextRoad()
        {
            RoadController.Instance.RoadList.Add(ObjectPooler.Instance.SpawnFromPool(Extensions.RoadName,
                transform.position + Vector3.forward *
                (Extensions.RoadOffset * RoadController.Instance.InitialRoadCount), Quaternion.identity));
        }

        private void SpawnRandomProps()
        {
            if (Random.Range(0, 1f) <= .6f)
            {
                SpawnDiamonds();
            }
            else
            {
                SpawnObstacles();
            }
        }

        private void SpawnObstacles()
        {
            ObjectPooler.Instance.SpawnPropsFromPool(Extensions.ObstacleName, new Vector3(
                    Random.Range(-Extensions.ObstacleXOffset, Extensions.ObstacleXOffset),
                    0,
                    Random.Range(Extensions.ObstacleZMinOffset, Extensions.ObstacleZMaxOffset)),
                Quaternion.Euler(0, 90, 0),
                propSpawnTransform);
        }

        private void SpawnDiamonds()
        {
            if (Random.Range(0,1f)<.5f)
            {
                ObjectPooler.Instance.SpawnPropsFromPool(Extensions.DiamondName, new Vector3(
                        Random.Range(-Extensions.ObstacleXOffset, Extensions.ObstacleXOffset),
                        .75f,
                        Random.Range(Extensions.ObstacleZMinOffset, Extensions.ObstacleZMaxOffset)),
                    Quaternion.Euler(0, 90, 0),
                    propSpawnTransform);
            }
            else
            {
                ObjectPooler.Instance.SpawnPropsFromPool(Extensions.DiamondAlternativeName, new Vector3(
                        Random.Range(-Extensions.ObstacleXOffset, Extensions.ObstacleXOffset),
                        .75f,
                        Random.Range(Extensions.ObstacleZMinOffset, Extensions.ObstacleZMaxOffset)),
                    Quaternion.Euler(0, 90, 0),
                    propSpawnTransform);
            }
        }
    }
}