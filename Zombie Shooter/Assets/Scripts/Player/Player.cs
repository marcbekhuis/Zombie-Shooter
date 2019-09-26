using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Gun _gun = null;
    [SerializeField] private float _movementSpeed = 10.0f;
    [SerializeField] private Rigidbody RB;

    private Camera _camera;//Caching a reference to the camera since we frequently need it (Update) and Camera.main is a costly operation 

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
            RotateTowardsMouse();
            CheckForFire();
        }
    }

    private void Move()
    {
        //Get a normalized movement direction
        Vector2 movementInput = RegisterMovement();

        //Apply consistent(not fps dependent, aka use of Time.deltaTime) displacement with a variable speed property(_movementSpeed)
        RB.velocity = new Vector3(movementInput.x * _movementSpeed, RB.velocity.y, movementInput.y * _movementSpeed);
    }

    private Vector2 RegisterMovement()
    {
        //Start every movement input check with an empty Vector
        Vector2 movementInput = Vector2.zero;

        //Every move key alters the Vector, so that directions can cancel each other out (left + right => no horizontal change)
        if (Input.GetKey(KeyCode.W))
        {
            movementInput.y++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementInput.x--;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementInput.y--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementInput.x++;
        }

        //Normalize Vector so that diagonal movement is not faster than horizontal or vertical movement
        movementInput.Normalize();

        return movementInput;
    }

    private void RotateTowardsMouse()
    {
        //Create a ray from the mouse cursor on the game screen into the world using the camera
        Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);
        //Create a RaycastHit variable to store additional data about the raycastHit
        RaycastHit hitInfo;



        if (Physics.Raycast(cameraRay, out hitInfo))
        {
            //Storing the position of the raycastCollision into a new Vector
            Vector3 rayCollisionPosition = hitInfo.point;

            //Copying the collisionPosition over to a new lookPosition Vector
            Vector3 lookPosition = rayCollisionPosition;
            //Altering the lookPosition height to the same height as the player currently is (mainly because we want the player to keep looking forward, not downwards or upwards
            lookPosition.y = transform.position.y;

            //Optional visualization of the ray in green to show a successful ray collision
            Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.green);
            transform.LookAt(lookPosition);
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

}
