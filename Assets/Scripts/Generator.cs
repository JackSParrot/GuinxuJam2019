using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
#endif
public class Generator : MonoBehaviour
{
    List<GameObject> gos = new List<GameObject>();
    List<List<GameObject>> temps = new List<List<GameObject>>();

    public float size = 10f;
    public int width = 10;
    public float height = 10;

    public GameObject A;

    public GameObject N;
    public GameObject S;
    public GameObject E;
    public GameObject W;

    public GameObject NS;
    public GameObject NE;
    public GameObject NW;
    public GameObject SE;
    public GameObject SW;
    public GameObject EW;

    public GameObject SEW;
    public GameObject NEW;
    public GameObject NSW;
    public GameObject NSE;

    public void Prepare()
    {
        ClearTemps();
        for (int y = 0; y < height; ++y)
        {
            temps.Add(new List<GameObject>());
            for (int x = 0; x < width; ++x)
            {
                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.localScale = new Vector3(size, 0.2f, size);
                go.transform.position = new Vector3(x * size, 0f, y * size);
                temps[y].Add(go);
                go.transform.SetParent(transform, true);
            }
        }
    }

    public void ClearTemps()
    {
        foreach(var gos in temps)
        {
            foreach(var go in gos)
            {
                DestroyImmediate(go);
            }
        }
        temps.Clear();
    }


    public void Generate()
    {
        Clear();
        var prefabs = new Dictionary<string, GameObject>
        {
            {"nsew", A },
            {"n", N },
            {"s", S },
            {"e", E },
            {"w", W },
            {"ns", NS },
            {"ne", NE },
            {"nw", NW },
            {"se", SE },
            {"sw", SW },
            {"ew", EW },
            {"sew", SEW },
            {"new", NEW },
            {"nsw", NSW },
            {"nse", NSE }
        };

        for(int y = 0; y < height; ++y)
        {
            for(int x = 0; x < width; ++x)
            {
                if (!temps[y][x].activeSelf) continue;
                string gon = "";
                if (y < height - 1 && temps[y+1][x].activeSelf) gon += "n";
                if (y > 0 && temps[y-1][x].activeSelf) gon += "s";
                if (x < width - 1 && temps[y][x+1].activeSelf) gon += "e";
                if (x > 0 && temps[y][x-1].activeSelf) gon += "w";
                if (gon != "")
                {

#if UNITY_EDITOR
                    var go = PrefabUtility.InstantiatePrefab(prefabs[gon]) as GameObject;
                    go.transform.position = temps[y][x].transform.position;
                    go.transform.SetParent(transform, true);
                    gos.Add(go);
#endif
                }
            }
        }
        ClearTemps();
    }

    public void Clear()
    {
        foreach (var go in gos)
        {
            DestroyImmediate(go);
        }
        gos.Clear();
    }

    public void FullClear()
    {
        Clear();
        for(int i = 0; i < transform.childCount; ++i)
        {
            gos.Add(transform.GetChild(i).gameObject);
        }
        Clear();
        temps.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}