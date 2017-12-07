    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;

// Reference :
// https://processing.org/examples/flocking.html

public class Agent : MonoBehaviour {

    private Rigidbody rb;
    public Rigidbody Rb {
    	get {
    		return rb;
    	}
    }

    private AgentsSettings settings;
    public AgentsSettings Settings {
    	get {
    		return settings;
    	}
    }

    private Flock flocking;
    public Flock Flocking {
    	get {
    		return flocking;
    	}
    }

    private BDI bdi;
    public BDI Bdi {
    	get {
    		return bdi;
    	}
    }




	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		settings = GetComponent<AgentsSettings>();
		flocking = new Flock(this);
		bdi = new BDI(this);
	}

    private void OnDrawGizmos()
    {
        float Radius = 1f;
        Transform t = GetComponent<Transform>();
        
        float theta = 0f;
        float x = Radius * Mathf.Cos(theta);
        float y = Radius * Mathf.Sin(theta);
        Vector3 pos = t.position + new Vector3(x, 0, y);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = Radius * Mathf.Cos(theta);
            y = Radius * Mathf.Sin(theta);
            newPos = t.position + new Vector3(x, 0, y);
            Debug.DrawLine(pos, newPos, Color.red);
            pos = newPos;
        }
        Debug.DrawLine(pos, lastPos, Color.red);
    }

    // Update is called once per frame
    void Update () {
        Vector3 reflexes = bdi.UpdateBDI();
        Debug.Log(gameObject.name + " my intention is " + (bdi.myIntention == null ? "None" : bdi.myIntention.GetType().ToString()));
        if (!reflexes.Equals(Vector3.zero) || bdi.myIntention == null)
        {
            Vector3 destination = transform.position + reflexes.normalized;
            //Debug.DrawLine(transform.position, destination, Color.black);
            rb.velocity = ((destination - transform.position).normalized) * settings.MaxSpeed;
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else
        {
            Vector3 intentionDirection = bdi.myIntention.DefaultState(this).normalized;

            intentionDirection.y = 0;
            List<Agent> neighbors = bdi.myPerception.AgentsInSight;
            List<Agent> relative = bdi.myBelief.MesoInSight(bdi.myPerception);

            Vector3 flockingDirection = (relative.Count > 0 ?
                (1 - settings.CoeffMesoFlock) * flocking.Flocking(neighbors) + settings.CoeffMesoFlock * flocking.Flocking(relative) :
                flocking.Flocking(neighbors));

            Vector3 force = (intentionDirection.Equals(Vector3.zero) ? flockingDirection : (1 - settings.CoeffI) * flockingDirection + settings.CoeffI * intentionDirection);
            Vector3 destination = transform.position + force.normalized;
            //Debug.DrawLine(transform.position, destination, Color.black);
            rb.velocity = (force.Equals(Vector3.zero) ? transform.TransformDirection(Vector3.forward) : (destination - transform.position).normalized) * settings.MaxSpeed;
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Checkpoint"))
        {
            bdi.myBelief.OnCheckpoint = collision.gameObject;
            if (bdi.myBelief.CheckedPoints.Keys.Contains(collision.gameObject))
            {
                bdi.myBelief.CheckPoint(collision.gameObject);
            }
            else
            {
                bdi.myBelief.AddCP(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag.Equals("Exit"))
        {
            gameObject.SetActive(false);
        }
    }

}

//Vector3 viewAngleA = bdi.myPerception.DirFromAngle(this, - settings.ViewAngle / 2, false);
//Vector3 viewAngleB = bdi.myPerception.DirFromAngle(this, settings.ViewAngle / 2, false);

//Debug.DrawLine(transform.position, transform.position + viewAngleA * settings.ViewRadius);
//Debug.DrawLine(transform.position, transform.position + viewAngleB * settings.ViewRadius);

//foreach(Agent a in neighbors){
//    Debug.DrawLine(transform.position, a.transform.position, Color.yellow);
//}