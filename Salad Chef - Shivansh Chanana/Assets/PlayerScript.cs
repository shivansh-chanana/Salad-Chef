using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum player {
        player_1,
        player_2
    }
    public enum allPickupSpot {
        none,
        cucumber,
        eggplant,
        pumpkin,
        tomato,
        whiteRadish,
        paprika,
        trash,
        chopper,
        plate
    }

    #region Player Variables
    public player thisPlayer;
    public float moveSpeed;
    public List<string> currentItems;
    [Space]
    [SerializeField]
    allPickupSpot currentPickupSpot;
    [SerializeField]
    string collidername;
    [SerializeField]
    bool canPressButton;

    float x;
    float z;
    UiManager uiManager;
    #endregion

    #region Player Components
    Rigidbody rb;
    #endregion

    void Start()
    {
        //get Rigidbody component
        rb = GetComponent<Rigidbody>();
        //get UI Manager
        uiManager = FindObjectOfType<UiManager>();

        //Set tag according to player
        #region Set tag
        if (thisPlayer == player.player_1) tag = "Player_1";
        else tag = "Player_2";
        #endregion
    }

    void Update()
    {
        //Controls
        PlayerControls();
    }

    void PlayerControls() {

        #region Controls for player 1
        //Player 1
        if (thisPlayer == player.player_1) {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.F) && canPressButton) GetCurrentPickupSpot(collidername);
        }
        #endregion

        #region Controls for player 2
        //Player 2
        if (thisPlayer == player.player_2)
        {
            x = Input.GetAxis("HorizontalPlayer_2");
            z = Input.GetAxis("VerticalPlayer_2");
            if(Input.GetKeyDown(KeyCode.Comma) && canPressButton) GetCurrentPickupSpot(collidername);
        }
        #endregion

        rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(x * moveSpeed, 0, z * moveSpeed), 0.5f);
    }

    void GetCurrentPickupSpot(string pickupSpotName) {
        switch (pickupSpotName) {

            case "cucumber":
                currentPickupSpot = allPickupSpot.cucumber;
                break;
            case "eggplant":
                currentPickupSpot = allPickupSpot.eggplant;
                break;
            case "pumpkin":
                currentPickupSpot = allPickupSpot.pumpkin;
                break;
            case "tomato":
                currentPickupSpot = allPickupSpot.tomato;
                break;
            case "whiteRadish":
                currentPickupSpot = allPickupSpot.whiteRadish;
                break;
            case "paprika":
                currentPickupSpot = allPickupSpot.paprika;
                break;
            case "trash":
                currentPickupSpot = allPickupSpot.trash;
                break;
            case "chopper":
                currentPickupSpot = allPickupSpot.chopper;
                break;
            case "plate":
                currentPickupSpot = allPickupSpot.plate;
                break;
            default:
                currentPickupSpot = allPickupSpot.none;
                break;
        }

        //Add item to list
        if (currentItems.Count < 2 && currentPickupSpot != allPickupSpot.trash && currentPickupSpot != allPickupSpot.chopper && currentPickupSpot != allPickupSpot.none && currentPickupSpot != allPickupSpot.plate)
        {
            currentItems.Add(pickupSpotName);
            uiManager.AddItem((int)thisPlayer + 1);
        }
        else
        {
            //Trash can selected
            if (currentPickupSpot == allPickupSpot.trash)
            {
                currentItems.Clear();
                uiManager.RemoveItem((int)thisPlayer + 1);
            }

            //Error inventory full
            if (currentItems.Count >= 2)Debug.Log("ALREADY HAS 2 ITEMS");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        canPressButton = true;
        collidername = other.name;
    }

    private void OnTriggerExit(Collider other)
    {
        canPressButton = false;
    }
}
