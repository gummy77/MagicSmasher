using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private bool spawnOnGround;

    [Header("Burst Spawning")]
    [SerializeField] private float bspawnCount;
    [SerializeField] private float bspawnDistance;

    [Header("Continuous Spawning")]
    [SerializeField] private float cspawnCount;
    [SerializeField] private float cspawnDelay;
    [SerializeField] private float cspawnDistance;

    private float cTimer;

    void Start() {
        for(int i = 0; i < bspawnCount; i++) {
            Vector3 pos = transform.position + transform.forward + new Vector3(Random.Range(-bspawnDistance, bspawnDistance), 0, Random.Range(-bspawnDistance, bspawnDistance));
            if(spawnOnGround) {
                RaycastHit hit;
                if (Physics.Raycast(pos, -Vector3.up, out hit)) {
                    pos = hit.point;
                } else {
                    continue;
                }
                
            }
            Instantiate(prefab, pos, transform.rotation, this.transform);
        }
    }

    void Update()
    {
        cTimer += Time.deltaTime;

        if(cTimer >= cspawnDelay) {
            cTimer = 0;
            for(int i = 0; i < cspawnCount; i++) {
                Vector3 pos = transform.position + transform.forward + new Vector3(Random.Range(-cspawnDistance, cspawnDistance), 0, Random.Range(-cspawnDistance, cspawnDistance));
                Instantiate(prefab, pos, transform.rotation, this.transform);
            }
        }
    }
}
