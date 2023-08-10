using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class gameManager : MonoBehaviour
{
    public Text timeTxt;
    public GameObject card;
    public GameObject endPanel;
    public Text matchTxt;
    public Text matchTimeTxt;
    public Text timeScoreTxt;
    public Text matchScoreTxt;
    public Text thisScoreTxt;
    public Text maxScoreTxt;
    public Text levelTxt;
    public GameObject successTxt;
    public GameObject failTxt;
    int matchCount = 0;
    float matchtime = 5.0f;
    float time = 60.0f;
    bool isFliped = false;
    public static gameManager I;

    public GameObject firstCard;
    public GameObject secondCard;

    public AudioSource audioSource;
    public AudioClip match;
    
    // Start is called before the first frame update
    void Awake()
    {
        I = this;
    }

    void Start()
    {
        Time.timeScale = 1.0f;

        int currentLevel = 1;
        levelTxt.text = currentLevel.ToString();

        int[] teamMember = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        teamMember = teamMember.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 18; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;
            float x = (i / 6) * 1.2f - 1.25f;
            float y = (i % 6) * 1.2f - 4.0f;
            newCard.transform.position = new Vector3(x, y, 0);
            newCard.GetComponent<card>().SetcardNumber(teamMember[i]);
            string teamMemberName = "teamMember" + teamMember[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(teamMemberName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time <= 30f && time > 10f)
        {
            timeTxt.color = Color.yellow;
            timeTxt.fontSize = 80;
        }
        else if (time <= 10f && time > 0.0f)
        {
            timeTxt.color = Color.red;
            timeTxt.fontSize = 100;
        }
        else if (time < 0.0f)
        {
            timeTxt.text = "0.00";
            Invoke("gameEnd", 0.1f);
        }

        if (isFliped)
        {
            matchtime -= Time.deltaTime;
            matchTimeTxt.text = matchtime.ToString("N2");
            
            if (matchtime < 0.0f)
            {
                firstCard.GetComponent<card>().closeCard();
                firstCard = null;
                matchtime = 5.0f;
                isFliped = false;
            }
        }
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        matchCount++;
        matchTxt.text = matchCount.ToString();


        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(match);
            successMatchInvoke(firstCard.GetComponent<card>().GetcardNumber());
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if (cardsLeft == 2)
            {
                Invoke("gameEnd", 0.1f);
            }
        }
        else
        {
            time--;
            failTxt.SetActive(true);
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
        matchtime = 5.0f;
        isFliped = false;
        Invoke("successFailTxtInvoke", 0.4f);
    }

    void gameEnd()
    {
        Time.timeScale = 0.0f;
        float thisScore = 0.0f;
        float maxScore = 0.0f;

        timeScoreTxt.text = timeTxt.text;
        matchScoreTxt.text = matchTxt.text;

        thisScore = (time * 10.0f - matchCount);
        if (thisScore < 0.0f)
        {
            thisScore = 0.0f;
        }
        
        thisScoreTxt.text = thisScore.ToString("N0");

        if (PlayerPrefs.HasKey("maxBestScore") == false)
        {
            PlayerPrefs.SetFloat("maxBestScore", thisScore);
        }
        else
        {
            maxScore = PlayerPrefs.GetFloat("maxBestScore");
            if (maxScore < thisScore)
            {
                PlayerPrefs.SetFloat("maxBestScore", thisScore);
            }
        }
        maxScoreTxt.text = PlayerPrefs.GetFloat("maxBestScore").ToString("N1");
        endPanel.SetActive(true);
    }

    public void matchStart()
    {
        isFliped = true;
    }

    public void retryGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    void successFailTxtInvoke()
    {
        successTxt.SetActive(false);
        failTxt.SetActive(false);
    }

    public void successMatchInvoke(int teamMemberNumber)
    {
        switch(teamMemberNumber / 3)
        {
            case 0:
                successTxt.GetComponent<Text>().text = "�� �� ��";
                successTxt.SetActive(true);
                break;
            case 1:
                successTxt.GetComponent<Text>().text = "�� �� ��";
                successTxt.SetActive(true);
                break;
            case 2:
                successTxt.GetComponent<Text>().text = "�� �� ��";
                successTxt.SetActive(true);
                break;
            default:
                successTxt.SetActive(true);
                break;
        }
    }
}

