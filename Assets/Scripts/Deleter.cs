using UnityEngine;

public class Deleter : MonoBehaviour
{
    [SerializeField] private float deleteTime = 5.0f;

    void Start()
    {
        Destroy(gameObject, deleteTime);
    }
}
