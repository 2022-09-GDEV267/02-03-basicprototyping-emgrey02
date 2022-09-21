using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public GameObject treePrefab;
    public int numTrees = 50;
    public Vector3 treePosMin = new Vector3(-10, -10, 20);
    public Vector3 treePosMax = new Vector3(50, -10, 100);
    public float treeScaleMin = .2f; 
    public float treeScaleMax = 3; 

    private GameObject[] trees;
    void Start()
    {
        createTrees();
    }

    // Update is called once per frame
    public void createTrees()
    {
        trees = new GameObject[numTrees];

        GameObject tree;

        for (int i = 0; i < numTrees; i++)
        {
            tree = Instantiate<GameObject>(treePrefab);

            // position of building
            Vector3 bPos = Vector3.zero;
            bPos.x = Random.Range(treePosMin.x, treePosMax.x);
            bPos.y = treePosMin.y;

            // scale building
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(treeScaleMin, treeScaleMax, scaleU);

            // smaller buildings should be farther away
            bPos.z = 100 - 90 * scaleU;

            // apply these transforms to the building
            tree.transform.localScale = Vector3.one * scaleVal;

            tree.transform.position = bPos;
            trees[i] = tree;
        }
    }

    public void clearBuildings()
    {
        for (int i = 0; i < numTrees; i++)
        {
            Destroy(trees[i]);
        }
    }
}
