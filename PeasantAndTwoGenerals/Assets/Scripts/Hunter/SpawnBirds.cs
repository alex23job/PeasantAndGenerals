using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBirds : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    private List<GameObject> _birds = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBirds(GameObject prefab, HunterControl hc)
    {
        int numStart = Random.Range(0, _spawnPoints.Length);
        List<Vector3> path = new List<Vector3>();
        Vector3 pos = _spawnPoints[numStart].position;
        Vector3 pt = pos;
        if (pt.x > 0)
        {
            pt.x -= 1f;
            path.Add(pt);
            pt.x = 7.5f;
            path.Add(pt);
            if (pos.z > 0) { pt.z -= 8f; } else { pt.z += 8f; }
            pt.y += 1f;
            path.Add(pt);
            pt.x = -7.5f;
            path.Add(pt);
            if (pos.z > 0) { pt.z += 8f; } else { pt.z -= 8f; }
            path.Add(pt);
            pt.x = -pos.x;
            path.Add(pt);
        }
        else
        {
            pt.x += 1f;
            path.Add(pt);
            pt.x = -7.5f;
            path.Add(pt);
            if (pos.z > 0) { pt.z -= 8f; } else { pt.z += 8f; }
            path.Add(pt);
            pt.x = 7.5f;
            path.Add(pt);
            if (pos.z > 0) { pt.z += 8f; } else { pt.z -= 8f; }
            pt.y += 1f;
            path.Add(pt);
            pt.x = -pos.x;
            pt.y = pos.y;
            path.Add(pt);
        }
        GameObject bird = Instantiate(prefab, pos, Quaternion.identity);
        BirdsControl birdsControl = bird.GetComponent<BirdsControl>();
        if (birdsControl != null) 
        {
            birdsControl.SetPath(path, hc);
        }
        _birds.Add(bird);
    }

    public void ClearBirds()
    {
        for(int i = _birds.Count; i > 0; i--)
        {
            if (_birds[i - 1] != null) Destroy(_birds[i - 1]);
        }
        _birds.Clear();
    }
}
