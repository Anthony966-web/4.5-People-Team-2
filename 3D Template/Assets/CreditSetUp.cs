using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreditSetUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public People people;
    public GameObject CreditView;

    void Start()
    {
        SetUp();
    }

    public void SetUp()
    {
        if(people != null)
        {
            gameObject.name = people.FullName;
            gameObject.GetComponent<TMP_Text>().text = people.FullName;
        }
        else
        {
            Destroy(this.gameObject);
        }    
    }

    
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CreditView.SetActive(true);

        if(people.VGD == true && people.AMI == false)
        {
            CreditView.transform.GetChild(2).gameObject.SetActive(true);
            CreditView.transform.GetChild(3).gameObject.SetActive(true);
            CreditView.transform.GetChild(4).gameObject.SetActive(true);

            CreditView.transform.GetChild(5).gameObject.SetActive(false);
            CreditView.transform.GetChild(6).gameObject.SetActive(false);
            CreditView.transform.GetChild(7).gameObject.SetActive(false);
            CreditView.transform.GetChild(8).gameObject.SetActive(false);
            CreditView.transform.GetChild(9).gameObject.SetActive(false);

            CreditView.transform.GetChild(0).GetComponent<TMP_Text>().text = people.FullName;
            CreditView.transform.GetChild(1).GetComponent<TMP_Text>().text = people.TeamRole;
            CreditView.transform.GetChild(2).GetComponent<Image>().sprite = people.Image1;
            CreditView.transform.GetChild(3).GetComponent<Image>().sprite = people.Image2;
            CreditView.transform.GetChild(4).GetComponent<Image>().sprite = people.Image3;

        }

        if (people.VGD == false && people.AMI == true)
        {
            CreditView.transform.GetChild(5).gameObject.SetActive(true);
            CreditView.transform.GetChild(6).gameObject.SetActive(true);
            CreditView.transform.GetChild(7).gameObject.SetActive(true);
            CreditView.transform.GetChild(8).gameObject.SetActive(true);
            CreditView.transform.GetChild(9).gameObject.SetActive(true);

            CreditView.transform.GetChild(2).gameObject.SetActive(false);
            CreditView.transform.GetChild(3).gameObject.SetActive(false);
            CreditView.transform.GetChild(4).gameObject.SetActive(false);

            CreditView.transform.GetChild(0).GetComponent<TMP_Text>().text = people.FullName;
            CreditView.transform.GetChild(1).GetComponent<TMP_Text>().text = people.TeamRole;
            CreditView.transform.GetChild(6).GetComponent<Image>().sprite = people.Image1;
            CreditView.transform.GetChild(5).GetComponent<Image>().sprite = people.Image2;
            CreditView.transform.GetChild(9).GetComponent<Image>().sprite = people.Image3;
            CreditView.transform.GetChild(7).GetComponent<Image>().sprite = people.Image4;
            CreditView.transform.GetChild(8).GetComponent<Image>().sprite = people.Image5;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CreditView.SetActive(false);
    }
}
