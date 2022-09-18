using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    int curNoteSets = 0;
    public bool loadFromFile;
    public List<Notes> notes;
    // Start is called before the first frame update
    void Start()
    {
        if (loadFromFile)
        {
            LoadNotes();
        }
        // Check number of saved notes

        // Generate list of notes

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNotes()
    {
        int size = PlayerPrefs.GetInt(curNoteSets + " num");
        for (int i = 0; i < size; i++)
        {
            // Load note
            Notes note = new Notes();
            note.text = PlayerPrefs.GetString(curNoteSets + " " + i);
            notes.Add(note);
        }

    }

    public void SaveNotes()
    {
        // Save size
        PlayerPrefs.SetInt(curNoteSets + " num", notes.Count);
        for (int i = 0; i < notes.Count; i++)
        {
            // Save notes
            PlayerPrefs.SetString(curNoteSets + " " + i, notes[i].text);
        }
    }
}
