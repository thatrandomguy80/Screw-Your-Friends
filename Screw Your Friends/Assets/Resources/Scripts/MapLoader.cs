using UnityEngine;
using System.Collections;
using System.IO;

[RequireComponent(typeof(GameState))]
public class MapLoader : MonoBehaviour
{

    enum t { BASIC = 0, DEST = 1, COIN = 2, SPAWN = 3, CHECK = 4, GOAL = 5 };
    [Header("Loadable Tiles or objects")]
    public GameObject[] tiles;
    [Header("Load this map from Maps file")]
    public string[] mapNames;
    [Header("offsets map scale")]
    public float xScale = 1f;
    public float yScale = 1f;

    private Texture2D tex;
    private WWW w;
    private GameObject map;

    public void nextMap(int index)
    {
        if (index > mapNames.Length - 1) Debug.Log("Out Of Maps");
        if (map != null) Destroy(map);
        map = new GameObject();
        map.name = "Map";
        tex = new Texture2D(20, 20);
        loadImg(index);
    }

    private void buildMap()
    {
        Debug.Log("map building");
        Color32[] pix = tex.GetPixels32();

        this.transform.GetComponent<GameState>().MapDim(tex.height);
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                Color32 tmp = pix[x + (tex.width * y)];
                int item = 999;
                if (tmp.a != 0)
                {
                    if (tmp.b == 0) item = (int)t.BASIC;
                    else if (tmp.b == 50) item = (int)t.DEST;
                    else if (tmp.b == 100) item = (int)t.COIN;
                    else if (tmp.b == 150) item = (int)t.SPAWN;
                    else if (tmp.b == 200) item = (int)t.CHECK;
                    else if (tmp.b == 250) item = (int)t.GOAL;
                    else if (tmp.b != 255) Debug.Log(tmp.b);

                    if (item != 999)
                    {
                        GameObject obj = Instantiate(tiles[item], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                        obj.transform.localScale = new Vector3(xScale, yScale, 1f);
                        if (item != (int)t.SPAWN)
                            obj.transform.SetParent(map.transform);
                    }
                }
            }
        }
    }


    private void loadImg(int index)
    {
        //gen filename
        string fileName = Application.dataPath + "/Resources/Maps/" + mapNames[index] + ".png";
        byte[] bytes = File.ReadAllBytes(fileName);
        tex.LoadImage(bytes);
        buildMap();
    }
}
