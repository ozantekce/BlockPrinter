using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{


    [SerializeField]
    private Material _material;


    private static Printer instance;

    public static Printer Instance { get => instance;}

    private void Awake()
    {
        instance = this;
    }

    public void Print(Sprite sprite, Vector3 position, int simpler, PrimitiveType primitiveType , bool oneMesh)
    {
        GameObject parent = new GameObject(sprite.name);

        simpler = Mathf.Clamp(simpler,1,256);


        int w = (int)sprite.rect.width;
        int h = (int)sprite.rect.height;
        parent.transform.position = new Vector3(w / 2, h / 2, 0f);
        Texture2D temp = sprite.texture;

        for (int i = 0; i < w; i++)
        {

            for (int j = 0; j < h; j++)
            {

                Color32 c = temp.GetPixel(i, j);
                if (c.a == 0)
                {
                    continue;
                }

                c.r = (byte)((c.r/ simpler) * simpler);
                c.b = (byte)((c.b/ simpler) * simpler);
                c.g = (byte)((c.g/ simpler) * simpler);

                GameObject go = GameObject.CreatePrimitive(primitiveType);
                go.name = i + "-" + j;
                MeshFilter mf = go.GetComponent<MeshFilter>();
                Color[] colors = new Color[mf.mesh.vertexCount];
                for (int t = 0; t < mf.mesh.vertexCount; t++)
                {
                    colors[t] = c;
                }
                mf.mesh.colors = colors;
                if (!oneMesh)
                {
                    go.transform.position = new Vector3(i, j, 0);
                }
                else
                {
                    go.transform.position = new Vector3(i, j, 0) - position;
                }
                

                go.transform.SetParent(parent.transform);

                if (!oneMesh)
                {
                    go.GetComponent<MeshRenderer>().material = _material;
                }

            }

        }
        parent.transform.position = position;


        if (oneMesh)
        {
            MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
            Debug.Log(meshFilters.Length);
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            int k = 0;
            while (k < meshFilters.Length)
            {
                combine[k].mesh = meshFilters[k].sharedMesh;
                combine[k].transform = meshFilters[k].transform.localToWorldMatrix;
                meshFilters[k].gameObject.SetActive(false);

                k++;
            }
            parent.AddComponent<MeshRenderer>().material = _material;
            MeshFilter mfp = parent.AddComponent<MeshFilter>();
            mfp.mesh = new Mesh();
            mfp.mesh.CombineMeshes(combine);

            foreach (MeshFilter meshFilter in meshFilters)
            {
                DestroyImmediate(meshFilter.gameObject);
            }

        }


    }


}
