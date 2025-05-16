using UnityEngine;

public class CelebrityJeepFollower : MonoBehaviour
{
    private string[] celebrityNames = {
        "Lionel Messafari",
        "Cristiano Runnaldo",
        "Drake the Tracker",
        "MrBeastmode",
        "Kanye Nomad",
        "Pedro Paws-cal",
        "Timothée Chilla-met",
        "Jungkook of the Jungle",
        "Logan Paw",
        "Mbappé the Mover",
        "Kai Snap",
        "Tom Hyeland",
        "Ryan Roamsling",
        "Jacob Roar-di",
        "Erling Howler",
        "LeBron Ranger",
        "Speedy the Streamer",
        "Zebra Chalamet",
        "Beastie James",
        "Hasan Savanna"
    };

    private int SafeCountByTag(string tag)
    {
        try
        {
            return GameObject.FindGameObjectsWithTag(tag).Length;
        }
        catch
        {
            return 0;
        }
    }

    private string currentCelebrity;

    public TouristManager touristManager;
    public LineRenderer lineRenderer;
    public float speed = 0.5f;

    public Sprite movingSprite;
    public Sprite takingPicturesSprite;

    private SpriteRenderer sr;

    private int currentIndex = 0;
    private float t = 0f;
    private float yOffset = 0.3f;

    private float stopTimer = 0f;
    private float stopInterval = 10f;
    private float stopDuration = 6f;
    private float pauseDuration = 0f;
    private bool isPaused = false;

    private float startDelay = 5f;
    private float endDelay = 10f;
    private bool isStarting = true;
    private bool isRestarting = false;
    private float delayTimer = 0f;

    void Start()
    {
        currentCelebrity = celebrityNames[Random.Range(0, celebrityNames.Length)];
        sr = GetComponent<SpriteRenderer>();

        if (lineRenderer != null && lineRenderer.positionCount > 0)
        {
            Vector3 firstPoint = lineRenderer.GetPosition(0);
            Vector3 startPos = new Vector3(firstPoint.x, firstPoint.y + yOffset, firstPoint.z);
            transform.position = startPos;

            Vector3 nextPoint = lineRenderer.GetPosition(1);
            Vector3 direction = (nextPoint - firstPoint).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        sr.sprite = movingSprite;
        delayTimer = startDelay;
    }

    void Update()
    {
        if (lineRenderer == null || lineRenderer.positionCount < 2) return;

        if (isStarting)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0f)
            {
                isStarting = false;
            }
            return;
        }

        if (isRestarting)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0f)
            {
                isRestarting = false;
                currentIndex = 0;
                Debug.Log("Celebrity tour restarted after break");
            }
            return;
        }

        if (isPaused)
        {
            pauseDuration -= Time.deltaTime;
            if (pauseDuration <= 0f)
            {
                isPaused = false;
                sr.sprite = movingSprite;
                Debug.Log("Finished photo stop");
            }
            return;
        }

        stopTimer += Time.deltaTime;
        if (stopTimer >= stopInterval)
        {
            stopTimer = 0f;
            isPaused = true;
            pauseDuration = stopDuration;
            sr.sprite = takingPicturesSprite;
            OnPhotoTaken(); // Leave as-is
            Debug.Log("Celebrity taking photo");
            return;
        }

        Vector3 start = lineRenderer.GetPosition(currentIndex);
        Vector3 end = lineRenderer.GetPosition(currentIndex + 1);

        t += Time.deltaTime * speed / Vector3.Distance(start, end);

        Vector3 position = Vector3.Lerp(start, end, t);
        position.y += yOffset;
        transform.position = position;

        Vector3 direction = (end - start).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (t >= 1f)
        {
            t = 0f;
            currentIndex++;
            if (currentIndex >= lineRenderer.positionCount - 1)
            {
                isRestarting = true;
                delayTimer = endDelay;
                Debug.Log("Celebrity tour completed. Taking a break...");
                return;
            }
        }
    }

    void OnPhotoTaken()
    {
        if (touristManager == null || touristManager.shopScript == null)
        {
            Debug.LogWarning("TouristManager or ShopScript not assigned.");
            return;
        }

        int herbivores = SafeCountByTag("Herbivore");
        int carnivores = SafeCountByTag("Carnivore");

        int totalAnimals = herbivores + carnivores;

        // Earnings per animal: balanced = 15, unbalanced = 5
        int earningsPerAnimal = Mathf.Abs(herbivores - carnivores) > 10 ? 5 : 15;
        int earnings = totalAnimals * earningsPerAnimal;

        string message = "";
        if (totalAnimals == 0)
        {
            string[] ghostTown = {
             "I’d rather be home listening to my wife’s book club recap than be here.",
             "Honestly, traffic was more exciting than this.",
             "I cancelled lunch with my accountant for this?",
             "This made me miss family dinner. Unforgivable.",
             "Next time, just send me a brochure and save us both the effort.",
             "Even my GPS asked me why I came here.",
             "This is a ghost town. I need to go home...",
             "I don't want to be here...",
             "Are you kidding me? Is this the great desert tour?",
             "This is called The Great Reserve? My garden has more animals than this! kkkkkkkk",
             "Bro I came to see animals, not to finance your comeback story...",
             "Why am I the only one investing in this place?"

         };

            string rareRoast = ghostTown[Random.Range(0, ghostTown.Length)];
            MessageBoxUI.Instance.ShowMessage($"{currentCelebrity}: {rareRoast}");
            return;
        }
        else if (totalAnimals < 4)
        {
            string[] boring = {
             "Where are the animals? Is this a prank?",
             "Two squirrels and a rock? Really?",
             "This feels like budget wildlife.",
             "Did everyone call in sick today?",
             "Are the animals on strike or something?",
             "This tour needs a search party.",
             "Even the silence is bored.",
             "I’ve seen bus stops with more excitement.",
             "This park should come with a warning label: empty.",
             "Honestly, I thought this was the loading screen."
         };

            message = boring[Random.Range(0, boring.Length)];
            earnings = 0;
        }
        else if (herbivores == 0 && carnivores > 0)
        {
            string[] danger = {
             "This tour comes with teeth.",
             "All bite, no balance.",
             "Did I just pay to get hunted?",
             "Feels less like a tour, more like bait.",
             "Everyone’s a predator. Including the tour guide?",
             "I blinked and lost eye contact... with five of them.",
             "Pretty sure something just growled at me.",
             "Am I part of the food chain now?",
             "This park needs more fences.",
             "If this was a movie, I’d be the first to go."
         };

            message = danger[Random.Range(0, danger.Length)];
        }
        else if (carnivores == 0 && herbivores > 0)
        {
            string[] soft = {
             "Fluffy, friendly, and... that’s it.",
             "No drama, just munching.",
             "Felt like a slow petting zoo.",
             "It’s calm. A little too calm.",
             "This park runs on salad and silence.",
             "Very gentle. Like a nature nap.",
             "All bark, zero bite.",
             "That was cute. I guess.",
             "A chill vibe... maybe too chill.",
             "It's peaceful. I almost fell asleep."
         };

            message = soft[Random.Range(0, soft.Length)];
        }
        else if (herbivores >= 4 && carnivores >= 4)
        {
            string[] wow = {
             $"That’s wild. ${earnings} well earned.",
             $"Loved the chaos. Take ${earnings}.",
             $"Great combo. Sending ${earnings}.",
             $"Now we’re talking. ${earnings} is yours.",
             $"Peak safari. You earned every cent of that ${earnings}.",
             $"That’s what I flew in for. ${earnings} approved.",
             $"Perfect mix of cute and dangerous. ${earnings} delivered.",
             $"This shot’s going viral. ${earnings} well deserved.",
             $"Finally, something exciting. ${earnings} paid.",
             $"This park’s got style. Here’s ${earnings}."
         };

            message = wow[Random.Range(0, wow.Length)];
        }
        else
        {
            string[] ok = {
             $"Not bad. ${earnings} coming your way.",
             $"Could be worse. Here’s ${earnings}.",
             $"Okay-ish. You get ${earnings}.",
             $"Seen better, but fine. ${earnings}.",
             $"Mildly interesting. Pocketing ${earnings}.",
             $"Solid effort. I’ll allow ${earnings}.",
             $"Expected more, settled for this. ${earnings}.",
             $"Somewhere between boring and decent. ${earnings}.",
             $"Alright... this works. ${earnings}.",
             $"Took a photo just in case. ${earnings}."
         };

            message = ok[Random.Range(0, ok.Length)];
        }

        if (earnings > 0)
        {
            touristManager.shopScript.AddMoney(earnings);
        }

        MessageBoxUI.Instance.ShowMessage($"{currentCelebrity}: {message}");
    }


}
