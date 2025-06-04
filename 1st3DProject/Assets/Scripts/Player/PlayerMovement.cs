using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float _speed=5f;
    [SerializeField] private float _dashSpeed = 5f;
    [SerializeField] private float _jumpSpeed=5f;
   
    public CharacterController controller;
    private Vector3 _velocity;
    [SerializeField]private float _gravity=-9.81f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _ground;

    void Update()
    {
        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        Vector3 yMovement = transform.up * _jumpSpeed;

        controller.Move(movement.normalized*_speed*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            controller.Move(movement * _speed * _dashSpeed*Time.deltaTime);
        }
        _velocity.y += _gravity * Time.deltaTime;
        controller.Move(_velocity*Time.deltaTime);

        if (isGrounded() && _velocity.y < 0f)
        {
            _velocity.y = -2f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = Mathf.Sqrt(-2 * _jumpSpeed * _gravity);
            }
        }
    }

    private bool isGrounded()
    {
        return Physics.CheckSphere(groundChecker.position,_checkRadius,_ground);
    }

}
