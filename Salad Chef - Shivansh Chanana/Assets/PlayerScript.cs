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
    [SerializeField]
    bool hasTakenChopperItem;
    [SerializeField]
    bool canSendCombination;
    [SerializeField]
    int currentCustomer;

    float x;
    float z;
    UiManager uiManager;
    ChopperScript lastChopper;
    PeopleManager peopleManager;
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
        //get people Manager
        peopleManager = FindObjectOfType<PeopleManager>();
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
            if (Input.GetKeyDown(KeyCode.F) && canPressButton){
                if (canSendCombination && hasTakenChopperItem)
                {
                    peopleManager.CheckCombination(1, currentItems);
                    currentItems.Clear();
                    uiManager.RemoveItem(1, 0);
                    hasTakenChopperItem = false;
                    canSendCombination = false;
                }
                else GetCurrentPickupSpot(collidername);
            }
        } 
        #endregion

        #region Controls for player 2
        //Player 2
        if (thisPlayer == player.player_2)
        {
            x = Input.GetAxis("HorizontalPlayer_2");
            z = Input.GetAxis("VerticalPlayer_2");
            if (Input.GetKeyDown(KeyCode.Comma) && canPressButton) {
                if (canSendCombination && hasTakenChopperItem)
                {
                    peopleManager.CheckCombination(currentCustomer, currentItems);
                    currentItems.Clear();
                    uiManager.RemoveItem(2, 0);
                    hasTakenChopperItem = false;
                    canSendCombination = false;
                }
                else GetCurrentPickupSpot(collidername);
            }
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
        if (currentItems.Count < 2 && currentPickupSpot != allPickupSpot.trash && currentPickupSpot != allPickupSpot.chopper &&
            currentPickupSpot != allPickupSpot.none && currentPickupSpot != allPickupSpot.plate && !hasTakenChopperItem)
        {
            currentItems.Add(pickupSpotName);
            uiManager.AddItem((int)thisPlayer + 1, pickupSpotName);
        }
        else
        {
            //Trash Can selected
            if (currentPickupSpot == allPickupSpot.trash)
            {
                currentItems.Clear();
                uiManager.RemoveItem((int)thisPlayer + 1);

                hasTakenChopperItem = false;

                Debug.Log("MINUS POINTS");
            }

            //Chopper selected
            if (currentPickupSpot == allPickupSpot.chopper)
            {
                //Has items to put in chopper
                if (lastChopper.currentItems.Count < 3 && currentItems.Count > 0) StartCoroutine(StartChopperTimer());
                else if (currentItems.Count == 0) {
                    //get items from chopper
                    for (int i = 0; i < lastChopper.currentItems.Count;i++) {
                        currentItems.Add(lastChopper.currentItems[i]);
                        uiManager.AddItem((int)thisPlayer + 1, lastChopper.currentItems[i]);
                    }
                    lastChopper.currentItems.Clear();
                    uiManager.RemoveItemFromChopper(lastChopperNumber);
                    hasTakenChopperItem = true;
                }
                else Debug.Log("CHOPPER FILLED");
            }

            //Error inventory full
            if (currentItems.Count >= 2 && currentPickupSpot != allPickupSpot.trash && currentPickupSpot != allPickupSpot.chopper &&
                currentPickupSpot != allPickupSpot.none && currentPickupSpot != allPickupSpot.plate && !hasTakenChopperItem)
                Debug.Log("ALREADY HAS 2 ITEMS");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        canPressButton = true;
        collidername = other.name;

        if (other.CompareTag("Customer"))
        {
            if (other.name == "customer_1")
            {
                canSendCombination = true;
                currentCustomer = 1;
            } else if (other.name == "customer_2") {
                canSendCombination = true;
                currentCustomer = 2;
            } else if (other.name == "customer_3")
            {
                canSendCombination = true;
                currentCustomer = 3;
            }

            return;
        }
        else {
            canSendCombination = false;
        }

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
        canSendCombination = false;
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
        uiManager.AddItemInChopper(lastChopperNumber,currentItems[0]);
        currentItems.RemoveAt(0);
        uiManager.RemoveItem((int)thisPlayer + 1,1);
        yield return null;
    }



}
