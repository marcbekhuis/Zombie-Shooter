using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Tile> tiles = new List<Tile>();
    public int amountOfWallsUp = 0;

    [Space]

    public Transform player;
    public List<Transform> zombies = new List<Transform>();

    public Vector2 mapSize;

    [Space]

    [SerializeField]
    private float delay = 60;

    [SerializeField]
    private float speed = 20;

    private float cooldown;

    [SerializeField]
    public class Tile
    {
        // All the information need for each wall to make them move up and down, also need for the zombie spawner
        public Tile(GameObject Wall,Vector3 Location, bool WallUp, bool WallMovingUp, bool WallMovingDown, Material Material)
        {
            wall = Wall;
            location = Location;
            wallUp = WallUp;
            wallMovingDown = WallMovingDown;
            wallMovingUp = WallMovingUp;
            material = Material;
        }
        public GameObject wall;
        public Vector3 location;
        public bool wallUp;
        public bool wallMovingUp;
        public bool wallMovingDown;
        public Material material;
    }

    private void Start()
    {
        cooldown = Time.time + delay;
    }

    private void FixedUpdate()
    {
        if (!PlayerHealth.gameOver && !PauseMenuToggle.gamePaused)
        {
            // Loops through all the walls when the cooldown is done, changing bools to tell the system to moving all the walls that are up down and making around the same amount of walls go up.
            if (Time.time > cooldown)
            {
                int wallsGoingUp = 0;
                foreach (var tile in tiles)
                {
                    bool zombieInRange = false;
                    foreach (var zombie in zombies)
                    {
                        if (Vector2.Distance(new Vector2(zombie.position.x, zombie.position.z), new Vector2(tile.location.x, tile.location.z)) < 2)
                        {
                            zombieInRange = true;
                            break;
                        }
                    }

                    // Makes the wall move down if its up and not moving up.
                    if (tile.wallUp && !tile.wallMovingUp)
                    {
                        tile.wallUp = false;
                        tile.wallMovingDown = true;
                    }
                    // Makes the wall move up if the random number is right and its not moving down.
                    else if (Random.Range(0, 20) < 5 && (wallsGoingUp <= amountOfWallsUp && !tile.wallMovingDown) && Vector2.Distance(new Vector2(player.position.x, player.position.z), new Vector2(tile.location.x, tile.location.z)) > 3 && !zombieInRange)
                    {
                        tile.wallUp = true;
                        tile.wallMovingUp = true;
                        wallsGoingUp++;
                    }
                }
                cooldown = Time.time + delay;
            }
            MoveWall();
        }
    }

    // moves the walls up or down.
    void MoveWall()
    {
        foreach (var tile in tiles)
        {
            if (tile.wallMovingDown)
            {

                tile.wall.transform.position = new Vector3(tile.wall.transform.position.x, Mathf.Clamp(tile.wall.transform.position.y - Time.deltaTime * speed, -1.95f, 0), tile.wall.transform.position.z);
                // Sets the color of the wall to blue when its moving down.
                tile.material.color = new Color(0, 0, Mathf.Clamp(tile.material.color.b + Time.deltaTime * speed, 0.1f, 1));
                if (tile.wall.transform.position.y <= -1.95f)
                {
                    tile.wallMovingDown = false;
                }
            }
            else if (tile.wallMovingUp)
            {
                tile.wall.transform.position = new Vector3(tile.wall.transform.position.x, Mathf.Clamp(tile.wall.transform.position.y + Time.deltaTime * speed, -1.95f, 0), tile.wall.transform.position.z);
                // Sets the color of the wall to dark blue when its moving up.
                tile.material.color = new Color(0, 0, Mathf.Clamp(tile.material.color.b - Time.deltaTime * speed, 0.1f, 1));
                if (tile.wall.transform.position.y >= 0)
                {
                    tile.wallMovingUp = false;
                }
            }
        }
    }
}
