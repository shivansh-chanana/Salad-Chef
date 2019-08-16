using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public Transform hudPlayer_1, hudPlayer_2;
    public Vector3 hudOffset;
    public GameObject imagePrefab;
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
    }

    // Update is called once per frame
    void Update()
    {
        #region Update HUD position
        hudPlayer_1.transform.position = mainCam.WorldToScreenPoint(player_1.transform.position) + hudOffset;
        hudPlayer_2.transform.position = mainCam.WorldToScreenPoint(player_2.transform.position) + hudOffset;
        #endregion
    }

    //Add Item in HUD PLAYER parent to show on screen
    public void AddItem(int playerNum = 1) {
        if (playerNum == 1)
        {
            Instantiate(imagePrefab,Vector3.zero,Quaternion.identity,hudPlayer_1.transform);
        }
        else {
            Instantiate(imagePrefab, Vector3.zero, Quaternion.identity, hudPlayer_2.transform);
        }
    }
    //Remove Item from HUD PLAYER parent
    public void RemoveItem(int playerNum = 1)
    {
        if (playerNum == 1)
        {
            foreach (Transform child in hudPlayer_1) {
                Destroy(child.gameObject);
            }
        }
        else
        {
            foreach (Transform child in hudPlayer_2)
            {
                Destroy(child.gameObject);
            }
        }
    }

}
