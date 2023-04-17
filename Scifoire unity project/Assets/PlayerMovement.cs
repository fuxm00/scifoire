using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _gravity;
    [SerializeField] float _jumpHeight;

    private CharacterController _controller;
    private float _inputX;
    private float _inputY;
    private Vector3 _velocity;

    private Transform _groundCheck;
    [SerializeField] float _groundCheckDistance;
    [SerializeField] LayerMask _groundMask;
    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _groundCheck = transform.Find("GroundCheck");
        _controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        StandingGravity();
        ObtainInput();
        HorizontalForces();
        VerticalForces();
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
    }

    private void StandingGravity()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    private void ObtainInput()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputY = Input.GetAxis("Vertical");
    }

    private void HorizontalForces()
    {
        Vector3 move = transform.right * _inputX + transform.forward * _inputY;
        _controller.Move(move * _speed * Time.deltaTime);
    }  


    private void VerticalForces() {
        JumpCheck();
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }
}
