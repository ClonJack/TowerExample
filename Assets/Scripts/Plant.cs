using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{


    [SerializeField] private Manager _manager;



    private void Start()
    {
        _manager = FindObjectOfType<Manager>();

    }

    private void OnMouseDown()
    {

        _manager.CurrentPlant = transform;
        _manager.Trade.AcivateButton(transform, IsPlant());

        if (IsPlant())
        {
            var curTw = transform.GetChild(0);
            _manager.tower = curTw.GetChild(0).GetComponent<Tower>();

        }
        
            if (_manager.tower != null)
                _manager.Trade.ShowPriceForCurrentTower(_manager.tower);




    }


  

    private bool IsPlant()
    {

        if (transform.GetChild(0).childCount > 0)
            return true;

        return false;
    }


}
