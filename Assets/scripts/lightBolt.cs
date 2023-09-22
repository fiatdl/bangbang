using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightBolt : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private LayerMask tankLaymask;
    private float damage;
    void Start()
    {

        damage = 0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir= end.transform.position - start.transform.position;
        if (Physics.Raycast(start.position, dir, out RaycastHit raycastHit, 8f, tankLaymask)) 
        {
         
            if (raycastHit.transform.TryGetComponent(out tank tank))
            {


                tank.TakedDamage(damage);
                tank.isElectric = true;


            }
        }
    }
}
