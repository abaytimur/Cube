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
            var tempObj = ObjectPooler.Instance.SpawnFromPool(Extensions.RoadName,
                transform.position + Vector3.forward *
                (Extensions.RoadOffset * RoadController.Instance.InitialRoadCount), Quaternion.identity);
            RoadController.Instance.RoadList.Add(tempObj);
            tempObj.GetComponent<Road>().SpawnRandomProps();
        }

        private void SpawnRandomProps()
        {
            if (Random.Range(0, 1f) <= 0.4f)
            {
                SpawnDiamonds();
            }
            else if (Random.Range(0, 1f) <= 0.6f)
            {
                SpawnShields();
            }
            {
                SpawnObstacles();
            }
        }

        private void SpawnShields()
        {
            var tempObject = ObjectPooler.Instance.SpawnPropsFromPool(Extensions.ShieldName, new Vector3(
                    Random.Range(-Extensions.ObstacleXOffset, Extensions.ObstacleXOffset),
                    0,
                    Random.Range(Extensions.ObstacleZMinOffset, Extensions.ObstacleZMaxOffset)),
                Quaternion.Euler(0, 90, 0),
                propSpawnTransform);

            tempObject.transform.parent = null;
        }

        private void SpawnObstacles()
        {
           var tempObject = ObjectPooler.Instance.SpawnPropsFromPool(Extensions.ObstacleName, new Vector3(
                    Random.Range(-Extensions.ObstacleXOffset, Extensions.ObstacleXOffset),
                    0,
                    Random.Range(Extensions.ObstacleZMinOffset, Extensions.ObstacleZMaxOffset)),
                Quaternion.Euler(0, 90, 0),
                propSpawnTransform);

           tempObject.transform.parent = null;
        }

        private void SpawnDiamonds()
        {
            var randomNumber = Random.Range(0, 10f);
            if (randomNumber< 5f)
            {
                var temporaryObject = ObjectPooler.Instance.SpawnPropsFromPool(Extensions.DiamondName, new Vector3(
                        Random.Range(-Extensions.ObstacleXOffset, Extensions.ObstacleXOffset),
                        .75f,
                        Random.Range(Extensions.ObstacleZMinOffset, Extensions.ObstacleZMaxOffset)),
                    Quaternion.Euler(0, 90, 0),
                    propSpawnTransform);
                temporaryObject.transform.parent = null;
            }
            else
            {
                var temporaryObject = ObjectPooler.Instance.SpawnPropsFromPool(Extensions.DiamondAlternativeName, new Vector3(
                        Random.Range(-Extensions.ObstacleXOffset, Extensions.ObstacleXOffset),
                        .75f,
                        Random.Range(Extensions.ObstacleZMinOffset, Extensions.ObstacleZMaxOffset)),
                    Quaternion.Euler(0, 90, 0),
                    propSpawnTransform);
                temporaryObject.transform.parent = null;

            }
        }
    }
}