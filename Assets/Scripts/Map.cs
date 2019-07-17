using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    [SerializeField]GameObject square = null;
    [SerializeField] float start = -7;
    [SerializeField] float end = 7;
    public Vector2 distance = new Vector2(0.5f, 0.5f);
    [SerializeField] Color from = Color.white;
    [SerializeField] Color to = Color.black;
    [SerializeField] int numberColor = 15;
    [SerializeField] int startXMin;
    [SerializeField] int startXMax;
    [SerializeField] int highMin;
    [SerializeField] int highMax;
    [SerializeField] float timeFade = 3;
   
    int startColor = 0;
    int startOrder = 100;
    int startLine = 0;
    Color rateColor = Color.white;
    Direction lastDirect = Direction.LEFT;

    public List<Stair> stairs = new List<Stair>();

    public enum Direction { LEFT, RIGHT }

    void Start()
    {
        rateColor = (to - from) / numberColor;
        int numberStair = 15;
        for(int i = 0; i < numberStair; i++)
        {
            int startX = Random.Range(startXMin, startXMax);
            int high = Random.Range(highMin, highMax);
            Direction direction = i % 2 == 0 ? Direction.LEFT : Direction.RIGHT;
            lastDirect = direction;
            var stair = GenerateStairs(direction, startLine, startX, high, startColor, startOrder, distance);

            startColor++;
            startOrder--;
            startLine += high;
        }
    }

    public void UpdateColor(int center)
    {
        for (int i = 0; i < stairs.Count; i++)
        {
            Stair stair = stairs[i];
            Color curStair = stair.stairList[0].GetComponent<SpriteRenderer>().color;
            Color curWall = stair.wall.GetComponent<SpriteRenderer>().color;

            if (i < center) stair.SetColor(changeColor(curStair, 1), changeColor(curWall, 1), timeFade);
            else stair.SetColor(changeColor(curStair, -1), changeColor(curWall, -1), timeFade);
        }
    }

    public Stair AddStair()
    {
        int startX = Random.Range(startXMin, startXMax);
        int high = Random.Range(highMin, highMax);
        lastDirect = lastDirect == Direction.LEFT ? Direction.RIGHT : Direction.LEFT;
        var stair = GenerateStairs(lastDirect, startLine, startX, high, startColor, startOrder, distance);
        startOrder--;
        startLine += high;
        return stair;
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
        wall.transform.localScale += new Vector3(0, (high - 1) * wall.transform.localScale.y, 0);
        stair.wall = wall;

        stair.SetEnableColliderStair(false);
        stair.SetEnableColliderWall(false);
        stairs.Add(stair);

        return stair;
    }

    GameObject SpawnSquare(Direction from, Vector2 position, Color color, int order, Vector2 rate)
    {
        var obj = Instantiate(square, transform);
        obj.transform.position = new Vector2(position.x * rate.x, position.y * rate.y);

        var sprite = obj.GetComponent<SpriteRenderer>();
        //sprite.flipX = from == Direction.LEFT ? true : false;
        sprite.color = color;
        sprite.sortingOrder = order;

        obj.transform.localScale = obj.transform.localScale * new Vector2(from == Direction.LEFT ? -1 : 1, 1);

        //var boxC2 = obj.GetComponent<BoxCollider2D>();
        //Vector2 newOffset = boxC2.offset;
        //newOffset.x *= from == Direction.LEFT ? -1 : 1;
        //boxC2.offset = newOffset;

        return obj;
    }

    Color GetColor(int index)
    {
        Color newColor = rateColor * index + from;
        return newColor;
    }

    Color changeColor(Color current, int direction)
    {
        return current + direction * rateColor;
    }
}
