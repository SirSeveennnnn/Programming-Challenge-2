using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteObject : MonoBehaviour
{
    private Note note = Note.SWIPE_RIGHT;    
    private Image spriteImage;

    [SerializeField] private Sprite[] gestureSprites;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
    }

    public Note GetNote()
    {
        return this.note;
    }

    public void SetNote(Note note)
    {
        this.note = note;
        Debug.Log($"note: {note}");
        spriteImage.sprite = gestureSprites[(int)note];
    }
}

public enum Note
{
    SWIPE_RIGHT = 0, SWIPE_LEFT, SWIPE_UP, SWIPE_DOWN, 
    PINCH, SPREAD,
    ROTATE_CW, ROTATE_CCW
}
