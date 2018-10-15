﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Inspector vars
    [Tooltip("Speed of player movement")]
    [SerializeField]
    private float walkSpeed = 3;
    [Tooltip("Size of rays that detect walls")]
    [SerializeField]
    private float wallDetection = 0.5f;
    [Tooltip("Rotation speed of player")]
    [SerializeField]
    private float rotSpeed = 1.0f;
    [Tooltip("The max time you remain static when you reattach")]
    [SerializeField]
    private float attachRefresh = 0.3f;
    [SerializeField]
    private GameObject anchor;
    [Tooltip("The distance that the anchor spawns away from the player")]
    [SerializeField]
    private float anchorDistance = -1.0f;
    [Tooltip("The distance that the anchor offsets on the x axis")]
    [SerializeField]
    private float anchorOffset = -1.0f;
    [Tooltip("The amount of time it takes to rotate")]
    [SerializeField]
    private float rotateTime = 1.0f;
    [SerializeField]
    private GameObject m_model = null;
    [SerializeField]
    private GameObject shadow = null;
    [SerializeField]
    private Animator pipAni = null;
    [SerializeField]
    private float drag = 3.0f;
    #endregion

    #region Private vars
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private Vector3 direction = Vector3.zero;
    private Vector3 localMove;
    private Vector3 respawnPos = Vector3.zero;
    private Vector3 vel;
    private Quaternion oldRot;
    private Quaternion targetRot;
    private Rigidbody rb;
    private RaycastDetection raycastDetection;
    private ChangeForm changeForm;
    private GameObject anch;
    private Animator animator;
    private int anchorCount = 0;
    private float rotTimer;
    private float controlStaticTimer;
    private float inputX;
    private float inputY;
    private float attachTimer;
    private float rotZeroTimer = 0.0f;
    private bool onWall = false;
    private bool changeRotateDir = true;
    private bool detectFloor = false;
    private bool canMove = true;
    private bool canRotate = false;
    private bool rotateToZero = false;
    private bool onGround = false;
    private bool fallOff = false;
    private bool rotate = false;
    private bool downDetect = false;
    private bool startAttachTimer = false;
    private bool leftCorner = false;
    private bool rightCorner = false;
    private bool isDead = false;
    private bool respawning = false;
    private bool onRoof = false;
    private bool isMelding = false;
    private bool isUnmelded = false;
    #endregion

    #region Get Set
    public bool LeftCorner
    {
        get { return leftCorner; ; }
        set { leftCorner = value; }
    }

    public bool RightCorner
    {
        get { return rightCorner; ; }
        set { rightCorner = value; }
    }

    public bool OnWall
    {
        get { return onWall; }
    }

    public bool OnGround
    {
        get { return onGround; ; }
        set { onGround = value; }
    }

    public bool CanMove
    {
        get { return canMove; ; }
        set { canMove = value; }
    }

    public int AnchorCount
    {
        get { return anchorCount; }
        set { anchorCount = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public Vector3 RespawnPos
    {
        get { return respawnPos; }
        set { respawnPos = value; }
    }

    public bool IsMelding
    {
        get { return isMelding; }
        set { isMelding = value; }
    }

    public bool IsUnmelded
    {
        get { return isUnmelded; }
        set { isUnmelded = value; }
    }

    public bool DownDetect
    {
        get { return downDetect; }
    }
    #endregion

    void Awake()
    {
        //Obtain components
        rb = GetComponent<Rigidbody>();
        raycastDetection = GetComponent<RaycastDetection>();
        animator = GetComponent<Animator>();
        changeForm = GetComponent<ChangeForm>();
        respawnPos = transform.position;
    }

    void Update()
    {
        #region Small if checks

        if (onWall || onGround)
        {
            rb.drag = drag;
        }
        else
        {
            rb.drag = 0;
        }

        if (anchorCount == 1 && !onGround)
        {
            anch = Instantiate(anchor, transform.position + new Vector3(anchorOffset, 0, anchorDistance), Quaternion.identity);
            transform.parent = anch.transform;
            anchorCount++;
        }

        if (onWall)
        {
            //Do not use gravity
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            onGround = false;
        }
        else
        {
            rb.useGravity = true;
        }

        if (controlStaticTimer > (rotateTime + 0.0001f))
        {
            //Restore movement
            canMove = true;
        }

        if (fallOff)
        {
            rb.AddForce(Vector3.down);
        }

        if (startAttachTimer)
        {
            attachTimer += Time.deltaTime;
        }

        if (attachTimer > attachRefresh)
        {
            downDetect = false;
            startAttachTimer = false;
            attachTimer = 0.0f;
        }

        if (isDead)
        {
            respawning = true;
            rb.angularVelocity = Vector3.zero;
            transform.position = respawnPos;
            transform.rotation = Quaternion.identity;
            isDead = false;
        }
        #endregion

        //Raycasting to detect wall
        DetectWalls();

        if (canMove && !respawning)
        {
            //Reset static timer
            controlStaticTimer = 0.0f;

            //Calculate movement based on controller input normalised
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
            Vector3 horizontalMove = Vector3.zero;
            Vector3 verticalMove = Vector3.zero;
            Vector3 moveDir = Vector3.zero;

            if (onWall && !onRoof)
            {
                //Wall relative movement
                Vector3 velY = Vector3.up * inputY;// * Time.deltaTime * 100;
                Vector3 velX = new Vector3(-transform.up.z, 0, transform.up.x) * inputX;
                vel = Vector3.Normalize(velX + velY);
                vel *= Time.deltaTime * 100;

            }
            else
            {
                // This is character relative movement
                // vel = transform.forward * inputY + transform.right * inputX;
                // vel *= Time.deltaTime * 100;

                //Camera relative movement
                vel = Camera.main.transform.rotation * (new Vector3(inputX, 0.0f, inputY));
                vel.y = 0;
                vel.Normalize();
                vel *= Time.deltaTime * 100;
            }

            if (pipAni.gameObject.activeSelf == true)
            {
                if (!isUnmelded)
                {
                    pipAni.SetBool("isUnmelding", false);

                    if (!onWall && !onRoof && Input.GetButtonDown("Fire1") && raycastDetection.InShadow == true)
                    {
                        isMelding = true;
                        pipAni.Play("Meld");
                    }
                }
                else
                {
                    pipAni.SetBool("isUnmelding", true);
                    pipAni.Play("Unmeld");
                }
            
                //Animations
                if (vel != Vector3.zero)
                {
                    pipAni.SetBool("isIdle", false);
                }
                else
                {
                    pipAni.SetBool("isIdle", true);
                }
            }



                //Model rotation
                m_model.gameObject.transform.LookAt(transform.position + vel);

                shadow.transform.localRotation = Quaternion.Euler(new Vector3(0, Vector3.SignedAngle(transform.forward, m_model.gameObject.transform.forward, transform.up), 0));

                //Update move direction
                moveDir = vel;

                //Smooth the movement
                Vector3 targetMoveAmount = moveDir * walkSpeed;
                moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
            }
            else
            {
                //Reset values and start timer
                inputX = 0.0f;
                inputY = 0.0f;
                moveAmount = Vector3.zero;
                controlStaticTimer += Time.deltaTime;
            }
    }

    private void LateUpdate()
    {
        //Rotation across walls
        RotatePlayer();

        //Rotate when you're back on the ground
        RotateBack();

        if (fallOff)
        {
            FallOffWall();
        }

        DetachOffWall();

        FallOutOfShadow();
    }

    private void DetectWalls()
    {
        RaycastHit objectHit;

        //Shoot raycast in four directions
        if (canMove)
        {
            //Downwards detection
            if (!Physics.Raycast(shadow.transform.position, -shadow.transform.up, out objectHit, 0.5f))
            {
                fallOff = true;

                if (onGround && !rotateToZero)
                {
                    onGround = false;
                }
            }
            else
            {
                if (objectHit.transform.tag == "Floor")
                {
                    onRoof = false;
                    onWall = false;
                    onGround = true;
                }

                if (objectHit.transform.tag == "Roof" && !downDetect)
                {
                    onRoof = true;
                }
            }

            //Up
            if (Physics.Raycast(shadow.transform.position, shadow.transform.up, out objectHit, 0.5f) && changeForm.IsLittle)
            {
                if (!onWall)
                {
                    Debug.Log("Up Raycast on: " + objectHit.collider);
                    Debug.DrawRay(shadow.transform.position, shadow.transform.up, Color.blue);
                    canRotate = true;

                    //If hit wall or floor, change direction to the normal of the hit objectvggw
                    if (objectHit.transform.tag == "Wall")
                    {
                        direction = objectHit.normal.normalized;
                        detectFloor = false;
                    }
                    else if (objectHit.transform.tag == "Floor")
                    {
                        direction = objectHit.normal.normalized;
                        detectFloor = true;
                    }
                }
            }

            //Forward
            else if (Physics.Raycast(shadow.transform.position, shadow.transform.forward, out objectHit, wallDetection) && (inputY != 0 || inputX != 0) && changeForm.IsLittle)
            {
                Debug.Log("Forward Raycast on: " + objectHit.collider);
                Debug.DrawRay(transform.position, transform.forward, Color.blue);
                canRotate = true;

                //If hit wall or floor, change direction to the normal of the hit object
                if (objectHit.transform.tag == "Wall")
                {
                    direction = objectHit.normal.normalized;
                    detectFloor = false;
                }
                else if (objectHit.transform.tag == "Roof")
                {
                    direction = objectHit.normal.normalized;
                    detectFloor = false;
                }
                else if (objectHit.transform.tag == "Floor")
                {
                    direction = objectHit.normal.normalized;
                    detectFloor = true;
                }
            }

            //Down
            else if (Physics.Raycast(transform.position, -transform.up, out objectHit, 0.5f) && !onWall && !onGround && changeForm.IsLittle)
            {
                Debug.Log("Downwards Raycast on: " + objectHit.collider);
                Debug.DrawRay(transform.position, transform.forward, Color.blue);


                //If hit wall or floor, change direction to the normal of the hit object
                if (objectHit.transform.tag == "Wall")
                {
                    downDetect = true;
                    detectFloor = false;
                }
                else if(objectHit.transform.tag == "Roof")
                {
                    downDetect = true;
                    detectFloor = false;
                }
                else if (objectHit.transform.tag == "Floor")
                {
                    downDetect = false;
                    detectFloor = true;
                }
            }
            else
            {
                //While rotating
                direction = Vector3.zero;
                canRotate = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (onGround)
        {
            //Reset values
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            fallOff = false;
            downDetect = false;
            respawning = false;
        }

        if (canMove)
        {
            //Apply movement to rigidbody
            //localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
            localMove = moveAmount * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + localMove);
        }
        else
        {
            //Reset values
            localMove = Vector3.zero;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void RotatePlayer()
    {
        //Mounting wall from down raycast
        if (Input.GetButtonDown("Fire1") && !onWall && !onRoof && downDetect && raycastDetection.InShadow == true && changeForm.IsLittle)
        {
            onWall = true;
            fallOff = false;
            startAttachTimer = true;
            rotateToZero = false;
            changeRotateDir = true;
        }
        //Mounting wall
        else if (direction != Vector3.zero && raycastDetection.InShadow == true && !downDetect && changeForm.IsLittle)
        {
            fallOff = false;
            canMove = false;
            onWall = true;
            rotTimer = 0.0f;
            oldRot = transform.rotation;

            RotateDirection();

            #region Old Detect
            //Looking into wall
            //if (Vector3.Dot(transform.forward, direction) < -0.95f)
            //{
            //    targetRot = Quaternion.LookRotation(Vector3.up, direction);
            //}
            ////Looking Away From Wall
            //else if (Vector3.Dot(transform.forward, direction) > 0.95f)
            //{
            //    targetRot = Quaternion.LookRotation(Vector3.down, direction);
            //}
            ////Looking Across the wall
            //else if (Vector3.Dot(transform.forward, direction) < 0.05f &&
            //    Vector3.Dot(transform.forward, direction) > -0.05f)
            //{
            //    targetRot = Quaternion.LookRotation(transform.forward, direction);
            //}
            #endregion

            rotate = true;
            direction = Vector3.zero;
        }
        //Leaving Wall
        else if (direction != Vector3.zero && detectFloor == true && onWall && changeForm.IsLittle)
        {
            onWall = false;
            canMove = false;
            rotTimer = 0.0f;
            oldRot = transform.rotation;
            RotateDirection();

            rotate = true;
            direction = Vector3.zero;
        }


        if (rotate == true)
        {
            rotTimer += Time.deltaTime;

            if (transform.rotation != targetRot) /* Does not equal our target rotation */
            {
                //Slerp towards rotation
                transform.rotation = Quaternion.Slerp(oldRot, targetRot, rotTimer * rotSpeed);
            }
        }

        if (rotTimer > rotateTime)
        {
            //Reset
            rotate = false;
            transform.rotation = targetRot;
            changeRotateDir = true;
            rotTimer = 0.0f;
        }
    }

    private void DetachOffWall()
    {
        if ((onWall || onRoof) && !canRotate && !downDetect && !rightCorner && !leftCorner)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //Player falls and rotates back to land on ground
                rb.AddForce(Vector3.down);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 1.0f);
                changeRotateDir = true;
                direction = Vector3.up;
                onWall = false;
                onRoof = false;
                rotateToZero = true;
                RotateDirection();
            }
        }
    }

    private void FallOutOfShadow()
    {
        if (onWall && raycastDetection.InShadow == false)
        {
            //Player falls and rotates back to land on ground
            rb.AddForce(Vector3.down);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 1.0f);
            changeRotateDir = true;
            direction = Vector3.up;
            onRoof = false;
            onWall = false;
            rotateToZero = true;
            RotateDirection();

            if (anch != null)
            {
                transform.parent = null;
                anchorCount = 0;
                leftCorner = false;
                rightCorner = false;
                Destroy(anch);
            }
        }
    }

    private void FallOffWall()
    {
        if ((onWall || onRoof) && fallOff)
        {
            //Player falls and rotates back to land on ground
            rb.AddForce(Vector3.down);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 1.0f);
            changeRotateDir = true;
            direction = Vector3.up;
            onWall = false;
            onRoof = false;
            rotateToZero = true;
            RotateDirection();

            if (anch != null)
            {
                transform.parent = null;
                anchorCount = 0;
                leftCorner = false;
                rightCorner = false;
                Destroy(anch);
            }
        }
    }

    private void RotateBack()
    {
        if (!onWall && rotateToZero && onGround)
        {
            //Slerp to pos
            rotZeroTimer += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotZeroTimer * rotSpeed);
        }

        if (rotZeroTimer > 1)
        {
            //Reset
            rotateToZero = false;
            transform.rotation = targetRot;
            changeRotateDir = true;
            rotZeroTimer = 0;
        }
    }

    private void RotateDirection()
    {
        if (changeRotateDir)
        {
            //Looking into floor
            if (Vector3.Dot(transform.forward, direction) < -0.95f)
            {
                targetRot = Quaternion.LookRotation(transform.up, direction);
                changeRotateDir = false;
            }
            //Looking Away From floor
            else if (Vector3.Dot(transform.forward, direction) > 0.95f)
            {
                targetRot = Quaternion.LookRotation(-transform.up, direction);
                changeRotateDir = false;
            }
            //Looking Across the floor
            else if (Vector3.Dot(transform.forward, direction) < 0.05f &&
                Vector3.Dot(transform.forward, direction) > -0.05f)
            {
                targetRot = Quaternion.LookRotation(transform.forward, direction);
                changeRotateDir = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            onGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LeftCorner")
        {
            leftCorner = true;
            anchorCount++;
        }

        if (other.tag == "RightCorner")
        {
            rightCorner = true;
            anchorCount++;
        }

        if (other.tag == "Death")
        {
            isDead = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (onWall)
        {
            if (other.tag == "LeftCorner")
            {
                //Destroy
                leftCorner = true;
                anchorCount = 0;
                transform.parent = null;
                Destroy(anch);
            }

            if (other.tag == "RightCorner")
            {
                //Destroy
                rightCorner = true;
                anchorCount = 0;
                transform.parent = null;
                Destroy(anch);
            }
        }
    }
}
