using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu()]
public class TankItemByShop : ScriptableObject
{
    
    public Sprite sprite;
    public string objectName;


    public float hP;
    public float moveSpeed;
    public float attackSpeed;
    public float armor;
    public float coolDown;
    public float critPercent;
    public float damamge;
    public float price;
}
