using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour {
    [HideInInspector]
    [SerializeField] List<Waypoint> path;
    public GameObject prefab;
    int index = 0;

    public List<GameObject> Points;

    public List<Waypoint> GetPath() {
        if (path == null) path = new List<Waypoint>();
        return path;
    }

    public void CreatePoint() {
        Waypoint point = new Waypoint();
        path.Add(point);
    }

    public Waypoint GetNext() {
        index = (index + 1) % path.Count;
        print(path[index].Index);
        return path[index];
    }

    private void Start() {
        Points = new List<GameObject>();
        int index = 0;
        foreach(Waypoint point in path) {
            GameObject go = Instantiate(prefab);
            go.transform.position = point.Position;
            Points.Add(go);
            go.name = $"{index}";
            point.Index = index;
            index++;
        }
    }

    private void Update() {
        for (int i = 0; i < path.Count; i++) {
            Waypoint point = path[i];
            GameObject go = Points[i];
            go.transform.position = point.Position;
        }
    }
}
