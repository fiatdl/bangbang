using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public float timeLife;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Interact() { }
    public void SetTimeLife(float time)
    {
        timeLife = time;
    }
}
