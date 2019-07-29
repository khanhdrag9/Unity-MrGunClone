using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Enemy[] enemies = null;
    [SerializeField] Range xMore = null;
    [SerializeField] PlayerController player = null;
    public Enemy currentEnemy { get; private set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyCurrentEnemy()
    {
        if(currentEnemy)
        {
            // currentEnemy.GetComponent<Die>().PlayEffect();  
            Destroy(currentEnemy.gameObject, 2f);
        } 
        currentEnemy = null;
    }

    public void KillPlayer()
    {
        if(currentEnemy)
        {
            var es = currentEnemy.GetComponent<Shooter>();
            if (es)
            {
                Debug.Log("Enemy Shooter");
                es.ShootTo(player.transform.position);
            }
        }
    }


    public Enemy Spawn(int index)
    {
        var enemy = Instantiate(enemies[index]);
        currentEnemy = enemy;
        return enemy;
    }
}
