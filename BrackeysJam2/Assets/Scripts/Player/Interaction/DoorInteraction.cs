using UnityEngine;
using System;
using System.Collections;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private PlayerMovement m_movement;
    private bool isDoorUnlocked=false;
    
    
    private void Update()
    {
        if ( m_movement.isInInteractionRange)
        {
            StartCoroutine(OnDoorOpen());
        }

    }
    public IEnumerator OnDoorOpen()
    {
            if (!isDoorUnlocked)
            {
                Debug.Log("Door is Opening");
                m_animator.SetBool("DoorOpen", true);
                isDoorUnlocked = true;
                yield return null;
               /* float time = Time.time;
                Debug.Log($"Time:{time}");
                yield return new WaitForSeconds(6f);
                Debug.Log($"Time Passed:{time}");
                m_animator.SetBool("DoorClose", true);
                yield return new WaitForSeconds(0.8f);
                m_animator.SetBool("DoorOpen", false);
                isDoorUnlocked = false;*/
            }
        
    }


}
