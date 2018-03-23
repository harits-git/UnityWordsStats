using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace protopronunciation
{
    public class AppFormHandler : MonoBehaviour
    {
        public Text resultTxt;
        public InputField aIn, bIn;
        public void ButtonOnClick()
        {
            string dL = PronounciationMeasurements.LevenshteinDistance(aIn.text, bIn.text).ToString();
            resultTxt.text = "Levenshtein distance between: "+dL+
                "\nTotal syllables: "+PronounciationMeasurements.SyllableCount(aIn.text);
        }
    }
}
