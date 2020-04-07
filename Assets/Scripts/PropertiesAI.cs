using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PropertiesAI
{

    public float Speed;
    public float Hp;
    public float Damage;
    public int Money;

  

    public List<Transform> paths;

    public GameObject Prefab
    {
        get
        {
            return Resources.Load<GameObject>("Enemy/Cylinder");
        }
    }

    public PropertiesAI(PropertiesAI properties)
    {
        Speed = properties.Speed;
        Hp = properties.Hp;
        Damage = properties.Damage;
        paths = properties.paths;
        Money = properties.Money;
    }

    public PropertiesAI() { }

    public void PropertiesUpdate(int nextWavetUpdate)
    {

        Damage += Random.Range(0, nextWavetUpdate);
        Hp += Random.Range(0, nextWavetUpdate);
        Speed += Random.Range(0, nextWavetUpdate);
        Money += Random.Range(0, (int)nextWavetUpdate);


    }

    public Player Player
    {
        get
        {
            return GameObject.FindObjectOfType<Player>();
        }
    }



}
