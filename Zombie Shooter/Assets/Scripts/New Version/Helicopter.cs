using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Transform landingZone;

    [SerializeField] private float speed = 10;

    private bool aboveDestanation = false;
    private bool atDestanation = false;

    private void Start()
    {
        this.transform.LookAt(new Vector3(landingZone.position.x, 100, landingZone.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (true) //Time.timeSinceLevelLoad > 60 * 10
        {
            if (Vector3.Distance(new Vector3(landingZone.position.x, 100, landingZone.position.z),this.transform.position) < 0.3f)
            {
                aboveDestanation = true;
            }

            if (aboveDestanation && !atDestanation)
            {
                if (Vector3.Distance(landingZone.position, this.transform.position) < 0.3f)
                {
                    atDestanation = true;
                }
                else
                {
                    if (Vector3.Distance(landingZone.position, this.transform.position) < 4)
                    {
                        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
                        this.transform.Translate(new Vector3(0, -Time.deltaTime, 0));
                    }
                    else
                    {
                        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
                        this.transform.Translate(new Vector3(0, -Time.deltaTime * 4, 0));
                    }
                }
            }
            else if (!atDestanation)
            {
                if (Vector3.Distance(new Vector3(landingZone.position.x, 100, landingZone.position.z), this.transform.position) < 2)
                {
                    this.transform.LookAt(new Vector3(landingZone.position.x, 100, landingZone.position.z));
                    this.transform.Translate(this.transform.rotation * new Vector3(0, 0, Time.deltaTime * 5), Space.World);
                }
                else if (Vector3.Distance(new Vector3(landingZone.position.x, 100, landingZone.position.z), this.transform.position) < 7)
                {
                    this.transform.LookAt(new Vector3(landingZone.position.x, 100, landingZone.position.z));
                    this.transform.Translate(this.transform.rotation * new Vector3(0, 0, Time.deltaTime * (speed / 8)), Space.World);
                }
                else if (Vector3.Distance(new Vector3(landingZone.position.x, 100, landingZone.position.z), this.transform.position) < 25)
                {
                    this.transform.LookAt(new Vector3(landingZone.position.x, 100, landingZone.position.z));
                    this.transform.Translate(this.transform.rotation * new Vector3(0, 0, Time.deltaTime * (speed / 4)), Space.World);
                }
                else
                {
                    this.transform.LookAt(new Vector3(landingZone.position.x, 100, landingZone.position.z));
                    this.transform.Translate(this.transform.rotation * new Vector3(0, 0, Time.deltaTime * speed), Space.World);
                }
            }
        }
    }
}
