using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGunGameItem : GameItem
{
    // Start is called before the first frame update
    public float healAmount;
    public float healtime;
    public bool isAvaiable;
    public float avaiableTime;
    public float avaiableCurrentTime;
    public static UpdateGunGameItem Instance { get; private set; }
    void Start()
    {
        healAmount = 50f;
        Instance = this;
        healtime = 5f;
        isAvaiable = true;
        avaiableTime = 10f;
        avaiableCurrentTime = 0f;


    }

    // Update is called once per frame
    void Update()
    {
        timeLife -= Time.deltaTime;
        if (timeLife <= 0)
        {
            Destroy(gameObject);
        }
    }
    public override void Interact()
    {

    }
}
