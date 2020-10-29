using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyliderGenerate : MonoBehaviour
{
    public MeshFilter mf;
    public Mesh mesh;

    public GameObject cube;

    int num;

    public float iter;
    public float leng;
    public float radius;

    Vector3[] vertices;
    int[] tris;
    int[] FinalTri;
    int[] firstplane;
    public void GenerateStart()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        mesh = new Mesh();
        MakingVertices(radius, iter, leng, 1.0f, 0.1f);
    }

    void MakingVertices(float radius, float iterations, float lenggth, float gap, float noise)
    {
        float noise_x;
        float noise_y;
        float noise_z;
        float x;
        float z;
        float y = 0;
        int i;
        int p = 0;
        float angle;

        vertices = new Vector3[(Mathf.CeilToInt(iterations) * Mathf.CeilToInt(lenggth)) + 2];
        int tempo = 0;
        vertices[vertices.Length - 2] = Vector3.zero;

        while (p < Mathf.CeilToInt(lenggth))
        {
            i = 0;
            while (i < Mathf.CeilToInt(iterations))
            {
                angle = (i * 1.0f) / Mathf.CeilToInt(iterations) * Mathf.PI * 2;
                x = Mathf.Sin(angle) * radius;
                z = Mathf.Cos(angle) * radius;
                vertices[tempo] = new Vector3(x, y, z);
                //GameObject go = Instantiate(cube, vertices[tempo], Quaternion.identity);
                //go.transform.GetChild(1).GetComponent<TextMesh>().text = num.ToString();
                //go.name = num.ToString();
                i++;
                num++;
                tempo += 1;
            }
            y += gap;
            p++;
        }


        vertices[vertices.Length - 1] = new Vector3(0, vertices[vertices.Length - 3].y, 0);
        Debug.Log("Vertices: " + num);
        mesh.vertices = vertices;
        MakingNormals();
    }
    void MakingNormals()
    {
        int i = 0;
        Vector3[] normals = new Vector3[num + 2];
        while (i < num+2)
        {
            normals[i] = Vector3.forward;
            i++;
        }
        mesh.normals = normals;

        Debug.Log(" normals");
        MakingTrianges();
    }
    void MakingTrianges()
    {
        Debug.Log(" MakingTrianges");
        int i = 0;
        tris = new int[((3 * (Mathf.CeilToInt(leng) - 1) * Mathf.CeilToInt(iter)) * 2) + 3];
        while (i < (Mathf.CeilToInt(leng) - 1) * Mathf.CeilToInt(iter))
        {
            tris[i * 3] = i;
            if ((i + 1) % Mathf.CeilToInt(iter) == 0)
            {
                tris[i * 3 + 1] = 1 + i - Mathf.CeilToInt(iter);
            }
            else
            {
                tris[i * 3 + 1] = 1 + i;
            }
            tris[i * 3 + 2] = Mathf.CeilToInt(iter) + i;
            i++;
        }
        int IndexofNewTriangles = -1;

        for (int u = (tris.Length - 3) / 2; u < tris.Length - 6; u += 3)
        {
            //mesh.RecalculateTangents();
            if ((IndexofNewTriangles + 2) % Mathf.CeilToInt(iter) == 0)
            {
                tris[u] = IndexofNewTriangles + Mathf.CeilToInt(iter) * 2 + 1;
            }
            else
                tris[u] = IndexofNewTriangles + Mathf.CeilToInt(iter) + 1;

            tris[u + 1] = IndexofNewTriangles + 2;
            tris[u + 2] = IndexofNewTriangles + Mathf.CeilToInt(iter) + 2;
            IndexofNewTriangles += 1;
        }
        tris[tris.Length - 3] = 0;
        tris[tris.Length - 2] = (Mathf.CeilToInt(iter) * 2) - 1;
        tris[tris.Length - 1] = Mathf.CeilToInt(iter);

        firstplane = new int[(Mathf.CeilToInt(iter) * 3) * 2];
        int felmnt = 0;
        for (int h = 0; h < firstplane.Length / 2; h += 3)
        {

            firstplane[h] = felmnt;

            if (felmnt + 1 != Mathf.CeilToInt(iter))
                firstplane[h + 1] = felmnt + 1;
            else
                firstplane[h + 1] = 0;
            firstplane[h + 2] = vertices.Length - 2;
            felmnt += 1;
        }

        felmnt = Mathf.CeilToInt(iter) * (Mathf.CeilToInt(leng) - 1);
        for (int h = firstplane.Length / 2; h < firstplane.Length; h += 3)
        {

            firstplane[h] = felmnt;

            if (felmnt + 1 != Mathf.CeilToInt(iter) * (Mathf.CeilToInt(leng) - 1))
                firstplane[h + 1] = felmnt + 1;
            else
                firstplane[h + 1] = Mathf.CeilToInt(iter) * (Mathf.CeilToInt(leng) - 1);
            firstplane[h + 2] = vertices.Length - 1;
            felmnt += 1;
        }

        firstplane[firstplane.Length - 3] = Mathf.CeilToInt(iter) * (Mathf.CeilToInt(leng) - 1);
        firstplane[firstplane.Length - 2] = vertices.Length - 3;
        firstplane[firstplane.Length - 1] = vertices.Length - 1;

        FinalTri = new int[tris.Length + firstplane.Length];

        int k = 0, l = 0;
        for (k = 0, l = 0; k < tris.Length; k++)
        {
            FinalTri[l++] = tris[k];
        }
        for (k = 0; k < firstplane.Length; k++)
        {
            FinalTri[l++] = firstplane[k];
        }

        mesh.triangles = FinalTri;
        mesh.Optimize();
        mesh.RecalculateNormals();
        mf.mesh = mesh;
    }
}