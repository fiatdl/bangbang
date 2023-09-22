using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamp : EnemyObject
{
    [SerializeField] private EnemySO minion;
    [SerializeField] private road roadd;
    public float timerToSpwnMinion;
    public float timerToSpwnMaxionCurrent;
    private int amountOfMinion;
    // Start is called before the first frame update
    void Start()
    {
        destroyVisual.gameObject.SetActive(false);
        amountOfMinion = 0;
        timerToSpwnMinion = 10f;
        timerToSpwnMaxionCurrent = 0f;
        hp = 5000;
        CurrentHp = hp;

        gold.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        timerToSpwnMaxionCurrent-=Time.deltaTime;
        if (timerToSpwnMaxionCurrent <= 0f&&amountOfMinion<=12)
        {

            amountOfMinion++;
            Transform bulletTransform = Instantiate(minion.preFab, transform.position, Quaternion.identity, bullets);
            minion kitchenObject = bulletTransform.GetComponent<minion>();
            kitchenObject.target = gameManagement.Instance.mainTank;
            kitchenObject.nextRay = roadd;
            kitchenObject.moveDir = new Vector3(roadd.transform.position.x - transform.position.x, 0f, roadd.transform.position.z - transform.position.z).normalized;
            timerToSpwnMaxionCurrent = timerToSpwnMinion;
        }
    }
}
