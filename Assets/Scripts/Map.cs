using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]GameObject square = null;
    [SerializeField] float start = -7;
    [SerializeField] float end = 7;
    [SerializeField] Vector2 distance = new Vector2(0.5f, 0.5f);
    [SerializeField] Color[] colors;
    int startColor = 0;
    int startOrder = 100;

    class Stair
    {
        public List<GameObject> stairList = null;
        public List<GameObject> wall = null;
        public Stair()
        {
            stairList = new List<GameObject>();
            wall = new List<GameObject>();
        }
    }

    public enum Direction { LEFT, RIGHT }

    void Start()
    {
        int numberStair = 7;
        int startLine = 0;
        for(int i = 0; i < numberStair; i++)
        {
            int startX = Random.Range(0, 1);
            int high = Random.Range(3, 3);
            Direction direction = i % 2 == 0 ? Direction.LEFT : Direction.RIGHT;
            GenerateStairs(direction, startLine, startX, high, distance);
            startLine += high;
        }
    }

    void Update()
    {
        
    }

    Stair GenerateStairs(Direction from, float startLine, float startX, float high, Vector2 rate)
    {
        Stair stair = new Stair();
        int add = (from == Direction.LEFT ? -1 : 1);
        for (int i = 0; i < high; i++)
        {
            float x = startX + i * add;
            float y = startLine + i;
            GameObject square = SpawnSquare(from, new Vector2(x, y), GetColor(startColor), startOrder, rate);
            stair.stairList.Add(square);

            GameObject wall = SpawnSquare(Direction.RIGHT, new Vector2(this.start * 2, y), GetColor(startColor + 1), startOrder - 1, rate);
            stair.wall.Add(wall);
        }
        startColor++;
        startOrder--;
        return stair;
    }

    GameObject SpawnSquare(Direction from, Vector2 position, Color color, int order, Vector2 rate)
    {
        var obj = Instantiate(square, transform);
        obj.transform.position = new Vector2(position.x * rate.x, position.y * rate.y);

        var sprite = obj.GetComponent<SpriteRenderer>();
        sprite.flipX = from == Direction.LEFT ? true : false;
        sprite.color = color;
        sprite.sortingOrder = order;
  
        return obj;
    }

    Color GetColor(int index)
    {
        if (index >= colors.Length) return Color.black;
        else return colors[index];
    }
}
