using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum player {
        player_1,
        player_2
    }

    #region Player Variables
    public player thisPlayer;
    public float moveSpeed;
    #endregion

    #region Player Components
    Rigidbody rb;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            rb.velocity = Vector3.Lerp(rb.velocity ,new Vector3(x * moveSpeed, 0,z * moveSpeed) , 0.5f);
        }
        #endregion

        #region Controls for player 2
        //Player 2
        if (thisPlayer == player.player_2)
        {
            float x = Input.GetAxis("HorizontalPlayer_2");
            float z = Input.GetAxis("VerticalPlayer_2");
            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(x * moveSpeed, 0, z * moveSpeed), 0.5f);
        }
        #endregion
    }
}
