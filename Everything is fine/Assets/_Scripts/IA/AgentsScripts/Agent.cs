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

    private bool isLit = false;
    public bool IsLit
    {
        get { return isLit; }
        set { isLit = value; }
    }


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		settings = GetComponent<AgentsSettings>();
		flocking = new Flock(this);
		bdi = new BDI(this);
	}

    // Update is called once per frame
    void Update () {
        if (!isLit)
        {
            Vector3 reflexes = bdi.UpdateBDI();
            //Debug.Log(gameObject.name + " my intention is " + (bdi.myIntention == null ? "None" : bdi.myIntention.GetType().ToString()));
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
        else if(collision.gameObject.tag.Equals("Fire") && !isLit)
        {
            IsLit = true;
            Dying();
        }
    }

    private void Dying()
    {
        if(isLit)
        {
            SpreadFear();
            gameObject.GetComponent<MeshRenderer>().material = settings.burnedColor;
            gameObject.layer = ToLayer(settings.ObstacleMask);
            rb.constraints = RigidbodyConstraints.None;
            rb.MoveRotation(Quaternion.AngleAxis(90f, new Vector3(1, 0, 0)));

            if(transform.childCount > 0)
            {
                transform.Find("feeling_aura").gameObject.SetActive(false);
            }
        }
    }

    private void SpreadFear()
    {
        Collider[] agentsInRadius = Physics.OverlapSphere(transform.position, settings.ViewRadius, settings.TargetMask);
        foreach(Collider c in agentsInRadius)
        {
            Agent a = c.GetComponent<Agent>();
            if(c != null)
            {
                if(a.Bdi.myPerception.AgentsInSight.Contains(this))
                {
                    if(a.Bdi.myBelief.MyGroup.Group.Keys.ToList().Contains(this))
                    {
                        a.Bdi.myFeelings.Fear += 0.2f;
                    }
                    else
                    {
                        a.Bdi.myFeelings.Fear += 0.1f;
                    }
                }
            }
        }
    }

    public static int ToLayer(int bitmask)
    {
        int result = bitmask > 0 ? 0 : 31;
        while (bitmask > 1)
        {
            bitmask = bitmask >> 1;
            result++;
        }
        return result;
    }
}
