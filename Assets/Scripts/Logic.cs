using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    [SerializeField] Map map = null;
    [SerializeField] EnemyManager enemyMgr = null;
    [SerializeField] PlayerController player = null;

    Stair targetStair =  null;
    int stairIndex = 0;
    int indexObs = 0;
    bool isKillingPlayer = false;
    bool wasCheckResult = true;

    void Awake()
    {
        GameQuick.logic = this;
    }

    public void CheckShoot(float delay)
    {
        if (!wasCheckResult)
        {
            Invoke("CheckResultShoot", delay);
        }
    }

    public void CheckResultShoot()
    {
        wasCheckResult = true;
        if (enemyMgr.currentEnemy && enemyMgr.currentEnemy.isDied)
        {
            enemyMgr.DestroyCurrentEnemy();
            NextEnemy();
        }
        else
        {
            wasCheckResult = true;
            isKillingPlayer = true;
            Debug.Log("LOSE");
        }
    }

    void GetCurrentStair(int index)
    {
        stairIndex = index;
        indexObs = 0;
        targetStair = map.stairs[stairIndex];
        targetStair.SetEnableColliderStair(true);
    }

    public GameObject NextFontObs()
    {
        if (targetStair == null)
        {
            if(stairIndex == map.stairs.Count)
            {
                map.AddStair();
            }
            GetCurrentStair(stairIndex);
        }
        if(indexObs >= 0 && indexObs < targetStair.stairList.Count)
        {
            return targetStair.stairList[indexObs++];
        }
        else
        {
            return null;
        }
    }

    public void NextStair()
    {
        map.AddStair();
        stairIndex++;
        targetStair.SetEnableColliderWall(true);
        targetStair = null;
        GetCurrentStair(stairIndex);
    }

    public void Play()
    {
        wasCheckResult = false;
        map.UpdateColor(stairIndex - 1);
        NextStair();
        enemyMgr.SpawnAtStair(targetStair, 0);
    }

    public void NextEnemy()
    {
        player.MoveToNextStair();
    }

    public void Reset()
    {
        targetStair = null;
        stairIndex = 0;
        indexObs = 0;
    }
}
