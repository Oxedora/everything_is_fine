/* Copyright (c) 2016-2017 Lewis Ward
// Fire Propagation System
// author: Lewis Ward
// date  : 10/02/2017
*/
using UnityEngine;
using System.Collections;

public class FireIgniter : MonoBehaviour {
    [SerializeField][Tooltip("Width of the fire grid, fire starts in the center of the grid")]
    private int m_gridWidth = 10;
    [SerializeField][Tooltip("Height of the fire grid, fire starts in the center of the grid")]
    private int m_gridHeight = 10;
    [SerializeField][Tooltip("Prefab of the fire to use")]
    private GameObject m_firePrefab;
    [SerializeField][Tooltip("Delete this GameObject when there is a collision with it and the terrain or another GameObject?")]
    private bool m_destroyOnCollision = false;
    private bool m_fireIgnited = false;
    public bool fireIgnited
    {
        get { return m_fireIgnited; }
        set { m_fireIgnited = value; }
    }

    private FireGrid grid;
    private FMODUnity.StudioEventEmitter emitter;

    // Use this for initialization
    void Awake () {
        if(m_firePrefab == null)
        {
            Debug.LogError("No Fire Prefab set on Fire Igniter.");
        }

        // negate negative values
        if (m_gridWidth < 0)
            m_gridWidth = -m_gridWidth;
        if (m_gridHeight < 0)
            m_gridHeight = -m_gridHeight;

        // valid size grid
        if (m_gridWidth == 0)
            m_gridWidth = 1;
        if (m_gridHeight == 0)
            m_gridHeight = 1;
    }

    // brief Call this once a GameObject has detected a collision
    public void OnCollision()
    {
        GameObject fireGrid = new GameObject();
        fireGrid.name = "FireGrid";
        grid = fireGrid.AddComponent<FireGrid>();
        fireGrid.AddComponent<FireGrassRemover>();

        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        if (emitter == null)
        {
            Debug.Log("Unable to create FMOD_StudioEventEmitter on " + gameObject.name);
        }

        grid.IgniterUpdate(m_firePrefab, gameObject.transform.position, m_gridWidth, m_gridHeight);
    }

    // brief On collision
    // param Collision
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with "+collision.gameObject.name);
        if (m_fireIgnited == false)
        {
            OnCollision();
            m_fireIgnited = true;

            if (m_destroyOnCollision)
                Destroy();
        }
    }

    private void Update()
    {
        if(emitter != null && grid != null)
        {
            float nbCell = m_gridHeight * m_gridWidth;
            float percentageLit = (float)grid.CellsLit / nbCell;
            float value = percentageLit * 50;
            Debug.Log("PercentageLit : " + value);
            emitter.SetParameter("Strength", value);
            emitter.OverrideMinDistance = grid.CellsLit;
            emitter.OverrideMaxDistance = emitter.OverrideMinDistance + 49f;
        }
        
    }

    // brief Destroy this object
    void Destroy()
    {
        Destroy(gameObject);
    }
}
