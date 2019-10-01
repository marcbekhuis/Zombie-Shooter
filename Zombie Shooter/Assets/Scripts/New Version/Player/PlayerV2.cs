using UnityEngine;

public class PlayerV2 : MonoBehaviour
{
    [SerializeField] 
    private Gun _gun = null;

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

    private Camera _camera;//Caching a reference to the camera since we frequently need it (Update) and Camera.main is a costly operation 
    private float vertical = 0;
    private float horizontal = 0;

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
        if (!PlayerHealth.gameOver && !PauseMenuToggle.gamePaused)
        {
            Move();
            Rotate();
            RotateGunTowardsMouse();
            CheckForFire();
        }
    }

    private void Move()
    {
        //Get a normalized movement direction
        Vector2 movementInput = RegisterMovement();

        //Apply consistent(not fps dependent, aka use of Time.deltaTime) displacement with a variable speed property(_movementSpeed)
        RB.velocity = RB.transform.rotation * new Vector3(movementInput.x * _movementSpeed, 0, movementInput.y * _movementSpeed);
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
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            _gun.TryToFire();
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

}
