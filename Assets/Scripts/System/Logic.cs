using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public int stairIndex = 0;
    int indexObs = 0;
    bool isKillingPlayer = false;
    bool wasCheckResult = true;

    void Awake()
    {
        GameQuick.logic = this;
    }

}
