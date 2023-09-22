using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class aoeBullet : MonoBehaviour
{
    private float range;
    public AudioClip sound;
    public static aoeBullet Instance { get; private set; }
    public float dame;
    [SerializeField] public LayerMask tankMask;
    // Start is called before the first frame update
    private void Awake()
    {
        
        Instance = this;
        range = 2f;
    }
    void Start()
    {
        Invoke("DestroySefl", 2.5f);
        Invoke("Dame", 0.25f);
        Invoke("Dame", 0.5f);
        Invoke("Dame", 0.75f);
        Invoke("Dame", 1f);
        Invoke("Dame", 1.25f);
        Invoke("Dame", 1.5f);
        Invoke("Dame", 1.75f);
        Invoke("Dame", 2f);
        Invoke("Dame", 2.25f);




    }
    private void DestroySefl()
    {
        
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void Dame()
    {
       
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, tankMask);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.transform.TryGetComponent(out tank player))

                { 
                    player.TakedDamage(dame/10);


                }

            }
        }

    }
    void Update()
    {
      
    }
}
