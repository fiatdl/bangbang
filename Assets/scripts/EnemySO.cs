using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu()] 
public class EnemySO : ScriptableObject
{
    [SerializeField] public string enemyName;
    [SerializeField] public float coin;
    [SerializeField] public float exp;
    [SerializeField] public EnemyObject enemyTransform;
    [SerializeField] public Transform preFab;
    [SerializeField] public Sprite avatar;
}
