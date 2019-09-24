using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Tile> tiles = new List<Tile>();
    public int amountOfWallsUp = 0;

    public Transform player;
    public List<Transform> zombies = new List<Transform>();

    public Vector2 mapSize;

    [SerializeField]
    private float delay = 60;

    [SerializeField]
    private float speed = 20;

    private float cooldown;

    [SerializeField]
    public class Tile
    {
        public Tile(GameObject Wall,Vector3 Location, bool WallUp, bool WallMovingUp, bool WallMovingDown)
        {
            wall = Wall;
            location = Location;
            wallUp = WallUp;
            wallMovingDown = WallMovingDown;
            wallMovingUp = WallMovingUp;
        }
        public GameObject wall;
        public Vector3 location;
        public bool wallUp;
        public bool wallMovingUp;
        public bool wallMovingDown;
    }

    private void Start()
    {
        cooldown = Time.time + delay;
    }

    private void Update()
    {
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

                if (Vector2.Distance(new Vector2(player.position.x, player.position.z), new Vector2(tile.location.x, tile.location.z)) > 3 && !zombieInRange)
                {
                    if (tile.wallUp && !tile.wallMovingDown)
                    {
                        tile.wallUp = false;
                        tile.wallMovingDown = true;
                    }
                    else if (Random.Range(0, 20) < 5 && (wallsGoingUp <= amountOfWallsUp && !tile.wallMovingUp))
                    {
                        tile.wallUp = true;
                        tile.wallMovingUp = true;
                        wallsGoingUp++;
                    }
                }
            }
            cooldown = Time.time + delay;
        }
        MoveWall();
    }

    void MoveWall()
    {
        foreach (var tile in tiles)
        {
            if (tile.wallMovingDown)
            {

                tile.wall.transform.position = new Vector3(tile.wall.transform.position.x, Mathf.Clamp(tile.wall.transform.position.y - Time.deltaTime * speed, -1.95f, 0), tile.wall.transform.position.z);
                if (tile.wall.transform.position.y <= -1.95f)
                {
                    tile.wallMovingDown = false;
                }
            }
            else if (tile.wallMovingUp)
            {
                tile.wall.transform.position = new Vector3(tile.wall.transform.position.x, Mathf.Clamp(tile.wall.transform.position.y + Time.deltaTime * speed, -1.95f, 0), tile.wall.transform.position.z);
                if (tile.wall.transform.position.y >= 0)
                {
                    tile.wallMovingUp = false;
                }
            }
        }
    }
}
