using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentsPositionManager : MonoBehaviour {

    public Dictionary<Agent, Vector3> agentsOnStage;
	// Use this for initialization
	void Start () {
        agentsOnStage = new Dictionary<Agent, Vector3>();

        if(transform.childCount > 0)
        {
            foreach(Transform t in transform)
            {
                Agent a = t.GetComponent<Agent>();
                if(a != null)
                {
                    agentsOnStage.Add(a, t.position);
                }
                else
                {
                    Debug.Log("Unable to find Agent script on " + t.name);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartAgents()
    {
        List<Agent> agents = agentsOnStage.Keys.ToList();
        foreach(Agent a in agents)
        {
            a.ResetAgent(agentsOnStage[a]);
            a.gameObject.SetActive(true);
        }
    }
}
