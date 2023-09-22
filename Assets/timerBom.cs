using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerBom : MonoBehaviour
{
    [SerializeField] private target[] targets;
    private int numberTargetRequest;
    private int numberTargetResponse;
    [SerializeField] private Transform destroyVisual;
    [SerializeField] private SoundSO soundSO;
    private int lastHittedIndex;
    private int targetIndex;
    private bool isHit;
    void Start()
    {
        numberTargetRequest = 5;
        numberTargetResponse=0;
        isHit = false;
        targetIndex=Random.Range(0, targets.Length);
        targets[targetIndex].targetVisual.gameObject.SetActive(true);
        destroyVisual.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(numberTargetResponse);
        if (numberTargetResponse == numberTargetRequest)
        {
            AudioSource.PlayClipAtPoint(soundSO.destroyed,transform.position);
            destroyVisual.gameObject.SetActive(true);
            Invoke("DesSefl", 1f);
        }
        if(isHit) { isHit = false;
           
            if (lastHittedIndex == targetIndex)
            {
                Debug.Log("true");
                numberTargetResponse++;
                AudioSource.PlayClipAtPoint(soundSO.correct, transform.position);
            }
            else
            {
                Debug.Log("false");
                numberTargetResponse = 0;
                AudioSource.PlayClipAtPoint(soundSO.fail, transform.position);
            }
         targets[targetIndex].targetVisual.gameObject.SetActive(false);
            targetIndex = Random.Range(0, targets.Length);
            targets[targetIndex].targetVisual.gameObject.SetActive(true);
        }
     

    }
    public void Check()
    {
   for(int i = 0;i< targets.Length; i++)
        {
            if (targets[i].lastHit) {
            isHit = true;
                lastHittedIndex = i;
                targets[i].lastHit = false;
                break;
            }
        }
    }
    private void DesSefl()
    {
        Destroy(gameObject);
    }
}
