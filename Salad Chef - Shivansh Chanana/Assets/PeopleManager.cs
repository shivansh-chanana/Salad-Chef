using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    public UiManager uiManager;

    public List<string> customer_1;
    public List<string> customer_2;
    public List<string> customer_3;

    string vegetableName;

    void Start()
    {
        AssignToCustomer(1);
        AssignToCustomer(2);
        AssignToCustomer(3);

    }

    void AssignToCustomer(int customerNum = 1) {

        int randomCombinationCount = Random.Range(2, 4);

        for (int i = 0; i < randomCombinationCount; i++) {
            string curVeg = GetRandomItem();

            if (customerNum == 1)
            {
                customer_1.Add(curVeg);
                uiManager.AddItemInCustomer(1,curVeg);
            }
            if (customerNum == 2)
            {
                customer_2.Add(curVeg);
                uiManager.AddItemInCustomer(2, curVeg);
            }
            if (customerNum == 3)
            {
                customer_3.Add(curVeg);
                uiManager.AddItemInCustomer(3, curVeg);
            }
        }
    }

    public void CheckCombination(int customerNum , List<string> currentCombination) {

        if (customerNum == 1)
        {
            for (int i = 0; i < currentCombination.Count; i++) {
                if (customer_1.Contains(currentCombination[i]))
                {
                    Debug.Log("CONTAINS");
                }
                else
                {
                    Debug.Log("NOT CONTAINS");
                }
            }
            uiManager.RemoveItemFromCustomer(1);
            AssignToCustomer(1);
        }

        if (customerNum == 2)
        {
            for (int i = 0; i < currentCombination.Count; i++)
            {
                if (customer_2.Contains(currentCombination[i]))
                {
                    Debug.Log("CONTAINS");
                }
                else
                {
                    Debug.Log("NOT CONTAINS");
                }
            }
            uiManager.RemoveItemFromCustomer(2);
            AssignToCustomer(2);
        }

        if (customerNum == 3)
        {
            for (int i = 0; i < currentCombination.Count; i++)
            {
                if (customer_3.Contains(currentCombination[i]))
                {
                    Debug.Log("CONTAINS");
                }
                else
                {
                    Debug.Log("NOT CONTAINS");
                }
            }
            uiManager.RemoveItemFromCustomer(3);
            AssignToCustomer(3);
        }
    }

    string GetRandomItem() {
        switch (Random.Range(0,5)) {
            case 0:
                vegetableName = "cucumber";
                break;
            case 1:
                vegetableName = "eggplant";
                break;
            case 2:
                vegetableName = "pumpkin";
                break;
            case 3:
                vegetableName = "tomato";
                break;
            case 4:
                vegetableName = "whiteRadish";
                break;
            case 5:
                vegetableName = "paprika";
                break;
        }
        return vegetableName;
    }

}
