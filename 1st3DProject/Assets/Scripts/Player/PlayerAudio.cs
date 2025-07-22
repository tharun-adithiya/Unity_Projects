using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerAudio : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public AudioSource footstepsSound;// sprintSound;
        public AudioSource playerVoice;

        void Update()
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))&&playerMovement.IsGrounded())
            {
                // if (Input.GetKey(KeyCode.LeftShift))
                // {
                footstepsSound.enabled = true;
                footstepsSound.pitch = Random.Range(0.8f,1f);               
                // sprintSound.enabled = true;
                // }
                //else
                // {
                // footstepsSound.enabled = true;
                // sprintSound.enabled = false;
                // }
            }
            else
            {
                footstepsSound.enabled = false;
                //sprintSound.enabled = false;
            }
        }


}


