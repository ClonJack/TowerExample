using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("For Create Bot")]
    [SerializeField] private PropertiesAI _startProperty;
    private PropertiesAI _curretnProprty;
    public int NextWavetUpdate;

    [SerializeField] private List<WaveAttack> _waves;


    [SerializeField] private float _timeSpawner;
    [SerializeField] private float _timeBetweenWave;


    [SerializeField] private Transform _startPoint;



    [Header("For Create Tower ")]
    [SerializeField] private PropertiesTower _startTower = new PropertiesTower();

    [Header("For Trade")]
    public Trade Trade = new Trade();
    public float NextPriceUpdateTower;

    public Transform CurrentPlant;
    [HideInInspector] public Tower tower;

    public Player player;

    public List<AI> currentsBot = new List<AI>();

    [Header("K")]
    [SerializeField] private int countWaves;
    [Header("X")]
    [SerializeField] private int countBotInWaves;

    private int _idWave;
    private bool _isNextWave;



    public void DoTrade()
    {

        if (Trade.TradeTxt.text== "Build")
        {
            var isBuild = Trade.Player.IsCompleteTrade(Trade.Biuld);

            if (isBuild)
                CreateTower(CurrentPlant);
        }
        else
        {
            Trade.Player.CompleteBuy(Trade.Sell);
            Destroy(CurrentPlant.GetChild(0).GetChild(0).gameObject);
        }

        Trade.Canvas.SetActive(false);
    }

    public void DoUpGrade()
    {
        if (Trade.Player.IsCompleteUpGrade(tower.UpGradePrice))
        {
            tower.ApplyUpGrade(tower.Properties);
            Trade.UpdatePriceUpGrade(NextPriceUpdateTower, tower);
        }

        Trade.Canvas.SetActive(false);

    }

    public void СheckOnNextWave()
    {
        for (int i = 0; i < currentsBot.Count; i++)
        {
            if (currentsBot[i] == null)
                _isNextWave = true;
            else
                _isNextWave = false;
        }

        if (_isNextWave)
        {
            currentsBot.Clear();

            if (_idWave < _waves.Count - 1)
            {
                _idWave++;

                StartCoroutine(ItimerBot());
            }
            _isNextWave = false;
        }

    }

    private void CreateTower(Transform pointSpawn)
    {
        if (_startTower.prefab != null)
        {
            var tower = Instantiate(_startTower.prefab, pointSpawn.position, Quaternion.identity, pointSpawn.GetChild(0)).GetComponent<Tower>();

            tower.transform.position = pointSpawn.GetChild(0).transform.position;

            tower.Properties = _startTower;


            this.tower = tower;
        }
    }

    private void СreateBot(PropertiesAI properties)
    {
        if (properties.Prefab != null)
        {
            var enemy = Instantiate(properties.Prefab, _startPoint.position, Quaternion.identity).GetComponent<AI>();
            enemy.Properties = properties;
            currentsBot.Add(enemy);
        }
    }


    private IEnumerator ItimerBot()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeBetweenWave);
        yield return wait;

        for (int j = 0; j < _waves[_idWave].Properties.Count; j++)
        {

            var currentAi = _waves[_idWave].Properties[j];

            WaitForSeconds seconds = new WaitForSeconds(_timeSpawner);
            yield return seconds;

            СreateBot(currentAi);


        }



    }

    private List<WaveAttack> Waves()
    {
        var waves = new List<WaveAttack>();

        for (int i = 0; i < countWaves; i++)
            waves.Add(new WaveAttack());


        return waves;

    }

    private void Properties(ref List<WaveAttack> waves)
    {
        for (int i = 0; i < waves.Count; i++)
        {

            var countsEnemies = Random.Range(i, countBotInWaves + i);

            if (countsEnemies <= 0)
                countsEnemies++;

            for (int j = 0; j < countsEnemies; j++)
            {
                waves[i].Properties.Add(new PropertiesAI());
                waves[i].Properties[j] = new PropertiesAI(_curretnProprty);
            }

            _curretnProprty.PropertiesUpdate(NextWavetUpdate);



        }
    }



   

    private void Start()
    {
      
        _curretnProprty = new PropertiesAI(_startProperty);
        _waves = Waves();

        Properties(ref _waves);

        StartCoroutine(ItimerBot());



    }

    private void Update()
    {
        СheckOnNextWave();

    }


}
