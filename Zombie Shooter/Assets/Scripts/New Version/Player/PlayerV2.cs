﻿using UnityEngine;

public class PlayerV2 : MonoBehaviour
{
    [SerializeField] 
    private GunV2 _gun = null;

    [SerializeField]
    private Transform gunRotation;

    [SerializeField]
    private Transform cameraRotation;

    [SerializeField]
    private float cameraSpeed = 10.0f;

    [SerializeField] 
    private float _movementSpeed = 10.0f;

    [SerializeField] 
    private Rigidbody RB;
    [Space]
    [SerializeField] private AudioSource walkSource;
    [SerializeField] private AudioClip leftFoot1;
    [SerializeField] private AudioClip leftFoot2;
    [SerializeField] private AudioClip rightFoot1;
    [SerializeField] private AudioClip rightFoot2;
    [SerializeField] private float footSoundDelay = 3;

    private Camera _camera;//Caching a reference to the camera since we frequently need it (Update) and Camera.main is a costly operation 
    private float vertical = 0;
    private float horizontal = 0;
    private bool foot = false;
    private float footSoundCooldown = 0;

    private void Awake()
    {
        //Asserting for having a valid _gun reference
        Debug.Assert(_gun != null, "No Gun reference set in the Inspector");

        //Asserting for having a valid camera reference
        _camera = Camera.main;
        Debug.Assert(_camera != null, "Check if your main camera has the MainCamera Tag!");
    }

    private void Update()
    {
        if (!PlayerHealthV2.gameOver && !PauseMenuToggle.gamePaused)
        {
            Move();
            Rotate();
            RotateGunTowardsMouse();
            CheckForFire();
            CheckForReload();
            Jump();
        }
    }

    private void Move()
    {
        //Get a normalized movement direction
        Vector2 movementInput = RegisterMovement();

        //Apply consistent(not fps dependent, aka use of Time.deltaTime) displacement with a variable speed property(_movementSpeed)
        RB.velocity = RB.transform.rotation * new Vector3(movementInput.x * _movementSpeed, RB.velocity.y, movementInput.y * _movementSpeed);

        if (movementInput.x != 0 || movementInput.y != 0)
        {
            if (Time.timeSinceLevelLoad > footSoundCooldown)
            {
                if (foot)
                {
                    if (Random.Range(0, 100) < 50)
                    {
                        walkSource.PlayOneShot(leftFoot1);
                    }
                    else
                    {
                        walkSource.PlayOneShot(leftFoot2);
                    }
                    foot = false;
                }
                else
                {
                    if (Random.Range(0, 100) < 50)
                    {
                        walkSource.PlayOneShot(rightFoot1);
                    }
                    else
                    {
                        walkSource.PlayOneShot(rightFoot2);
                    }
                    foot = true;
                }
                footSoundCooldown = Time.timeSinceLevelLoad + footSoundDelay;
            }
        }
    }

    private Vector2 RegisterMovement()
    {
        //Start every movement input check with an empty Vector
        Vector2 movementInput = Vector2.zero;

        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //Normalize Vector so that diagonal movement is not faster than horizontal or vertical movement
        movementInput.Normalize();

        return movementInput;
    }

    private void RotateGunTowardsMouse()
    {
        //Create a ray from the mouse cursor on the game screen into the world using the camera
        Ray cameraRay = _camera.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
        //Create a RaycastHit variable to store additional data about the raycastHit
        RaycastHit hitInfo;



        if (Physics.Raycast(cameraRay, out hitInfo))
        {
            //Storing the position of the raycastCollision into a new Vector
            Vector3 rayCollisionPosition = hitInfo.point;

            //Copying the collisionPosition over to a new lookPosition Vector
            Vector3 lookPosition = rayCollisionPosition;

            //Optional visualization of the ray in green to show a successful ray collision
            Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.green);
            gunRotation.LookAt(lookPosition);
        }
        else
        {
            //Optional visualization of the ray in red to show NO successful ray collision
            Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.red);
        }

        //Note only rotates when there is a rayCastCollision, might not be the best solution
        //Consider calculating mathematically or using an invisible plane combined with a specific new layer
        //Warning fdaf

    }

    private void CheckForFire()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _gun.TryToFire();
        }
    }

    private void CheckForReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _gun.Reload();
        }
    }

    private void Rotate()
    {
        vertical += Input.GetAxis("Mouse X") * cameraSpeed;
        horizontal -= Input.GetAxis("Mouse Y") * cameraSpeed;
        if (horizontal >= 50)
        {
            horizontal = 50;
        }
        else if (horizontal <= -80)
        {
            horizontal = -80;
        }
        cameraRotation.localEulerAngles = new Vector3(horizontal,0,0);
        RB.transform.eulerAngles = new Vector3(RB.transform.eulerAngles.x,vertical, 0);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Physics.CheckBox(this.transform.position - new Vector3(0,1.02f,0), new Vector3(0.05f,0.01f,0.05f)))
        {
            RB.AddForce(new Vector3(0,30,0),ForceMode.Impulse);
        }
    }
}
