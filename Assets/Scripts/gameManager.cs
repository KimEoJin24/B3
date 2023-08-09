using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class gameManager : MonoBehaviour
{
    public Text timeTxt;
    public GameObject card;
    float time;

    // Start is called before the first frame update
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
}
