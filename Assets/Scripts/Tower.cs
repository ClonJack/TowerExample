using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private float _hp;

    [SerializeField] private float _damage;
    [SerializeField] private float _timeShot;
    [SerializeField] private float _dist;


    [HideInInspector] public PropertiesTower Properties;
    [SerializeField] private Manager _manager;


    [SerializeField] private Transform _target;


    private Quaternion currentTurn;

    [SerializeField] private Transform _pointShot;

    [SerializeField] private GameObject _bullet;


    private float _currentTime;



    public float UpGradePrice;





    private void Start()
    {


        _manager = FindObjectOfType<Manager>();
        _hp = Properties.Hp;

        _damage = Properties.Damage;
        _timeShot = Properties.TimeShot;
        _currentTime = _timeShot;

        _pointShot = transform.GetChild(0);

        UpGradePrice = _manager.Trade.Upgrade;



    }


    private void Update()
    {


        SerachNearBot();


    }


    private void SerachNearBot()
    {

        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;


        foreach (AI enemy in _manager.currentsBot)
        {
            if (enemy != null)
            {

                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy.transform;
                }

            }
        }

        if (nearestEnemy != null && shortestDistance <= 25)
        {
            _target = nearestEnemy.transform;

            currentTurn = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_target.transform.position - transform.position, Vector3.up), Time.deltaTime * 300);


            if (_currentTime <= 0 && Quaternion.Dot(transform.rotation, currentTurn) >= 0)
            {
                _currentTime = _timeShot;

                var curTr = _target.GetComponent<AI>();
                Shot(curTr);



            }

            _currentTime -= Time.deltaTime;

        }


        else
        {
            if (currentTurn != Quaternion.identity)
                currentTurn = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.zero, Vector3.up), Time.deltaTime * 300);

            _target = null;

        }



        transform.rotation = currentTurn;




    }




    private void Shot(AI currentTarget)
    {


        var currentBullet = Instantiate(_bullet, _pointShot.position, Quaternion.identity).GetComponent<Bullet>();
        currentBullet.SetShotValue(currentTarget.transform, 100, currentTarget, _damage);





    }



    private IEnumerator ITurnShot(AI currentBot)
    {
        WaitForSeconds waitFor = new WaitForSeconds(_timeShot);

        yield return waitFor;
        Shot(currentBot);

    }




    public void ApplyUpGrade(PropertiesTower properties)
    {
        _hp += properties.NextUpHP;

        _damage += properties.NextUpDamage;


    }





}
