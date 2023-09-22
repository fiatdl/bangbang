using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gun : MonoBehaviour
{
   
    [SerializeField] private Transform onwer;
    public Vector3 targetPosition;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
     
        transform.forward = Vector3.Slerp(transform.forward, GetDirToTarget(targetPosition), Time.deltaTime * 10f);

    }
   private  Vector3 GetDirToTarget(Vector3 TargetPosition)
    {
        targetPosition.y = 0f;
        targetPosition.x -= onwer.transform.position.x;
        targetPosition.z -= onwer.transform.position.z;
        return targetPosition.normalized;

    }
}
