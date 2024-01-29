using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake() 
    {
        instance = this;
    }

    //[HideInInspector]
    public int currentHealh;
    public int maxHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealh = maxHealth;

        UIController.instance.UpdateHealth(currentHealh, maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer (int damageAmount)
    {
        currentHealh -= damageAmount;

        if(currentHealh <=0)
        {
            currentHealh = 0;

            gameObject.SetActive(false);
        }

        UIController.instance.UpdateHealth(currentHealh, maxHealth);


    }

}
