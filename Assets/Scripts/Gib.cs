using UnityEngine;

public class Gib : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }
}
