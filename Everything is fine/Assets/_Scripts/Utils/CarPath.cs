using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
    
public class CarPath : MonoBehaviour {
       
    public List<Transform> points;
    public Transform currentPoint;
    private UnityEngine.AI.NavMeshAgent agent;
       
    void Start () {
    	NavMeshHit closestHit;
    	if( NavMesh.SamplePosition(this.gameObject.transform.position, out closestHit, 500, NavMesh.AllAreas ) ){
  			this.gameObject.transform.position = closestHit.position;
		}
		agent = GetComponent<NavMeshAgent>();
        agent.destination = currentPoint.position;
    }

    void Update() {
    	float dist = Vector3.Distance(transform.position, currentPoint.position);
    	if(dist < 2.0f){
    		currentPoint = NextOf(points, currentPoint);
    		agent.destination = currentPoint.position;
    	}
    }
    
    public static Transform NextOf(List<Transform> points, Transform item)
    {
    	return points[(points.IndexOf(item) + 1) == points.Count ? 0 : (points.IndexOf(item) + 1)];
    }
}
