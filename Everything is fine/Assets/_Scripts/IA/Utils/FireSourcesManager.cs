using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireSourcesManager : MonoBehaviour {

    private Dictionary<GameObject, Vector3> fireSources = new Dictionary<GameObject, Vector3>();
    [SerializeField]
    private float initialCooldown = 5.0f;
    private float cooldown;
    private bool fireActivated = false;

	// Use this for initialization
	void Start () {
        cooldown = initialCooldown;

        foreach(Transform child in transform)
        {
            if(child.tag.Equals("FireSource"))
            {
                fireSources.Add(child.gameObject, child.transform.position);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!fireActivated)
        {
            cooldown -= Time.deltaTime;

            if (cooldown < 0)
            {
                FireSourceRandomActivator();
                fireActivated = true;
            }
        }
	}

    private List<GameObject> GetDisableFireSources()
    {
        List<GameObject> disabledFireSources = new List<GameObject>();

        foreach(GameObject fs in fireSources.Keys)
        {
            if(!fs.activeInHierarchy)
            {
                disabledFireSources.Add(fs);
            }
        }

        return disabledFireSources;
    }

    private void FireSourceRandomActivator()
    {
        List<GameObject> disabledFireSource = GetDisableFireSources();

        if(disabledFireSource.Count > 0)
        {
            int fireSelected = Random.Range(0, disabledFireSource.Count);
            disabledFireSource[fireSelected].SetActive(true);

            Debug.Log("Activated " + disabledFireSource[fireSelected].name);
        }
    }

    public void ResetFireSources()
    {
       List<GameObject> fSources = fireSources.Keys.ToList();
       foreach(GameObject fire in fSources)
       {
            fire.transform.position = fireSources[fire];
            fire.GetComponent<FireIgniter>().fireIgnited = false;
            fire.SetActive(false);
       }

        List<GameObject> fireGrids = GameObject.FindGameObjectsWithTag("FireGrid").ToList();

        foreach(GameObject fg in fireGrids)
        {
            Destroy(fg);
        }

        cooldown = initialCooldown;
        fireActivated = false;
    }
}
