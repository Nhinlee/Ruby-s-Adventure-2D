using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyMovement : MonoBehaviour
{
    // Ref Component
    [SerializeField]
    private Rigidbody2D myRigid;
    [SerializeField]
    private Animator myAnimator;
    [SerializeField]
    private float moveSpeed;

    // Hash Animator Parameter (int)
    private int lookXParam;
    private int lookYParam;
    private int speedParam;

    // Fields
    private float vertical;
    private float horizontal;
    private Vector2 lookDirection = new Vector2(1f, 0f);

    public Vector2 LookDirection { get => lookDirection; }

    private void Start()
    {
        lookXParam = Animator.StringToHash("LookX");
        lookYParam = Animator.StringToHash("LookY");
        speedParam = Animator.StringToHash("Speed");
    }

    private void Update()
    {
        GetInput();
        MoveRuby();
        UpdateAnimatorParam();
    }

    private void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }

    private void MoveRuby()
    {
        // Move
        Vector2 newPos = transform.position;
        newPos.x += horizontal * moveSpeed;
        newPos.y += vertical * moveSpeed;
        myRigid.MovePosition(newPos);
        // Set Direction
        if(!Mathf.Approximately(horizontal,0f) || !Mathf.Approximately(vertical,0f))
        {
            lookDirection.Set(horizontal, vertical);
            lookDirection.Normalize();
        }
    }

    private void UpdateAnimatorParam()
    {
        myAnimator.SetFloat(lookXParam, lookDirection.x);
        myAnimator.SetFloat(lookYParam, lookDirection.y);
        myAnimator.SetFloat(speedParam, (new Vector2(horizontal,vertical)).magnitude);
    }

}
