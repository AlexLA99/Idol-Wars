using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool sakura;     //5
    public bool leafa;      //4
    public bool walky;      //3
    public bool shinobu;    //2
    public bool hoomie;     //1

    public GameObject hoomieImage;
    public GameObject shinobuImage;
    public GameObject walkyImage;
    public GameObject leafaImage;
    public GameObject sakuraImage;

    public int shinobuStats = 647;
    public int shinobuHP = 360;
    public float shinobuAbilityDuration = 5f;
    public float shinobuUltimateDuration = 5f;
    public float shinobuMultiplier = 2.5f;
    public int shinobuAbilityCooldown = 12;
    public int shinobuUltimateCooldown = 15;

    public int hoomieStats = 616;
    public int hoomieHP = 336;
    public float hoomieAbilityDuration = 5f;
    public float hoomieUltimateDuration = 3f;
    public float hoomieMultiplier = 2f;
    public float hoomieAbilityCooldown = 1150000f;
    public int hoomieUltimateCooldown = 20;

    public int walkyStats = 651;
    public int walkyHP = 356;
    public float walkyAbilityDuration = 4f;
    public float walkyUltimateDuration = 3f;
    public int walkyHeal = 10;
    public int walkyAbilityCooldown = 10;
    public int walkyUltimateCooldown = 20;

    public int leafaStats = 609;
    public int leafaHP = 340;
    public float leafaAbilityDuration = 5f;
    public float leafaUltimateDuration = 5f;
    public int leafaHeal = 3;
    public float leafaAbilityCooldown = 1050000f;
    public int leafaUltimateCooldown = 21;

    public int sakuraStats = 611;
    public int sakuraHP = 343;
    public float sakuraAbilityDuration = 5f;
    public float sakuraUltimateDuration = 3f;
    public float sakuraMultiplier = 2f;
    public int sakuraHeal = 5;
    public float sakuraAbilityCooldown = 1150000f;
    public int sakuraUltimateCooldown = 20;

    public bool abilityWorking = false, ultimateWorking = false;

    private int perfectHitInRow = 0, perfectHitsAbilities = 0, perfectGreatHitsInRow = 0;

    private float scoreAbilities = 0;

    private float timerUltimate = 0, timerAbility = 0;

    public bool canUseAbility = false;
    public bool canUseUltimate = false;

    public KeyCode abilityKey, ultimateKey;

    public int overallStats = 1;

    public GameObject abilityOff, abilityAvailable, abilityOn, ultimateOff, ultimateAvailable, ultimateOn;


    public AudioSource theMusic;
    
    public bool startPlaying;
    
    public BeatScroller theBS;

    public static GameManager instance;

    public float currentScore;
    public int scorePerGoodNote = 5;
    public int scorePerGreatNote = 20;
    public int scorePerPerfectNote = 50;

    public int maxHealth = 1;
    public int currentHealth;
    public float bossMaxHealth;
    public float bossCurrentHealth;
    public HealthBar healthBar;
    public BossHealthBar bossHealthBar;

    public int badDamage = 30;
    public int missDamage = 70;

    public float currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public int combo = 0;

    public Text scoreText;
    public Text multiText;
    public Text comboText;

    public float totalNotes;
    public float badHits;
    public float greatHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject bullets;

    public GameObject resultsScreen;
    public Text percentHitText, badsText, greatsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    public bool first = true;
    private bool failed = false;

    public float totalTime;
    public float currentTime = 0;

    public GameObject startMenu, pauseMenu;

    public int sScore;
    public int aScore;
    public int bScore;
    public int cScore;
    public int dScore;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        if(PlayerPrefs.GetInt("characterID") == 1)
        {
            hoomie = true;
            shinobu = false;
            walky = false;
            leafa = false;
            sakura = false;

            hoomieImage.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("characterID") == 2)
        {
            hoomie = false;
            shinobu = true;
            walky = false;
            leafa = false;
            sakura = false;

            shinobuImage.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("characterID") == 3)
        {
            hoomie = false;
            shinobu = false;
            walky = true;
            leafa = false;
            sakura = false;

            walkyImage.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("characterID") == 4)
        {
            hoomie = false;
            shinobu = false;
            walky = false;
            leafa = true;
            sakura = false;

            leafaImage.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("characterID") == 5)
        {
            hoomie = false;
            shinobu = false;
            walky = false;
            leafa = false;
            sakura = true;

            sakuraImage.SetActive(true);
        }

        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        comboText.text = "Combo: 0";

        totalNotes = FindObjectsOfType<NoteObject>().Length;

        totalTime = theMusic.clip.length;

        bossMaxHealth = totalTime;
        bossCurrentHealth = bossMaxHealth;
        bossHealthBar.SetMaxHealth(bossMaxHealth);

        if (shinobu)
        {
            maxHealth = shinobuHP;
            overallStats = shinobuStats;

        }
        else if(hoomie)
        {
            maxHealth = hoomieHP;
            overallStats = hoomieStats;
        }
        else if(walky)
        {
            overallStats = walkyStats;
            maxHealth = walkyHP;
        }
        else if (leafa)
        {
            overallStats = leafaStats;
            maxHealth = leafaHP;
        }
        else if (sakura)
        {
            overallStats = sakuraStats;
            maxHealth = sakuraHP;
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                startMenu.SetActive(false);

                theMusic.Play();
            }

        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                theBS.paused = true;
                theMusic.Pause();
                pauseMenu.SetActive(true);
            }
            if (!theBS.paused)
            {
                if (abilityWorking)
                {
                    abilityOn.SetActive(true);
                    abilityAvailable.SetActive(false);
                    abilityOff.SetActive(false);
                }
                else if (canUseAbility)
                {
                    abilityOn.SetActive(false);
                    abilityAvailable.SetActive(true);
                    abilityOff.SetActive(false);
                }
                else
                {
                    abilityOn.SetActive(false);
                    abilityAvailable.SetActive(false);
                    abilityOff.SetActive(true);
                }

                if (ultimateWorking)
                {
                    ultimateOn.SetActive(true);
                    ultimateAvailable.SetActive(false);
                    ultimateOff.SetActive(false);
                }
                else if (canUseUltimate)
                {
                    ultimateOn.SetActive(false);
                    ultimateAvailable.SetActive(true);
                    ultimateOff.SetActive(false);
                }
                else
                {
                    ultimateOn.SetActive(false);
                    ultimateAvailable.SetActive(false);
                    ultimateOff.SetActive(true);
                }

                if (shinobu)
                {
                    if (perfectHitsAbilities >= shinobuAbilityCooldown)
                    {
                        canUseAbility = true;
                    }
                    if (perfectGreatHitsInRow >= shinobuUltimateCooldown)
                    {
                        canUseUltimate = true;
                    }
                    if (canUseAbility && Input.GetKeyDown(abilityKey))
                    {
                        canUseAbility = false;
                        timerAbility = shinobuAbilityDuration;
                        abilityWorking = true;
                    }
                    if (abilityWorking)
                    {
                        perfectHitsAbilities = 0;
                        currentMultiplier = shinobuMultiplier;
                        timerAbility -= Time.deltaTime;
                        if (timerAbility <= 0.0f)
                        {
                            timerAbility = 0;
                            abilityWorking = false;
                            currentMultiplier = 1;
                        }
                    }
                    if (canUseUltimate && Input.GetKeyDown(ultimateKey))
                    {
                        canUseUltimate = false;
                        ultimateWorking = true;
                        timerUltimate = shinobuUltimateDuration;
                    }
                    if (ultimateWorking)
                    {
                        perfectGreatHitsInRow = 0;
                        timerUltimate -= Time.deltaTime;
                        if (timerUltimate <= 0.0f)
                        {
                            timerUltimate = 0;
                            ultimateWorking = false;
                        }
                    }
                }
                else if (hoomie)
                {
                    if (scoreAbilities >= hoomieAbilityCooldown)
                    {
                        canUseAbility = true;
                    }
                    if (perfectHitsAbilities >= hoomieUltimateCooldown)
                    {
                        canUseUltimate = true;
                    }
                    if (canUseAbility && Input.GetKeyDown(abilityKey) && !abilityWorking)
                    {
                        canUseAbility = false;
                        timerAbility = hoomieAbilityDuration;
                        abilityWorking = true;
                    }
                    if (abilityWorking)
                    {
                        scoreAbilities = 0;
                        currentMultiplier = hoomieMultiplier;
                        timerAbility -= Time.deltaTime;
                        if (timerAbility <= 0.0f)
                        {
                            timerAbility = 0;
                            abilityWorking = false;
                            currentMultiplier = 1;
                        }
                    }
                    if (canUseUltimate && Input.GetKeyDown(ultimateKey))
                    {
                        canUseUltimate = false;
                        ultimateWorking = true;
                        timerUltimate = hoomieUltimateDuration;
                    }
                    if (ultimateWorking)
                    {
                        perfectHitsAbilities = 0;
                        timerUltimate -= Time.deltaTime;
                        if (timerUltimate <= 0.0f)
                        {
                            timerUltimate = 0;
                            ultimateWorking = false;
                        }
                    }
                }
                else if (walky)
                {
                    if (perfectGreatHitsInRow >= walkyAbilityCooldown)
                    {
                        canUseAbility = true;
                    }
                    if (perfectHitsAbilities >= walkyUltimateCooldown)
                    {
                        canUseUltimate = true;
                    }
                    if (canUseAbility && Input.GetKeyDown(abilityKey) && !abilityWorking)
                    {
                        canUseAbility = false;
                        timerAbility = walkyAbilityDuration;
                        abilityWorking = true;
                    }
                    if (abilityWorking)
                    {
                        perfectGreatHitsInRow = 0;
                        timerAbility -= Time.deltaTime;
                        if (timerAbility <= 0.0f)
                        {
                            timerAbility = 0;
                            abilityWorking = false;
                        }
                    }
                    if (canUseUltimate && Input.GetKeyDown(ultimateKey))
                    {
                        canUseUltimate = false;
                        ultimateWorking = true;
                        timerUltimate = walkyUltimateDuration;
                    }
                    if (ultimateWorking)
                    {
                        perfectHitsAbilities = 0;
                        timerUltimate -= Time.deltaTime;
                        if (timerUltimate <= 0.0f)
                        {
                            timerUltimate = 0;
                            ultimateWorking = false;
                        }
                    }
                }
                else if (leafa)
                {
                    if (scoreAbilities >= leafaAbilityCooldown)
                    {
                        canUseAbility = true;
                    }
                    if (perfectHitsAbilities >= leafaUltimateCooldown)
                    {
                        canUseUltimate = true;
                    }
                    if (canUseAbility && Input.GetKeyDown(abilityKey) && !abilityWorking)
                    {
                        canUseAbility = false;
                        timerAbility = leafaAbilityDuration;
                        abilityWorking = true;
                    }
                    if (abilityWorking)
                    {
                        scoreAbilities = 0;
                        timerAbility -= Time.deltaTime;
                        if (timerAbility <= 0.0f)
                        {
                            timerAbility = 0;
                            abilityWorking = false;
                        }
                    }
                    if (canUseUltimate && Input.GetKeyDown(ultimateKey))
                    {
                        canUseUltimate = false;
                        ultimateWorking = true;
                        timerUltimate = leafaUltimateDuration;
                    }
                    if (ultimateWorking)
                    {
                        perfectHitsAbilities = 0;
                        timerUltimate -= Time.deltaTime;
                        if (timerUltimate <= 0.0f)
                        {
                            timerUltimate = 0;
                            ultimateWorking = false;
                        }
                    }
                }
                else if (sakura)
                {
                    if (scoreAbilities >= sakuraAbilityCooldown)
                    {
                        canUseAbility = true;
                    }
                    if (perfectHitsAbilities >= sakuraUltimateCooldown)
                    {
                        canUseUltimate = true;
                    }
                    if (canUseAbility && Input.GetKeyDown(abilityKey) && !abilityWorking)
                    {
                        canUseAbility = false;
                        timerAbility = sakuraAbilityDuration;
                        abilityWorking = true;
                    }
                    if (abilityWorking)
                    {
                        scoreAbilities = 0;
                        currentMultiplier = sakuraMultiplier;
                        timerAbility -= Time.deltaTime;
                        if (timerAbility <= 0.0f)
                        {
                            timerAbility = 0;
                            abilityWorking = false;
                            currentMultiplier = 1;
                        }
                    }
                    if (canUseUltimate && Input.GetKeyDown(ultimateKey))
                    {
                        canUseUltimate = false;
                        ultimateWorking = true;
                        timerUltimate = sakuraUltimateDuration;
                    }
                    if (ultimateWorking)
                    {
                        perfectHitsAbilities = 0;
                        timerUltimate -= Time.deltaTime;
                        if (timerUltimate <= 0.0f)
                        {
                            timerUltimate = 0;
                            ultimateWorking = false;
                        }
                    }
                }

                if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
                {
                    if (bossCurrentHealth <= 0 || currentHealth <= 0)
                    {


                        Destroy(FindObjectOfType<EffectObject>().gameObject);
                        resultsScreen.SetActive(true);

                        badsText.text = "" + badHits;
                        greatsText.text = "" + greatHits;
                        goodsText.text = goodHits.ToString();
                        perfectsText.text = perfectHits.ToString();
                        missesText.text = missedHits.ToString();

                        float totalHit = greatHits + goodHits + perfectHits;
                        float percentHit = (totalHit / totalNotes) * 100f;

                        percentHitText.text = percentHit.ToString("F1") + "%";

                        string rankVal = "F";

                        if (currentScore > dScore && !failed)
                        {
                            rankVal = "D";
                            if (currentScore > cScore)
                            {
                                rankVal = "C";
                                if (currentScore > bScore)
                                {
                                    rankVal = "B";
                                    if (currentScore > aScore)
                                    {
                                        rankVal = "A";
                                        if (currentScore > sScore)
                                        {
                                            rankVal = "S";
                                        }
                                    }
                                }
                            }
                        }

                        rankText.text = rankVal;

                        finalScoreText.text = currentScore.ToString("F0");
                    }
                }
                if (currentHealth <= 0)
                {
                    theMusic.Stop();
                    bullets.SetActive(false);
                    failed = true;
                }
                currentTime = theMusic.time;
                bossCurrentHealth = bossMaxHealth - currentTime;
                bossHealthBar.SetHealth(bossCurrentHealth);
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit");

        //if (currentMultiplier - 1 < multiplierThresholds.Length)
        //{
        //    multiplierTracker++;

        //    if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
        //    {
        //        multiplierTracker = 0;
        //        currentMultiplier++;
        //    }
        //}

        multiText.text = "Multiplier: x" + currentMultiplier;

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void BadHit()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;

        currentHealth -= badDamage;
        healthBar.SetHealth(currentHealth);

        multiText.text = "Multiplier: x" + currentMultiplier;

        badHits++;
        perfectGreatHitsInRow = 0;
        perfectHitInRow = 0;
        combo = 0;
        comboText.text = "Combo: " + combo;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * overallStats * currentMultiplier;
        scoreAbilities += scorePerGoodNote * overallStats * currentMultiplier;
        NoteHit();

        if (leafa && ultimateWorking)
        {
            currentHealth += leafaHeal;
            healthBar.SetHealth(currentHealth);
        }

        goodHits++;
        perfectGreatHitsInRow = 0;
        perfectHitInRow = 0;
        combo++;
        comboText.text = "Combo: " + combo;
    }

    public void GreatHit()
    {
        currentScore += scorePerGreatNote * overallStats * currentMultiplier;
        scoreAbilities += scorePerGreatNote * overallStats * currentMultiplier;
        NoteHit();

        if (leafa && ultimateWorking)
        {
            currentHealth += leafaHeal;
            healthBar.SetHealth(currentHealth);
        }
        else if (sakura && ultimateWorking)
        {
            currentHealth += sakuraHeal;
            healthBar.SetHealth(currentHealth);
        }

        greatHits++;
        perfectGreatHitsInRow++;
        perfectHitInRow = 0;
        combo++;
        comboText.text = "Combo: " + combo;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * overallStats * currentMultiplier;
        scoreAbilities += scorePerPerfectNote * overallStats * currentMultiplier;
        NoteHit();

        if(walky && ultimateWorking)
        {
            currentHealth += walkyHeal;
            healthBar.SetHealth(currentHealth);
        }
        else if(leafa && ultimateWorking)
        {
            currentHealth += leafaHeal;
            healthBar.SetHealth(currentHealth);
        }
        else if(sakura && ultimateWorking)
        {
            currentHealth += sakuraHeal;
            healthBar.SetHealth(currentHealth);
        }

        perfectHitInRow++;
        perfectGreatHitsInRow++;
        perfectHitsAbilities++;
        perfectHits++;
        combo++;
        comboText.text = "Combo: " + combo;
    }

    public void NoteMissed()
    {
        Debug.Log("Miss");

        //currentMultiplier = 1;
        //multiplierTracker = 0

        currentHealth -= missDamage;
        healthBar.SetHealth(currentHealth);


        multiText.text = "Multiplier: x" + currentMultiplier;
        combo = 0;
        comboText.text = "Combo: " + combo;

        missedHits++;
        perfectHitInRow = 0;
        perfectGreatHitsInRow = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        theMusic.Play();
        theBS.paused = false;
    }
}
