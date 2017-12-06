using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Contains all settings for every agents
public class AgentsSettings : MonoBehaviour {

	// Perception radius
    [Header("Perception")]
    [SerializeField]
	private float viewRadius = 20.0f;
	public float ViewRadius {
		get {
			return viewRadius;
		}
	}

    // Perception angle
    [SerializeField]
    [Range(0,360)]
	private float viewAngle= 180.0f;
	public float ViewAngle {
		get {
			return viewAngle;
		}
	}

    [Header("Movement")]
    // Minimal distance between me and another agent
    [SerializeField]
    private float safeSpace = 1.0f;
	public float SafeSpace {
		get {
			return safeSpace;
		}
	}

	// Maximal steering force
	private float maxForce = 1.0f;
	public float MaxForce {
		get {
			return maxForce;
		}
	}

    // Maximal speed
    [SerializeField]
    private float maxSpeed = 5.0f;
	public float MaxSpeed {
		get {
			return maxSpeed;
		}
	}

    [Header("Flocking coefficients")]
    // Coeff for Align force
    [SerializeField]
    [Range(0f, 1f)]
    private float coeffA = 0.33f;
	public float CoeffA {
		get {
			return coeffA;
		}
	}

    // Coeff for Cohesion force
    [SerializeField]
    [Range(0f, 1f)]
    private float coeffC = 0.33f;
	public float CoeffC {
		get {
			return coeffC;
		}
	}

    // Coeff for Separate force
    [SerializeField]
    [Range(0f, 1f)]
    private float coeffS = 0.5f;
	public float CoeffS {
		get {
			return coeffS;
		}
	}

    // Coeff for Dodge force
    [SerializeField]
    [Range(0.0f, 1.0f)]
	private float coeffD = 0.5f;
	public float CoeffD {
		get {
			return coeffD;
		}
	}

    // Coeff for Intention
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float coeffI = 0.7f;
    public float CoeffI
    {
        get
        {
            return coeffI;
        }
    }

    // Angle for dodging
    [SerializeField]
    [Range(0, 180)]
	private float dodgingAngle = 30.0f;
	public float DodgingAngle {
		get {
			return dodgingAngle;
		}
	}

	[Range(0.0f, 1.0f)]
    [SerializeField]
    private float ratioFear = 0.7f;
	public float RatioFear {
		get {
			return ratioFear;
		}
	}

    [Header("Layers")]
	// Agents layer 
    [SerializeField]
	private LayerMask targetMask;
	public LayerMask TargetMask {
		get {
			return targetMask;
		}
	}

    // Obstacles layer
    [SerializeField]
    private LayerMask obstacleMask;
	public LayerMask ObstacleMask {
		get {
			return obstacleMask;
		}
	}

    // Doors layer
    [SerializeField]
    private LayerMask doorMask;
	public LayerMask DoorMask {
		get {
			return doorMask;
		}
	}

    // Indications layer
    [SerializeField]
    private LayerMask indicationMask;
	public LayerMask IndicationMask {
		get {
			return indicationMask;
		}
	}

    // Fire layer
    [SerializeField]
    private LayerMask fireMask;
	public LayerMask FireMask {
		get {
			return fireMask;
		}
	}

    // Checkpoint layer
    [SerializeField]
    private LayerMask checkpointMask;
    public LayerMask CheckpointMask
    {
        get
        {
            return checkpointMask;
        }
    }

    [Header("Group")]
    [SerializeField]
    private List<Agent> mesoGroup = new List<Agent>();
    public List<Agent> MyGroup
    {
        get
        {
            return mesoGroup;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

}
