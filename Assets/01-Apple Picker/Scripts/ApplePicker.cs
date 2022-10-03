using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]

    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    public TextMeshProUGUI level;

    // Start is called before the first frame update
    void Start()
    {
        basketList = new List<GameObject>();

        // find reference to level GameObject
        GameObject levels = GameObject.Find("level");
        // get text component of that GameObject
        level = levels.GetComponent<TextMeshProUGUI>();
        updateLevelText();

        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public void AppleDestroyed()
    {
        // destroy all of the falling apples
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tG0 in tAppleArray)
        {
            Destroy(tG0);
        }

        // destroy one of the baskets
        // get the index of the last basket in basketlist
        int basketIndex = basketList.Count - 1;

        // get a reference to that Basket GameObject
        GameObject tBasketGO = basketList[basketIndex];

        // remove the basket from the list and destroy the GameObject
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        // if there are no baskets left, load the Game Over screen
        if (basketList.Count == 0)
        {
            AppleTree.level = 0;
            LoadApplePicker.GameOver();
        }
    }

    public void updateLevelText()
    {
        level.text = "LEVEL " + (AppleTree.level + 1).ToString();
        Invoke("RemoveLevelText", 2f);
    }
    
    private void RemoveLevelText()
    {
        level.text = "";
    }
}
