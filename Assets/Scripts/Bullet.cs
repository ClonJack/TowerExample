using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private AI _ai;
    [SerializeField] private float _damage;



    private void Update() => Hit();





    private void Hit()
    {


        if (_target != null)
        {


            var moveTarget = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);

            var thisTransform = Vector3Int.CeilToInt(transform.position);
            var moveTrgetInt = Vector3Int.CeilToInt(moveTarget);




            if (moveTrgetInt.x == thisTransform.x && moveTrgetInt.z == thisTransform.z)
            {
                _ai.SetDamage(_damage);
                Destroy(gameObject);
            }
            else
                transform.position = new Vector3(moveTarget.x, transform.position.y, moveTarget.z);



        }

        else
            Destroy(gameObject);


    }

    public void SetShotValue(Transform target, float speed, AI aI, float damage)
    {
        _target = target;
        _speed = speed;
        _ai = aI;
        _damage = damage;
    }

}
