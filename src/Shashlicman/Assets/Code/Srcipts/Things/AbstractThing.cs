using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    • Речі, які можливо підібрати +
     ◦ підсилювач шампура +
        ◦ він звеличує урон по противникам, але зменшує швидкість атаки +
     ◦ (які звеличують мораль) гашиш, смачна їжа, алкоголь, самовозвеличення,
    красиві речі, гординя, гнів, ідоли, романтизм, музика, священні писання +
        ◦ З часом, деякі елементи потрібно буде потребляти більше, щоб
    отримувати такий же ефект +
*/
public abstract class AbstractThing : MonoBehaviour
{
    /*private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;*/
    private BoxCollider2D boxCollider;
    [SerializeField] private GameObject audioSourcePrefab;
    [SerializeField] private AudioClip take_audioClips;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponentInChildren<BoxCollider2D>();
        /*spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;*/
    }

    // Update is called once per frame
    void Update()
    {
        take();
    }

    void take()
    { 
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.minDepth = 0;
        filter2D.maxDepth = 1;
        boxCollider.OverlapCollider(filter2D, colliders);
        colliders.RemoveAll(item => item.GetComponent<Player>() == null);

        if (colliders.Count > 0 )
        {
            GameObject.Destroy(gameObject);
            changeStatePlayer(colliders[0].gameObject.GetComponent<Player>());

            GameObject gaudiosource_ = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
            AudioSource audiosource = gaudiosource_.GetComponent<AudioSource>();
            audiosource.clip = take_audioClips;
            audiosource.Play();
            Debug.Log("Взял");
        }
        
    }

    public virtual void changeStatePlayer(Player player)
    {
        // realisation
    }
}
