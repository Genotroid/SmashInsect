using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _angle = 30f;
    [SerializeField] private float _changeAngleTime = 0.5f;

    private Rigidbody2D _rigidBody;

    private void OnEnable()
    {
        ChangeDirection(getRandomRotation());
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.Translate(-transform.up * _moveSpeed * Time.deltaTime, Space.World);
    }

    private Quaternion getRandomRotation()
    {
        return Quaternion.AngleAxis(Random.Range(-_angle, _angle), transform.forward);
    }

    public void ChangeDirection(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }

    public void Flip()
    {
        ChangeDirection(Quaternion.AngleAxis(-transform.eulerAngles.z, transform.forward));
    }
}
