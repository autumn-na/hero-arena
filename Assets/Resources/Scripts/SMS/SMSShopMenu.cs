using UnityEngine;
using UnityEngine.UI;

public class SMSShopMenu : MonoBehaviour
{
    public GameObject BoosterButtonGam = null;
    public GameObject CharacterButtonGam = null;
    public Image[] MenuImg;

    private void Start()
    {
        BoosterButtonGam.SetActive(true);
        CharacterButtonGam.SetActive(false);
        MenuImg[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Booster_Ui_2");
    }

    public void BoosterButton()
    {
        BoosterButtonGam.SetActive(true);
        CharacterButtonGam.SetActive(false);
        MenuImg[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Booster_Ui_2");
        MenuImg[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Character_Ui");
        SMSSoundMng.GetInstance.ClickButton();
    }

    public void CharacterButton()
    {
        BoosterButtonGam.SetActive(false);
        CharacterButtonGam.SetActive(true);
        MenuImg[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Booster_Ui");
        MenuImg[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/3_ShopScene/Character_Ui_2");
        SMSSoundMng.GetInstance.ClickButton();
    }

    public void BackButton()
    {
        NMHSceneMng.instance.LoadScene(NMHSceneMng.SceneType.Lobby);
        SMSSoundMng.GetInstance.ClickButton();
    }
}