﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform teleportPoint;

    public Transform renderPlane;

    public bool Growth = true;

    private bool playerIsOverlapping = false;

    // Update is called once per frame
    void Update()
    {

        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;

            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                CharacterController cc = player.GetComponent<CharacterController>();
                cc.enabled = false;

                float rotationDiff = -Quaternion.Angle(transform.rotation, teleportPoint.rotation);
                Debug.Log(rotationDiff);
                //rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;

                Vector3 portalOffsets = new Vector3(transform.position.x - renderPlane.position.x, 0, 0);


                if (Growth)
                {
                    player.transform.localScale = player.transform.localScale + new Vector3(0, player.localScale.y, 0);
                    player.position = teleportPoint.position + positionOffset + portalOffsets + new Vector3(0, player.localScale.y / 2, 0);
                }
                else
                {
                    player.transform.localScale = player.transform.localScale - new Vector3(0, player.localScale.y / 2, 0);
                    player.position = teleportPoint.position + positionOffset + portalOffsets - new Vector3(0, player.localScale.y / 2, 0);
                }

                cc.enabled = true;
                playerIsOverlapping = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }
}