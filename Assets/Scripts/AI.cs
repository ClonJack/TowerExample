using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AI : MonoBehaviour
{
    
    [SerializeField] private Transform _currentPoint;
    public float Hp => Properties.Hp;

    [SerializeField] private int _idPoint = 0;

    public PropertiesAI Properties;

    [SerializeField] private bool isFinal;
    [SerializeField] private Manager manager;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
        _currentPoint = Properties.paths[0];

    }

    private void Update()
    {
        Path();
        NextPoint();
        ToDamagePlayer(Properties.Player);
    }

    private void NextPoint()
    {

        var finalPoint = Properties.paths.Count - 1;

        if (transform.position == _currentPoint.position && finalPoint > _idPoint)
            _idPoint++;


        _currentPoint = Properties.paths[_idPoint];


        isFinal = (transform.position == _currentPoint.position && finalPoint == _idPoint);




    }

    public void SetDamage(float damage)
    {
        Properties.Hp -= damage;

        if (manager.player.IsCompleteKillEnemy(Properties.Money, Properties.Hp <= 0))
            Destroy(gameObject);


    }

    private void ToDamagePlayer(Player player)
    {
        if (isFinal)
        {
            player.SetDamage(Properties.Damage);
            Destroy(gameObject);

        }
    }

    private void Path() => transform.position = Vector3.MoveTowards(transform.position, _currentPoint.position, Properties.Speed * Time.deltaTime);






}


