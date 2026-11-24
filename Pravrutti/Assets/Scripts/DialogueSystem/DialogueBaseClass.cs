using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished { get; private set; }
        protected IEnumerator WriteText(string input, Text textHolder, AudioClip sound, float delayBetweenLines)
        {
            for(int i=0; i<input.Length; i++)
            {
                textHolder.text += input[i];
                SoundManager.instance.PlaySound(sound);
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitUntil(() => Keyboard.current[Key.Space].wasPressedThisFrame);

            finished = true;
        }
    }
}