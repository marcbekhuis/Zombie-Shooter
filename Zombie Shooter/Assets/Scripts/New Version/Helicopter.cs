using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private Transform landingZone;
    [SerializeField] private float speed = 10;

    private bool aboveDestanation = false;
    private bool atDestanation = false;

    private void Start()
    {
        this.transform.LookAt(new Vector3(landingZone.position.x, landingZone.position.y + 10, landingZone.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (true) //Time.timeSinceLevelLoad > 60 * 10
        {
            if (Vector3.Distance(new Vector3(landingZone.position.x, landingZone.position.y + 20, landingZone.position.z),this.transform.position) < 0.2f)
            {
                aboveDestanation = true;
            }

            if (aboveDestanation)
            {
                this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
                this.transform.Translate(new Vector3(0, -Time.deltaTime, 0));
            }
            else
            {
                this.transform.LookAt(new Vector3(landingZone.position.x, landingZone.position.y + 20, landingZone.position.z));
                this.transform.Translate(this.transform.rotation * new Vector3(0, 0, Time.deltaTime * speed));
            }
        }
    }
}
