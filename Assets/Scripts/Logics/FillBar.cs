using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class FillBar : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _finishLine;

    private bool _calculate = true;

    [SerializeField] private Image fillImage;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        _finishLine = (Vector3.forward * (RoadController.Instance.TotalRoadCount - 1) *
                       Extensions.RoadOffset) + (Vector3.forward * Extensions.RoadOffset / 2);
    }

    private void LateUpdate()
    {
        if (!_calculate) return;

        var temporaryFillAmount = _finishLine - new Vector3(0, 0, Mathf.Clamp(_player.transform.position.z, 0, _finishLine.z));
        var fillAmountFloat = temporaryFillAmount.z / _finishLine.z;
         
        fillImage.fillAmount = 1 - fillAmountFloat;
        if (fillImage.fillAmount >= 1)
        {
            _calculate = false;
        }
    }
}
