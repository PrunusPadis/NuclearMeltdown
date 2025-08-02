using UnityEngine;

public class test : MonoBehaviour
{

    private string[] dummystrings = { "test 1", "test 2", "test 3" };
    private int textIndex = 0;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            Debug.Log("here");

            textIndex = textIndex + 1 < dummystrings.Length ? textIndex + 1 : 0;
            var text = dummystrings[textIndex];

            SubtitleManager.Instance.PlaySubtitles(text);
        }
    }
}
