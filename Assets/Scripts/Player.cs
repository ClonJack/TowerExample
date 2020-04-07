using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] private Slider _sliderHp;

    [SerializeField] private float _hp;

    [SerializeField] private float _money;
    [SerializeField] private TextMeshProUGUI _textMoney;

    [SerializeField] private GameObject _panelOver;



    private void Start()
    {
        _sliderHp.value = _hp;
        _textMoney.text = _money.ToString();
    }


    public bool IsCompleteTrade(float cost)
    {
        var sum = (_money - cost);



        if (sum >= 0)
        {
            _money -= cost;
            _textMoney.text = _money.ToString();

            return true;


        }

        return false;

    }


    public void CompleteBuy(float cost)
    {
        _money += cost;
        _textMoney.text = _money.ToString();
    }

    public bool IsCompleteUpGrade(float cost)
    {
        var sum = _money - cost;

        if (sum >= 0)
        {
            _money -= cost;
            _textMoney.text = _money.ToString();
            return true;
        }

        else return false;



    }

    public bool IsCompleteKillEnemy(int money, bool IsKill)
    {
        if (IsKill)
        {
            _money += money;
            _textMoney.text = _money.ToString();

            return true;
        }

        return false;
    
    }

    public void SetDamage(float hp)
    {
        _hp -= hp;
        _sliderHp.value = _hp;


        if (_hp <= 0)
            _panelOver.SetActive(true);


    }


    public void Restart(int id)
    {

        SceneManager.LoadSceneAsync(id);
     
    }
}
