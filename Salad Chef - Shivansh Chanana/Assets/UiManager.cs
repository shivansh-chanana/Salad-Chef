using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Transform hudPlayer_1, hudPlayer_2;
    public Transform hudChopper_1, hudChopper_2;
    public Transform hudCustomer_1, hudCustomer_2, hudCustomer_3;
    public Transform chopper_1, chopper_2;
    public Vector3 hudOffset;
    public GameObject imagePrefab;
    public Sprite[] imgSprite;
    [Space]
    [SerializeField]
    Transform player_1, player_2;
    Camera mainCam;

    void Start()
    {
        //Assign Variables
        player_1 = GameObject.FindWithTag("Player_1").transform;
        player_2 = GameObject.FindWithTag("Player_2").transform;
        mainCam = FindObjectOfType<Camera>();

        ChopperHudPosition();
    }

    void ChopperHudPosition()
    {
        hudChopper_1.transform.position = mainCam.WorldToScreenPoint(chopper_1.transform.position) + hudOffset + new Vector3(0, 40, 0);
        hudChopper_2.transform.position = mainCam.WorldToScreenPoint(chopper_2.transform.position) + hudOffset + new Vector3(0, 40, 0);
    }

    void Update()
    {
        #region Update HUD position
        hudPlayer_1.transform.position = mainCam.WorldToScreenPoint(player_1.transform.position) + hudOffset;
        hudPlayer_2.transform.position = mainCam.WorldToScreenPoint(player_2.transform.position) + hudOffset;
        #endregion
    }

    //Add Item in HUD PLAYER parent to show on screen
    public void AddItem(int playerNum = 1, string vegetableName = "null")
    {
        if (playerNum == 1)
        {
            Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, hudPlayer_1.transform).GetComponent<Image>().sprite = GetItemImage(vegetableName);
        }
        else
        {
            Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, hudPlayer_2.transform).GetComponent<Image>().sprite = GetItemImage(vegetableName);
        }
    }
    //Remove Item from HUD PLAYER parent
    public void RemoveItem(int playerNum = 1, int numOfItemsToRemove = 0)
    {
        if (playerNum == 1)
        {
            if (numOfItemsToRemove == 0)
            {
                foreach (Transform child in hudPlayer_1)
                {
                    Destroy(child.gameObject);
                }
            }
            else
            {
                for (int i = 0; i < numOfItemsToRemove; i++)
                {
                    Destroy(hudPlayer_1.GetChild(i).gameObject);
                }
            }
        }
        else
        {
            if (numOfItemsToRemove == 0)
            {
                foreach (Transform child in hudPlayer_2)
                {
                    Destroy(child.gameObject);
                }
            }
            else
            {
                for (int i = 0; i < numOfItemsToRemove; i++)
                {
                    Destroy(hudPlayer_2.GetChild(i).gameObject);
                }
            }
        }
    }

    public void AddItemInChopper(int chopperNum, string vegetableName)
    {
        if (chopperNum == 1)
        {
            Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, hudChopper_1.transform).GetComponent<Image>().sprite = GetItemImage(vegetableName);
        }
        else
        {
            Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, hudChopper_2.transform).GetComponent<Image>().sprite = GetItemImage(vegetableName);
        }
    }

    public void RemoveItemFromChopper(int chopperNum)
    {
        if (chopperNum == 1)
        {
            foreach (Transform child in hudChopper_1)
            {
                Destroy(child.gameObject);
            }
        }
        else
        {
            foreach (Transform child in hudChopper_2)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void AddItemInCustomer(int customerNum = 1,string vegetableName = "null") {

        if (customerNum == 1)
        {
            Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, hudCustomer_1.transform).GetComponent<Image>().sprite = GetItemImage(vegetableName);
        }

        if (customerNum == 2)
        {
            Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, hudCustomer_2.transform).GetComponent<Image>().sprite = GetItemImage(vegetableName);
        }

        if (customerNum == 3)
        {
            Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, hudCustomer_3.transform).GetComponent<Image>().sprite = GetItemImage(vegetableName);
        }
    }

    public void RemoveItemFromCustomer(int customerNum = 1)
    {
        if (customerNum == 1)
        {
            foreach (Transform child in hudCustomer_1)
            {
                Destroy(child.gameObject);
            }
        }

        if (customerNum == 2)
        {
            foreach (Transform child in hudCustomer_2)
            {
                Destroy(child.gameObject);
            }
        }

        if (customerNum == 3)
        {
            foreach (Transform child in hudCustomer_3)
            {
                Destroy(child.gameObject);
            }
        }
    }

    Sprite GetItemImage(string pickupSpotName)
    {
        switch (pickupSpotName)
        {
            case "cucumber":
                return imgSprite[0];
            case "eggplant":
                return imgSprite[1];
            case "pumpkin":
                return imgSprite[2];
            case "tomato":
                return imgSprite[3];
            case "whiteRadish":
                return imgSprite[4];
            case "paprika":
                return imgSprite[5];
        }
        return null;
    }
}
