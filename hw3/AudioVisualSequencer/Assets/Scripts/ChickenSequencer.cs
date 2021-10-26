using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSequencer : MonoBehaviour
{
    // chicken prefab
    public GameObject the_chickenPrefab;
    // selector prefab
    public GameObject the_selectorPrefab;

    //--------- GRAPHICS -----------
    // number of chickens (MUST match NUM_CHICKENS in ChucK code)
    int NUM_CHICKENS = 4;
    // the global chicken array
    GameObject[] m_chickens;
    // chicken spacing
    float m_chickenSpacing = .75f;
    // the chicken sequencer playhead
    GameObject m_playhead;
    // the selector thingy over the selected chicken
    GameObject m_selector;

    // reference to animator component
    private Animator m_animPlayhead;
    // time step for run animation
    private float m_animPlayheadTimeRun;
    // which chicken is selected for editing
    private int m_selectedChicken = 0;

    // --------- AUDIO -------------
    // float sync
    private ChuckFloatSyncer m_ckPlayheadPos;
    // int sync
    private ChuckIntSyncer m_ckCurrentChicken;

    // --------- SHARED ------------
    // sequence: this information is duplicated across Chuck and Unity
    // and is communicated to chuck using ChucK events
    private float[] m_seqRate;
    private float[] m_seqGain;
    // previous discrete chicken number
    private int m_previousChicken = -1;


    // Start is called before the first frame update
    void Start()
    {
        InitGraphics();
        InitAudio();
        InitSequence();
    }

    // initialize graphics
    void InitGraphics()
    {
        // instantiate chicken array
        m_chickens = new GameObject[NUM_CHICKENS];
        // offset
        float offset = m_chickenSpacing * (NUM_CHICKENS - 1);
        // make chickens
        for (int i = 0; i < NUM_CHICKENS; i++)
        {
            m_chickens[i] = Instantiate(the_chickenPrefab, new Vector3(-offset / 2 + i * m_chickenSpacing, 0, 0), Quaternion.Euler(0, 180, 0));
        }

        // instantiate the playhead
        m_playhead = Instantiate(the_chickenPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 90, 0));
        // move the playhead
        m_playhead.transform.position = new Vector3(0, 0, -.3f);
        // scale the playhead
        m_playhead.transform.localScale = new Vector3(.3f, .3f, .3f);

        // the first chicken
        GameObject c = m_chickens[0];
        // instantiate the selector
        m_selector = Instantiate(the_selectorPrefab, new Vector3(c.transform.position.x, .2f + c.GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.magnitude, c.transform.position.z), Quaternion.identity);
        // position the selector
        PositionSelector();

        // get animators
        m_animPlayhead = m_playhead.GetComponent<Animator>();
    }

    // initialize audio
    void InitAudio()
    {
        // run the sequencer
        GetComponent<ChuckSubInstance>().RunFile("chickencer.ck", true);

        // add the float sync
        m_ckPlayheadPos = gameObject.AddComponent<ChuckFloatSyncer>();
        m_ckPlayheadPos.SyncFloat(GetComponent<ChuckSubInstance>(), "playheadPos");
        // add the int sync
        m_ckCurrentChicken = gameObject.AddComponent<ChuckIntSyncer>();
        m_ckCurrentChicken.SyncInt(GetComponent<ChuckSubInstance>(), "currentChicken");
    }

    // initialize sequence
    void InitSequence()
    {
        // sequence, the rate for each element
        m_seqRate = new float[NUM_CHICKENS];
        // sequence, the volume for each element
        m_seqGain = new float[NUM_CHICKENS];
        // initialize
        for (int i = 0; i < NUM_CHICKENS; i++)
        {
            m_seqRate[i] = 1.0f;
            m_seqGain[i] = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // get input (for chicken editing)
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_selectedChicken--;
            if (m_selectedChicken < 0) m_selectedChicken = NUM_CHICKENS - 1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_selectedChicken++;
            if (m_selectedChicken >= NUM_CHICKENS) m_selectedChicken = 0;
        }
        else if (Input.GetKeyDown("left"))
        {
            AdjustRate(m_selectedChicken, true);
        }
        else if (Input.GetKeyDown("right"))
        {
            AdjustRate(m_selectedChicken, false);
        }
        else if (Input.GetKeyDown("up"))
        {
            AdjustGain(m_selectedChicken, true);
        }
        else if (Input.GetKeyDown("down"))
        {
            AdjustGain(m_selectedChicken, false);
        }

        // offset
        float offset = m_chickenSpacing * (NUM_CHICKENS - 1) + m_chickenSpacing * .4f;
        // update the playhead using info from ChucK's playheadPos
        m_playhead.transform.position = new Vector3(
            -offset / 2f + m_chickenSpacing * m_ckPlayheadPos.GetCurrentValue(),
            m_playhead.transform.position.y,
            m_playhead.transform.position.z);

        // get current step number
        int currentChicken = m_ckCurrentChicken.GetCurrentValue();
        // should eat?
        if (currentChicken != m_previousChicken)
        {
            // chicken, eat!
            Eat(currentChicken);
            // remember
            m_previousChicken = currentChicken;
        }

        // animate the playhead
        animUpdate();
        // position the selector
        PositionSelector();
    }

    void animUpdate()
    {
        m_animPlayhead.Play("Run In Place", -1, m_animPlayheadTimeRun);
        m_animPlayheadTimeRun += Time.deltaTime * 3f; // animation speed
        if (m_animPlayheadTimeRun > 1) m_animPlayheadTimeRun = 0;
    }

    // position selector
    void PositionSelector()
    {
        // the first chicken
        GameObject c = m_chickens[m_selectedChicken];
        // translate the selector
        m_selector.transform.position = new Vector3(c.transform.position.x, -.1f + 2.0f * c.GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.magnitude, c.transform.position.z);
    }

    // Eat
    void Eat(int which)
    {
        // Debug.Log("chicken:" + which);
        // eat!
        m_chickens[which].GetComponent<Animator>().Play("Eat", -1, 0f);
    }

    // AdjustRate
    void AdjustRate(int which, bool isLeft)
    {
        // ajdust
        if (!isLeft)
        {
            m_seqRate[which] *= .9f;
            if (m_seqRate[which] < .5f) m_seqRate[which] = .5f;
        }
        else
        {
            m_seqRate[which] *= 1.1f;
            if (m_seqRate[which] > 2.0f) m_seqRate[which] = 2.0f;
        }

        // scale according to rate and gain
        m_chickens[which].transform.localScale = new Vector3(1f / m_seqRate[which], m_seqGain[which], 1);

        // set which and rate and fire the event
        GetComponent<ChuckSubInstance>().SetInt("editWhich", which);
        GetComponent<ChuckSubInstance>().SetFloat("editRate", m_seqRate[which]);
        GetComponent<ChuckSubInstance>().SetFloat("editGain", m_seqGain[which]);
        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened");
    }

    // AdjustRate
    void AdjustGain(int which, bool isUp)
    {
        Debug.Log(m_chickens[0].GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents.magnitude);
        // ajdust
        if (isUp)
        {
            m_seqGain[which] *= 1.1f;
            if (m_seqGain[which] > 5f) m_seqGain[which] = 5f;
        }
        else
        {
            m_seqGain[which] *= .9f;
            if (m_seqGain[which] < .2f) m_seqGain[which] = .2f;
        }

        // scale according to rate and gain
        m_chickens[which].transform.localScale = new Vector3(1f / m_seqRate[which], m_seqGain[which], 1);

        // set which and rate and fire the event
        GetComponent<ChuckSubInstance>().SetInt("editWhich", which);
        GetComponent<ChuckSubInstance>().SetFloat("editRate", m_seqRate[which]);
        GetComponent<ChuckSubInstance>().SetFloat("editGain", m_seqGain[which]);
        GetComponent<ChuckSubInstance>().BroadcastEvent("editHappened");
    }
}
