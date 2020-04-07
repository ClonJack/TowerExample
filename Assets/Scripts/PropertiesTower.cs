using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PropertiesTower
{
    public float Hp;
   
    public float Damage;
    public float TimeShot = 1f;

    public GameObject prefab;

    [Header("UPGRADE POINT")]
    public float NextUpHP;
    public float NextUpDamage;
   




    public void ApplyUpGrade(Tower tower)
    {
        Hp += NextUpHP;
        
        Damage += NextUpDamage;
    }

    public PropertiesTower()
    {
        Hp = 100;
        Damage = 100;
    }

}
