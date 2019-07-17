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

    void GetCurrentStair(int index)
    {
        stairIndex = index;
        indexObs = 0;
        targetStair = map.stairs[stairIndex];
        targetStair.SetEnableColliderStair(true);
    }

    public GameObject NextFontObs()
    {
        if(targetStair == null)
        {
            if(stairIndex == map.stairs.Count)
            {
                //Create more 1 row...
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
        stairIndex++;
        targetStair = null;
        GetCurrentStair(stairIndex);
        map.UpdateColor(stairIndex - 1);
    }

    public void Play()
    {
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
