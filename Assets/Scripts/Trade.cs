using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Trade
{

    [SerializeField] private TextMeshProUGUI _tradeTxt;
    [SerializeField] private TextMeshProUGUI _tradeCost;
    [SerializeField] private TextMeshProUGUI _upgradeTxt;
    [SerializeField] private TextMeshProUGUI _upgradeCost;

    [SerializeField] private GameObject _buttonUpgrade;
    [SerializeField] private GameObject _buttonSellAlsoBuild;
    [SerializeField] private GameObject _canvas;

    [SerializeField] private Player _player;

    [SerializeField] private float _build;
    [SerializeField] private float _sell;
    [SerializeField] private float _upgrade;

    public TextMeshProUGUI TradeTxt => _tradeTxt;
    public TextMeshProUGUI TradeCost => _tradeCost;
    public TextMeshProUGUI UpgradeTxt => _upgradeTxt;
    public TextMeshProUGUI UpgradeCost => _upgradeCost;

    public Player Player => _player;

    public float Biuld => _build;
    public float Upgrade => _upgrade;
    public float Sell => _sell;

    public GameObject Canvas => _canvas;

    public void AcivateButton(Transform currentPoint, bool isPlant)
    {

        _canvas.SetActive(true);
        _canvas.transform.position = currentPoint.position;
        _canvas.transform.position = new Vector3(_canvas.transform.position.x, _canvas.transform.position.y + 10f, _canvas.transform.position.z);

        if (!isPlant)
        {
            _buttonUpgrade.SetActive(false);
            _tradeTxt.text = "Build";
            _tradeCost.text = _build.ToString();

        }
        else
        {

            _buttonUpgrade.SetActive(true);
            _tradeTxt.text = "Sell";
            _tradeCost.text = _sell.ToString();

        }
    }

    public void ShowPriceForCurrentTower(Tower tower) => _upgradeCost.text = tower.UpGradePrice.ToString();

    public void UpdatePriceUpGrade(float updatePrice, Tower tower)
    {
        tower.UpGradePrice += updatePrice;

        _upgradeCost.text = tower.UpGradePrice.ToString();
    }




}
