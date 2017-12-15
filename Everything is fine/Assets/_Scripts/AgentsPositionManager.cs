using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentsPositionManager : MonoBehaviour {

    private Dictionary<Agent, Vector3> agentsOnStage = new Dictionary<Agent, Vector3>();
    public int TotalAgents
    {
        get { return agentsOnStage.Keys.Count; }
    }

    private int nbSafeAgent = 0;
    public int NbSafeAgent
    {
        get { return nbSafeAgent; }
        set { nbSafeAgent = value; }
    }

    private int nbDeadAgent = 0;
    public int NbDeadAgent
    {
        get { return nbDeadAgent; }
        set { nbDeadAgent = value; }
    }

    // Use this for initialization
    void Awake () {

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
        nbDeadAgent = 0;
        nbSafeAgent = 0;
        List<Agent> agents = agentsOnStage.Keys.ToList();
        foreach(Agent a in agents)
        {
            a.ResetAgent(agentsOnStage[a]);
            a.gameObject.SetActive(true);
        }
    }
}
