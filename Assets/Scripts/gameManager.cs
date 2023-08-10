using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class gameManager : MonoBehaviour
{
    public Text timeTxt;
    public GameObject endTxt;
    public GameObject card;
    float time;
    public static gameManager I;

    public GameObject firstCard;
    public GameObject secondCard;

    public AudioClip match;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        I = this;
    }

    void Start()
    {
        int[] teamMember = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        teamMember = teamMember.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 18; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;
            float x = (i / 6) * 1.2f - 1.25f;
            float y = (i % 6) * 1.2f - 4.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string teamMemberName = "teamMember" + teamMember[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(teamMemberName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
    }

    public void isMatched()
        {
            string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
            string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

            if (firstCardImage == secondCardImage)
            {
                audioSource.PlayOneShot(match);
                firstCard.GetComponent<card>().destroyCard();
                secondCard.GetComponent<card>().destroyCard();

                int cardsLeft = GameObject.Find("cards").transform.childCount;
                if (cardsLeft == 2)
                {
                    Time.timeScale = 0f;
                    endTxt.SetActive(true);
                    Invoke("GameEnd", 1f);
                }
            }
            else
            {
                firstCard.GetComponent<card>().closeCard();
                secondCard.GetComponent<card>().closeCard();
            }

            firstCard = null;
            secondCard = null;
        }

        void GameEnd()
        {
            Time.timeScale = 0f;
            endTxt.SetActive(true);
        }

        public void retryGame()
        {
            SceneManager.LoadScene("MainScene");
        }
    }