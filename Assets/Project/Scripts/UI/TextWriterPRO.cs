using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    public class TextWriterPRO : MonoBehaviour
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

        private int currentElementIndex = 0;

        private void Start()
        {
            StartCoroutine(WriteTextElements());
        }

        private IEnumerator WriteTextElements()
        {
            while (true)
            {
                for (int i = 0; i < textElements.Count; i++)
                {
                    ActivateTextElement(i);
                    yield return StartCoroutine(WriteText(textElements[i]));
                    yield return new WaitForSeconds(timeBetweenSentences);
                    DeactivateTextElement(i);
                }
            }
        }

        private void ActivateTextElement(int index)
        {
            for (int i = 0; i < textElements.Count; i++)
            {
                if (i == index)
                    textElements[i].textComponent.gameObject.SetActive(true);
                else
                    textElements[i].textComponent.gameObject.SetActive(false);
            }
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

            for (int i = 0; i <= textToWrite.Length; i++)
            {
                textComponent.text = textToWrite.Substring(0, i);
                yield return new WaitForSeconds(timeToWrite);
            }
        }
    }
}
