using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public properties
    public float movementSpeed = 10f;
    public float jumpPower = 10f;
    public float jumpMovementFactor = 1f;

    //state machine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState;
    [HideInInspector] public Jump jumpState;
    [HideInInspector] public Dead deadState;

    //internal properties
    [HideInInspector] public bool hasJumpInput;
    //[HideInInspector] public bool isGrounded;
    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public Rigidbody characterRigidbody;
    [HideInInspector] public Collider thisCollider;
    [HideInInspector] public Animator characterAnimator;

    private void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        thisCollider = GetComponent<Collider>();
        characterAnimator = GetComponent<Animator>();
    }


    void Start()
    {
        //stateMachine and its states
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        walkingState = new Walking(this);
        jumpState = new Jump(this);
        deadState = new Dead(this);
        stateMachine.ChangeState(idleState);
    }


    void Update()
    {
        //check game Over
        if (GameManager.Instance.isGameOver)
        {
            if(stateMachine.currentStateName != deadState.name)
            {
                stateMachine.ChangeState(deadState);
            }
        }
        //Read input
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        //Create Movement vector
        //this is the short hand if , can be read like this, if isup is true then movement x = 1 ,else if isdown is true then movement x = -1 else movement x = 0
        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        float inputY = isUp ? 1 : isDown ? -1 : 0;
        movementVector = new Vector2(inputX, inputY);        
        hasJumpInput = Input.GetKey(KeyCode.Space);

        // set the velocity from 0 to 1 to the animator so the character "knows" whta animation to play
        float velocity = characterRigidbody.velocity.magnitude;// this line will calculate the velocity that the Character Rigidbody is moving
        float velocityRate = velocity / movementSpeed; // this line will guarantee that it's between 0 and 1
        characterAnimator.SetFloat("fVelocity", velocity);// and here finally we get the speed and pass to the animator.

        //DetectGround
        //DetectGround();

        stateMachine.Update();
    }

    private void LateUpdate()
    {
        stateMachine.LateUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public Quaternion getForward()
    {
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        return Quaternion.Euler(0, eulerY, 0);
    }

    public void RotateBodyToFaceInput()
    {
        if (movementVector.IsZero()) { return; }
        //calculate rotation
     
        Camera camera = Camera.main;
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up);
        Quaternion q2 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Quaternion toRotation = q1 * q2;
        //interpolation so the rotation is not to too rough

        Quaternion newRotation = Quaternion.LerpUnclamped(transform.rotation, toRotation, 0.15f);

        //apply rotation
        characterRigidbody.MoveRotation(newRotation);

    }

    /*
    private void DetectGround()
    {
        
        //reset flag
        isGrounded = false;

        //detect gound
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Bounds bounds = thisCollider.bounds;
        float radius = bounds.size.x * 0.33f;
        float maxDistance = bounds.size.y * 0.25f;
        if (Physics.SphereCast(origin, radius, direction, out var hitInfo, maxDistance))
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject.CompareTag("Plataform") || hitObject.CompareTag("Plataform2"))
            {                
                isGrounded = true;
            }
        }
    }
    */
}
