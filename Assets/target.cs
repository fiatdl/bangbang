using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public bool isHited;
    [SerializeField] private timerBom timerBom;
    [SerializeField] public Transform targetVisual;
    public bool lastHit;
    void Start()
    {
    
        lastHit = false;
        isHited = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit()
    {
        lastHit=true;
        isHited=true;
        timerBom.Check();
    }
    public void Tran()
    {
        isHited = false;
    }
}
