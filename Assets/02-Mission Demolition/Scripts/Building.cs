using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject buildingPrefab;
    public int numBuildings = 5;
    public Vector3 buildingPosMin = new Vector3(-10, -10, 20);
    public Vector3 buildingPosMax = new Vector3(50, -10, 100);
    public float buildingScaleMin = .2f; // min scale for the clouds
    public float buildingScaleMax = 1; // max scale for the clouds

    private GameObject[] buildings;

    // Start is called before the first frame update
    void Start()
    {
        createBuildings();
    }

    // Update is called once per frame
    public void createBuildings()
    {
        buildings = new GameObject[numBuildings];

        GameObject building;

        for (int i = 0; i < numBuildings; i++)
        {
            building = Instantiate<GameObject>(buildingPrefab);
            GameObject cube1 = building.transform.GetChild(0).gameObject;
            GameObject cube2 = building.transform.GetChild(1).gameObject;

            // position of building
            Vector3 bPos = Vector3.zero;
            bPos.x = Random.Range(buildingPosMin.x, buildingPosMax.x);
            bPos.y = buildingPosMin.y;

            // scale building
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(buildingScaleMin, buildingScaleMax, scaleU);

            // smaller buildings should be farther away
            bPos.z = 100 - 90 * scaleU;

            // apply these transforms to the building
            building.transform.localScale = Vector3.one * scaleVal;

            //assign building a random color
            Renderer cubeRenderer1 = cube1.GetComponent<Renderer>();
            Renderer cubeRenderer2 = cube2.GetComponent<Renderer>();
            Color customColor = new Color(Random.value, Random.value, Random.value);
            cubeRenderer1.material.SetColor("_Color", customColor);
            cubeRenderer2.material.SetColor("_Color", customColor);

            building.transform.position = bPos;
            buildings[i] = building;
        }
    }

    public void clearBuildings()
    {
        for (int i = 0; i < numBuildings; i++)
        {
            Destroy(buildings[i]);
        }
    }
}
