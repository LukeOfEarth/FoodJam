using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int hp = 1;
    public GameObject innerLayer;
    public GameObject middleLayer;
    public GameObject outerLayer;

    public GameObject activeLayer;
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

    public void dropFood(int count, bool damaged)
    {
        HandleFood attachFood = activeLayer.GetComponent<HandleFood>();
        attachFood.removeFoodFromSlot(count, damaged);
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

    public void goUpLayer()
    {
        switchLayer(Transition.UP);
    }

    public void goDownLayer()
    {
        switchLayer(Transition.DOWN);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (CheckForDeath())
        {
            TriggerDeath();
        } 
        else
        {
            dropFood(amount, true);
        }
    }

    private bool CheckForDeath()
    {
        //Player death logic goes here
        if(hp <= 0)
        {
            print("Death");
            return true;
        }

        return false;
    }

    private void TriggerDeath()
    {
        this.gameObject.GetComponent<PlayerCollisions>().enabled = false;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        this.gameObject.GetComponent<PlayerController>().enabled = false;
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        this.gameObject.GetComponentInChildren<GrapplingGun>().enabled = false;
        this.gameObject.GetComponent<SpringJoint2D>().enabled = false;
        StartCoroutine("Kill");
    }

    IEnumerator Kill()
    {
        StartCoroutine("Flash");
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(0.01f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = !this.gameObject.GetComponent<SpriteRenderer>().enabled;
        StartCoroutine("Flash");
    }

    void StopEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            
        }
    }
}
