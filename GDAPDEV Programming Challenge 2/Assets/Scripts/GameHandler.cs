using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance
    {
        get; private set;
    }

    [Header("Setup")]
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject targetSequencePanel;
    [SerializeField] private GameObject historySequencePanel;
    [SerializeField] private TextMeshProUGUI curHPTxt;
    [SerializeField] private TextMeshProUGUI maxHPTxt;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI hiScoreTxt;
    [Header("Config")]
    public float MaxTime = 10f;
    public int MaxHP = 3;

    private List<Note> TargetSequence;
    private List<Note> HistorySequence;

    public float CurrentTime
    {
        get; private set;
    }

    private int currentHP;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        TargetSequence = new List<Note>();
        HistorySequence = new List<Note>();

        CurrentTime = MaxTime;
        currentHP = MaxHP;

        maxHPTxt.text = MaxHP.ToString();
        curHPTxt.text = currentHP.ToString();

        GetRandomSequence(1);
    }


    private void FixedUpdate()
    {
        // Count down
        CurrentTime -= Time.fixedDeltaTime;
        if (CurrentTime <= 0)
        {
            CurrentTime = MaxTime;
            // Insert code here
        }
    }


    /// <summary>
    /// Generates a new sequence for the player to follow
    /// Also clears the current notes in the history
    /// </summary>
    /// <param name="limit"> Max number of notes to generate</param>
    public void GetRandomSequence(int limit = 7)
    {
        limit = Mathf.Max(limit, 1);

        ClearTargetNotes();
        ClearHistoryNotes();

        for (int i = 0; i < limit; i++)
        {
            TargetSequence.Add((Note)Random.Range(0, 7));
        }

        SpawnTargetSequence();
    }

    /// <summary>
    /// Spawns the target sequence into the target sequence panel
    /// </summary>
    public void SpawnTargetSequence()
    {
        foreach (Note n in TargetSequence)
        {
            GameObject obj = Instantiate(notePrefab, targetSequencePanel.transform);
            obj.GetComponent<NoteObject>().SetNote(n);
        }
    }

    /// <summary>
    /// Spawns a note in the history holder and adds them to the history sequence
    /// </summary>
    /// <param name="note">Note to spawn</param>
    public void AddHistoryNote(Note note)
    {
        HistorySequence.Add(note);

        GameObject spawn = Instantiate(notePrefab);
        spawn.transform.SetParent(historySequencePanel.transform, false);
    }

    /// <summary>
    /// Removes all notes inside the history sequence panel
    /// </summary>
    public void ClearHistoryNotes()
    {
        HistorySequence.Clear();
        // Destroy all children of history sequence
        foreach (Transform childTf in historySequencePanel.GetComponentsInChildren<Transform>())
        {
            if (childTf == historySequencePanel.transform)
                continue;
            Destroy(childTf.gameObject);
        }
    }

    public void ClearTargetNotes()
    {
        TargetSequence.Clear();
        // Destroy all children of target sequence
        foreach (Transform childTf in targetSequencePanel.GetComponentsInChildren<Transform>())
        {
            if (childTf == targetSequencePanel.transform)
                continue;
            Destroy(childTf.gameObject);
        }
    }
}
