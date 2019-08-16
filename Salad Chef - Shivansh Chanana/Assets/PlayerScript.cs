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
    public float chopTime;
    public List<string> currentItems;
    [Space]
    [SerializeField]
    allPickupSpot currentPickupSpot;
    [SerializeField]
    string collidername;
    [SerializeField]
    bool canPressButton;
    [SerializeField]
    bool isChopping;
    [SerializeField]
    float curChoppingTime;

    float x;
    float z;
    UiManager uiManager;
    ChopperScript lastChopper;
    int lastChopperNumber;
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
        //If Chopping then dont proceed;
        if (isChopping) return;

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
            case "chopper_1":
            case "chopper_2":
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
            uiManager.AddItem((int)thisPlayer + 1,(int)currentPickupSpot);
        }
        else
        {
            //Trash can selected
            if (currentPickupSpot == allPickupSpot.trash)
            {
                currentItems.Clear();
                uiManager.RemoveItem((int)thisPlayer + 1);
            }

            //Trash can selected
            if (currentPickupSpot == allPickupSpot.chopper && currentItems.Count > 0)
            {
                StartCoroutine(StartChopperTimer());
            }

            //Error inventory full
            if (currentItems.Count >= 2)Debug.Log("ALREADY HAS 2 ITEMS");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        canPressButton = true;
        collidername = other.name;
        if ((other.name == "chopper_1" || other.name == "chopper_2") && lastChopper == null)
        {
            lastChopper = other.GetComponent<ChopperScript>();
            if (other.name == "chopper_1") lastChopperNumber = 1;
            else lastChopperNumber = 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canPressButton = false;
        lastChopper = null;
    }

    IEnumerator StartChopperTimer() {
        curChoppingTime = chopTime;
        isChopping = true;

        do {
            curChoppingTime--;
            yield return new WaitForSeconds(1);
        } while (curChoppingTime > 0);

        isChopping = false;
        lastChopper.currentItems.Add(currentItems[0]);
        uiManager.AddItemInChopper(lastChopperNumber,(int)currentPickupSpot + 1);
        currentItems.RemoveAt(0);
        uiManager.RemoveItem((int)thisPlayer + 1,1);
        yield return null;
    }



}
