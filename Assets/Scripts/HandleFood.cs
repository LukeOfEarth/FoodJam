using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandleFood : MonoBehaviour
{
    public int filledSlots = 0;
    public GameObject[] slots;
    public GameObject[] f;
    private PlayerState playerState;

    private void Start()
    {
        filledSlots = 0;
        playerState = GetComponentInParent<PlayerState>();
    }

    public bool addFoodToSlot(FoodController food)
    {
        if (filledSlots == slots.Length)
        {
            return false;
        }

        foreach (GameObject slot in slots)
        {
            if (slot.GetComponentInChildren<FoodController>() == null)
            {
                food.isAttached = true;
                food.Rotate(slot);
                filledSlots++;
                break;
            }
        }

        if (filledSlots == slots.Length)
        {
            playerState.goUpLayer();
        }

        f = getFilledSlots();

        return true;
    }

    public void removeFoodFromSlot(int count)
    {
        for(int i=0; i<count; i++)
        {
            FoodController food = slots[filledSlots - 1].GetComponentInChildren<FoodController>();
            if(food)
            {
                Destroy(food.gameObject);
                filledSlots--;
            }
        }

        if(filledSlots == 0)
        {
            playerState.goDownLayer();
        }
    }

    public GameObject[] getFilledSlots()
    {
        GameObject[] filled = slots.Where(s => s.GetComponentInChildren<FoodController>() != null).ToArray();
        return filled;
    }

    public GameObject useFoodAsAmmo()
    {
        GameObject ammo = slots[filledSlots - 1].GetComponentInChildren<FoodController>().gameObject;
        return ammo;
    }
}
