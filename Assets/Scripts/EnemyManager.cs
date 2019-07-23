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
        //Destroy(currentEnemy.gameObject);
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

    public Enemy SpawnAtStair(Stair stair, int index)
    {
        var enemy = Spawn(index);
        var highest = stair.stairList[stair.stairList.Count - 1];
        Transform highestTrans = highest.transform;
        float direction = highestTrans.localScale.x < 0 ? -1 : 1;
        Vector2 position = new Vector2(highestTrans.position.x + xMore.GetRandomAsInt() * direction, highestTrans.position.y + highest.transform.localScale.y);
        position.x += direction * enemy.transform.localScale.x / 1.5f;
        enemy.transform.position = position;
        enemy.transform.localScale = enemy.transform.localScale * new Vector2(direction, 1);
        return enemy;
    }

    public Enemy Spawn(int index)
    {
        var enemy = Instantiate(enemies[index]);
        currentEnemy = enemy;
        return enemy;
    }
}
