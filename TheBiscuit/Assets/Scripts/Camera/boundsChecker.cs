using System;
using UnityEngine;

public class BoundsChecker : MonoBehaviour
{
    public delegate void BoundsCollisionEvents(string BoundsName);
    public static event BoundsCollisionEvents OnPassCamBounds;
    public static void TriggerOnPassBounds(string gameObjName)
    {
        OnPassCamBounds?.Invoke(gameObjName);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CameraBounds"))
        {
            TriggerOnPassBounds(collision.gameObject.name);
        }
    }
}
