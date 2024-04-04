using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private List<TextWriterSingle> textWriterSingleList;

    private static TextWriter instance;
    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static TextWriterSingle AddWriter_Static(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd)
    {
        if(removeWriterBeforeAdd)
        {
            instance.RemoveWriter(uiText);
        }
        return instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters);
    }

    public TextWriterSingle AddWriter(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters);
        textWriterSingleList.Add(new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters));
        return textWriterSingle;
    }

    public static void RemoveWriter_Static(Text uiText)
    {
        instance.RemoveWriter(uiText);
    }
    private void RemoveWriter(Text uiText)
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            if(textWriterSingleList[i].GetUIText() == uiText)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }

        }

    }

    private void Update()
    {
        //Debug.Log(textWriterSingleList.Count);

        for(int i = 0; i < textWriterSingleList.Count; i++)
        {

            bool destroyInstance = textWriterSingleList[i].Update();
            if(destroyInstance)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }

        }
        
        
    }



    public class TextWriterSingle
    {
        private Text uiText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;

        public TextWriterSingle(Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
        {
            
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            this.characterIndex = 0;
        }

        //returns true on is complete

        public bool Update()
        {
            if(this.uiText)
            timer -= Time.deltaTime;
            while(timer <= 0)
            {
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                
                if(invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }
                uiText.text = text;

                if(characterIndex >= textToWrite.Length)
                {
                    uiText = null;
                    return true; ;
                }
            }
            
            return false;
        }
        public Text GetUIText()
        {
            return uiText;
        }

        public bool IsActive()
        {
            return characterIndex < textToWrite.Length;
        }

        public void WriteAllAndDestroy()
        {
            uiText.text = textToWrite;
            characterIndex = textToWrite.Length;
            TextWriter.RemoveWriter_Static(uiText);
        }
    
    
    
    }







}

