using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    private PlayerController player;


    private void LateUpdate()
    {
        if (player != null)
            transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, offset.z);
        else player = Root.PlayerReference;
    }
}
