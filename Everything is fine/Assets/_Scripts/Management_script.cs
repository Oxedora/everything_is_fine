using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Management_script : MonoBehaviour {

    public int time;
    public float timeElapsed = 0f;
	public int budget;

    private int initialBudget;
    public int InitialBudget
    {
        get { return initialBudget; }
    }

	public Text text_budget;
    public Text text_escapedAgent;
    public Text text_time;

    public GameObject bottom, playInfo, levelInfo, endGamePanel;
    public AgentsPositionManager agentsPos;

    public FireSourcesManager fireSourceMan;
    public List<GameObject> objects_signalisation;
    GameObject target;
    RaycastHit hitInfo;

    public AudioClip buildMusic, simMusic, siren;
    private AudioSource musique, audio;
    public void SetMusique(bool isLaunched)
    {
        musique.Stop();
        musique.clip = (isLaunched ? simMusic : buildMusic);
        musique.Play();
    }

    public bool endGame = false;

    // Use this for initialization
    void Start () {
        initialBudget = budget;
		text_budget.text = budget + " €";
        objects_signalisation = new List<GameObject>();
        audio = GetComponent<AudioSource>();
        if(audio != null)
        {
            audio.volume = GameVariables.volSound;
        }

        musique = gameObject.AddComponent<AudioSource>();
        musique.loop = true;
        musique.clip = buildMusic;
        musique.volume = GameVariables.volMusic;
        musique.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if(!endGame)
        {
            if (agentsPos.gameObject.activeInHierarchy)
            {
                text_escapedAgent.text = agentsPos.NbSafeAgent + " / " + agentsPos.TotalAgents;
            }

            if (Input.GetMouseButtonDown(1))
            {
                target = ReturnClickedPos(out hitInfo);
                deleteItem(target, 10);
            }
        }
    }

	public void addItem(GameObject item, int price, Vector3 pos, Vector3 norm){
		if (norm.y == 0) {
            Debug.Log(budget - price < 0);
            if(budget >= 0 && (budget - price) < 0)
            {
                if(audio != null)
                {
                    audio.Play();
                }
            }
			budget -= price;
            text_budget.color = (budget < 0 ? Color.red : Color.green);
			text_budget.text = budget + " €";
            Debug.Log("forward target " + target.transform.localRotation+ " quaternion identity " + Quaternion.identity);
			Vector3 rot = item.transform.eulerAngles;
			if (norm.z != 0) {
				rot.y =(-90 * norm.z) + item.transform.eulerAngles.y;
			} else if (norm.x == -1) {
				rot.y = 180 + item.transform.eulerAngles.y;
			}

			pos.y = 2.5f;
			GameObject clone = GameObject.Instantiate (item,pos,Quaternion.Euler(rot));

			Debug.Log (norm);
            objects_signalisation.Add(clone);

        }
	}

    public void deleteItem(GameObject item, int price){
        if (objects_signalisation.Contains(item)){
            objects_signalisation.Remove(item);
            GameObject.Destroy(item);
            budget += price;
            text_budget.color = (budget < 0 ? Color.red : Color.green);
            text_budget.text = budget + " €";
        }
	}


    public GameObject ReturnClickedPos(out RaycastHit hit)
    {
		int layer = LayerMask.GetMask ("Obstacles","Indications");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray.origin, ray.direction * 10, out hit,Mathf.Infinity,layer))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }

    public void EndGamePanel(int nbSafeAgent, int totalAgents)
    {
        levelInfo.SetActive(!endGame);
        musique.Stop();
        musique.clip = siren;
        musique.loop = false;
        musique.Play();
        endGamePanel.SetActive(endGame);
        endGamePanel.GetComponent<EndGameScript>().SetValues(nbSafeAgent, totalAgents, initialBudget, budget, timeElapsed);
    }

    public void Restart()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void backMenu()
    {
        SceneManager.LoadScene("Main_menu");
    }
}
