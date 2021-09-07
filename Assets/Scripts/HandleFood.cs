using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleFood : MonoBehaviour
{
    public int filledSlots = 0;
    public GameObject[] slots;

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

        return true;
    }

    public int removeFoodFromSlot(int count)
    {
        int tracker = 0;
        foreach (GameObject slot in slots)
        {
            if (slot.GetComponentInChildren<FoodController>() != null)
            {
                Destroy(slot.gameObject);
                filledSlots--;
                tracker++;
            }

            if (tracker == count)
            {
                return tracker;
            }
        }

        return count - tracker;
    }
}
