using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;
    public BoxCollider2D boundsBox;

    private float halfHeigtht, halfWidth;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();

        halfHeigtht = Camera.main.orthographicSize;
        halfWidth = halfHeigtht * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp( player.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth), 
                Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfHeigtht, boundsBox.bounds.max.y - halfHeigtht),
                transform.position.z);
        }
    }
}
