using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    public class TextWriterMAX : MonoBehaviour
    {
        [System.Serializable]
        public class TextElementData
        {
            public Text textComponent;
            public string textToWrite;
            public float timeToWrite;
        }

        [Header("Text Writer")]
        [SerializeField] private List<TextElementData> textElements = new List<TextElementData>();
        [SerializeField] private float timeBetweenSentences = 2f;

        private Coroutine writingCoroutine;
        private bool isWriting;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartWriting();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StopWriting();
            }
        }

        private void StartWriting()
        {
            if (!isWriting)
            {
                writingCoroutine = StartCoroutine(WriteTextElements());
            }
        }

        private void StopWriting()
        {
            if (isWriting)
            {
                if (writingCoroutine != null)
                {
                    StopCoroutine(writingCoroutine);
                }
                DeactivateAllTextElements();
                isWriting = false;
            }
        }

        private void DeactivateAllTextElements()
        {
            foreach (TextElementData elementData in textElements)
            {
                elementData.textComponent.gameObject.SetActive(false);
            }
        }

        private IEnumerator WriteTextElements()
        {
            isWriting = true;

            for (int i = 0; i < textElements.Count; i++)
            {
                ActivateTextElement(i);
                yield return StartCoroutine(WriteText(textElements[i]));
                yield return new WaitForSeconds(timeBetweenSentences);
                DeactivateTextElement(i);
            }

            isWriting = false;
        }

        private void ActivateTextElement(int index)
        {
            DeactivateAllTextElements();

            textElements[index].textComponent.gameObject.SetActive(true);
            textElements[index].textComponent.text = string.Empty;
        }

        private void DeactivateTextElement(int index)
        {
            textElements[index].textComponent.gameObject.SetActive(false);
        }

        private IEnumerator WriteText(TextElementData elementData)
        {
            Text textComponent = elementData.textComponent;
            string textToWrite = elementData.textToWrite;
            float timeToWrite = elementData.timeToWrite;

            float timer = 0f;
            int currentIndex = 0;

            while (currentIndex < textToWrite.Length)
            {
                timer += Time.deltaTime;
                float timePerCharacter = timeToWrite / textToWrite.Length;

                if (timer >= timePerCharacter)
                {
                    timer -= timePerCharacter;
                    currentIndex++;
                    textComponent.text = textToWrite.Substring(0, currentIndex);
                }

                yield return null;
            }
        }
    }
}

