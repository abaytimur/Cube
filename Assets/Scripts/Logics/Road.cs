using Controllers;
using Managers;
using UnityEngine;
using Utilities;

namespace Logics
{
    public class Road : MonoBehaviour
    {
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
    }
}