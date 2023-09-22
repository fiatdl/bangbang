using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class waterWave : MonoBehaviour
{
    [SerializeField] private VisualEffect[] waterwaves;
    [SerializeField] private LayerMask hitAble;
    // Start is called before the first frame update
    private float coundown = 13f;
    private float coundownCurrent;
    void Start()
    {
        coundownCurrent = 0;
        for (int i = 0;  i < waterwaves.Length; i++){
            waterwaves[i].Stop();
       
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (coundownCurrent > coundown)
        {
   int index= Random.Range(0,waterwaves.Length);
        waterwaves[index].Play();
            coundownCurrent = 0;
        }
        coundownCurrent += Time.deltaTime;

        DetectObject();
    }
    private void DetectObject()
    {
        Vector3 lastInteractionDir;
   
        Vector3 moveDir = new Vector3(0f, 0f,1f);
        if (moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }
        float interactionDistant = 7f;
        if (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactionDistant, hitAble))
        {


            if (raycastHit.transform.TryGetComponent(out tank player))
            {

                Debug.Log("interact");
                player.TakedDamage(2f);


            }
          
        }
       
    }
}
