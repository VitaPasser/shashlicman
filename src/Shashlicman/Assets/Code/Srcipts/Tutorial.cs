using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text tutorialText;
    public GameObject textBackground;
    public AudioClip[] dialogueAudioClips;

    private GameObject backObject;
    private AudioSource audioSource;
    private bool isDialoguePlaying = false;

    private string[] tutorialMessages = {
        "Hallo mein Freund. Сьогодні я навчу тебе грати в шашличок",
        "Роздупляйся швидко, а не то помреш",
        "Ходи на WASD, бий на ЛКМ, дешся на ПКМ",
        "Раджу збирати моральні бустери та ліки, бо буде складно",
        "Viel Glück"
    };

    private int currentMessageIndex = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (tutorialText != null)
        {
            backObject = Instantiate(textBackground, new Vector3(0, -4.5f, 0), Quaternion.identity);
            tutorialText.text = tutorialMessages[currentMessageIndex];
            tutorialText.gameObject.SetActive(true);
            isDialoguePlaying = true;
            PlayDialogueAudio();
        }
        UpdateTextBackground();
    }

    void Update()
    {
        if (isDialoguePlaying && !audioSource.isPlaying)
        {
            StartCoroutine(WaitAndShowNextMessage());
        }
    }

    IEnumerator WaitAndShowNextMessage()
    {
        isDialoguePlaying = false;
        yield return new WaitForSeconds(1f);
        ShowNextMessage();
    }

    void ShowNextMessage()
    {
        if (tutorialText != null)
        {
            currentMessageIndex++;
            if (currentMessageIndex < tutorialMessages.Length)
            {
                tutorialText.text = tutorialMessages[currentMessageIndex];
                isDialoguePlaying = true;
                PlayDialogueAudio();
            }
            else
            {
                tutorialText.gameObject.SetActive(false);
                Destroy(backObject);
            }
            UpdateTextBackground();
        }
    }

    void PlayDialogueAudio()
    {
        if (currentMessageIndex < dialogueAudioClips.Length)
        {
            audioSource.clip = dialogueAudioClips[currentMessageIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip found for current message index: " + currentMessageIndex);
        }
    }

    void UpdateTextBackground()
    {
        if (textBackground != null)
        {
            textBackground.SetActive(!string.IsNullOrEmpty(tutorialText.text));
        }
    }
}
