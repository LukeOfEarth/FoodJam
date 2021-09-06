using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int hp = 1;
    public GameObject innerLayer;
    public GameObject middleLayer;
    public GameObject outerLayer;

    private GameObject activeLayer;
    private enum Transition
    {
        UP,
        DOWN
    }
    public void Start()
    {
        activeLayer = innerLayer;
    }

    public void pickupFood(FoodController food)
    {
        if (food.isAttached)
        {
            return;
        }

        HandleFood layer = activeLayer.GetComponent<HandleFood>();
        bool addedFood = layer.addFoodToSlot(food);
        
        if (addedFood)
        {
            this.hp += food.hp;
            return;
        }

        bool canAttachMoreFood = switchLayer(Transition.UP);
        if (canAttachMoreFood)
        {
            layer.addFoodToSlot(food);
            this.hp += food.hp;
            return;
        }
        // Decide if this needs to return a bool
    }

    public void dropFood(int count)
    {
        HandleFood attachFood = activeLayer.GetComponent<HandleFood>();
        attachFood.removeFoodFromSlot(count);
    }

    private bool switchLayer(Transition t)
    {
        switch (t)
        {
            case Transition.UP:
                if (activeLayer == innerLayer)
                {
                    activeLayer = middleLayer;
                    return true;
                }

                if (activeLayer == middleLayer)
                {
                    activeLayer = outerLayer;
                    return true;
                }

                return false;
            case Transition.DOWN:
                if (activeLayer == outerLayer)
                {
                    activeLayer = middleLayer;
                    return true;
                }

                if (activeLayer == middleLayer)
                {
                    activeLayer = innerLayer;
                    return true;
                }

                return false;
            default:
                return false;
        }
    }
}