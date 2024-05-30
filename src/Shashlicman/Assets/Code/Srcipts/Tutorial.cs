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
    public bool enabled_tutorial = true;

    AudioSource soundtrack;
    public float MaxVolume = 0.8f;
    public float MinVolume = 0.1f;


    private string[] tutorialMessages = {
        "Hallo mein Freund. Сьогодні я навчу тебе грати в шашличок",
        "Роздупляйся швидко, або помреш",
        "Ходи на WASD, удар на ЛКМ, дешся на ПКМ",
        "Раджу збирати моральні бустери та ліки, бо буде складно",
        "Viel Gluck!"
    };

    private int currentMessageIndex = 0;

    void Start()
    {
        AudioSource soundtrack = GameObject.FindWithTag("SoundTrack").GetComponent<AudioSource>();
        soundtrack.volume = MinVolume;

        //player = GameObject.FindWithTag("Player").GetComponent<Player>();
        audioSource = gameObject.AddComponent<AudioSource>();
        if (enabled_tutorial == true)
        {
            backObject = Instantiate(textBackground, new Vector3(0, -4.5f, 0), Quaternion.identity);
            tutorialText.text = tutorialMessages[currentMessageIndex];
            tutorialText.gameObject.SetActive(true);
            isDialoguePlaying = true;
            enabled_tutorial = false;
            PlayDialogueAudio();
            UpdateTextBackground();
        } else
        {
            soundtrack.volume = MaxVolume;
        }
    }

    void Update()
    {
        if (isDialoguePlaying && !audioSource.isPlaying)
        {
            StartCoroutine(WaitAndShowNextMessage());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StopTutorial();
        }
    }

    void StopTutorial()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        tutorialText.gameObject.SetActive(false);
        if (backObject != null)
        {
            Destroy(backObject);
        }
        isDialoguePlaying = false;
        enabled_tutorial = false;
        AudioSource soundtrack = GameObject.FindWithTag("SoundTrack").GetComponent<AudioSource>();
        soundtrack.volume = MaxVolume;
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
                StopTutorial();
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
