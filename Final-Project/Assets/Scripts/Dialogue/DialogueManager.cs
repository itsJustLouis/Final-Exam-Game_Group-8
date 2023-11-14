using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI dialogueText;
    [SerializeField]TextMeshProUGUI nextbutton;
    Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartTutorial(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        if(sentences.Count == 1)
        {
            nextbutton.text = "START GAME";
        }
        else
        {
            nextbutton.text = "NEXT";
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        SceneManager.LoadScene("IntroCutScene");
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void SkipDialogue()
    {
        SceneManager.LoadScene("Main-Scene");
    }
}
