using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    public GameObject node;
    void Start()
    {

    }

    public void End()
    {
        
        Destroy(gameObject);
    }

    public void DestroyNode()
    {
        // if(node)Destroy(node);
    }
}
