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
    [SerializeField] int startXMin;
    [SerializeField] int startXMax;
    [SerializeField] int highMin;
    [SerializeField] int highMax;
    
    int startColor = 0;
    int startOrder = 100;
    int startLine = 0;

    class Stair
    {
        public List<GameObject> stairList = null;
        public GameObject wall = null;
        public Stair()
        {
            stairList = new List<GameObject>();
        }
    }

    public enum Direction { LEFT, RIGHT }

    void Start()
    {
        int numberStair = 15;
        for(int i = 0; i < numberStair; i++)
        {
            int startX = Random.Range(startXMin, startXMax);
            int high = Random.Range(highMin, highMax);
            Direction direction = i % 2 == 0 ? Direction.LEFT : Direction.RIGHT;
            GenerateStairs(direction, startLine, startX, high, startColor, startOrder, distance);

            startColor++;
            startOrder--;
            startLine += high;
        }
    }

    void Update()
    {
        
    }

    Stair GenerateStairs(Direction from, int startLine, float startX, int high, int colorIndex, int order, Vector2 rate)
    {
        Stair stair = new Stair();
        int add = (from == Direction.LEFT ? -1 : 1);
        for (int i = 0; i < high; i++)
        {
            float x = startX + i * add;
            float y = startLine + i;
            GameObject square = SpawnSquare(from, new Vector2(x, y), GetColor(colorIndex), startOrder, rate);
            stair.stairList.Add(square);
        }
        GameObject wall = SpawnSquare(Direction.RIGHT, new Vector2(this.start / rate.x, startLine), GetColor(colorIndex + 1), startOrder - 1, rate);
        wall.transform.localScale += new Vector3(0, high - 1, 0);

        stair.wall = wall;

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
