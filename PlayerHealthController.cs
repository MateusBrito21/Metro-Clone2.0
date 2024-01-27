using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [HideInInspector]
    public int currentHealh;
    public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealh = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
