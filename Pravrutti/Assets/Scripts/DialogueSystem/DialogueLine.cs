using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;
        [SerializeField] private string input;

        [Header("Time parameters")]
        [SerializeField] private float delayBetweenLines;

        [Header("Sound")]
        [SerializeField] private AudioClip sound;

        [Header("Background")]
        [SerializeField] private Image image;
        private void Awake()
        {
            textHolder = GetComponent<Text>();
        }
        private void Start()
        {
            StartCoroutine(WriteText(input, textHolder, sound, delayBetweenLines));
        }
    }
}