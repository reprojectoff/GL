using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _animator;
    string _isRunning;
    Rigidbody rb; //RigidBody
    Vector3 input; // ������ ��� ����������
  
    [SerializeField] private float _speedRun; // �������� ����
    [SerializeField] private float _speedWalk; // �������� ������
    private float _speed; // �������� ��������

    [SerializeField] private float _speedTurn; // �������� ��������
    [SerializeField] private bool _matrix = false; // �������������� �������� ��� ���������
    Vector3 relative;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        InputGet();
        Look();
        
    }
    private void FixedUpdate()
    {
        if (input != Vector3.zero)
        {
            Move();

        }


    }
    private void InputGet()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized; // �������� ������
        if (Input.GetKey(KeyCode.LeftShift)&& (input != Vector3.zero)) // ��������� ��� �����
        {
            _speed = _speedRun;
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _speed = _speedWalk;
            _animator.SetBool("IsWalking", true);
            _animator.SetBool("IsRunning", false);
        }
    }
    private void Move()
    {
        rb.MovePosition(transform.position + (transform.forward *input.magnitude) * _speed * Time.deltaTime); // ��������
    }
    private void Look()
    {
        if (input != Vector3.zero)
        {
            if (_matrix == true) // ��������� �� ��� �������
            {
                relative = (transform.position + input.ToIso()) - transform.position;
            }
            else
            {
                relative = (transform.position + input) - transform.position;
            }
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _speedTurn * Time.deltaTime);

        }
        else
        {
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("IsRunning", false);
        }
    }
}
