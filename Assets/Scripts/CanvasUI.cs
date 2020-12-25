using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    [System.Serializable] class ShopItem
    {
        public int ID;
        public Mesh mesh;
        public Sprite sprite;
        public int Price;
        public bool isPurchased;
        public bool isEquip;
        public string name;
    }
    [Header("Shop Settings")]
    public Animator anim_notEnough;
    public GameObject prefabItem;
    public GameObject content;
    [SerializeField] List<ShopItem> listItems;
    [Header("Transition Settings")]
    public List<Animator> animList = new List<Animator>();
    [SerializeField] Button buttonNextLevel;
    [SerializeField] Button buttonTryAgain;
    [Header("References")]
    [SerializeField] GameObject TOPRIGHTUI;
    [SerializeField] GameObject PanelMenu;
    [SerializeField] GameObject shop;
    Button buyButton;
    public Button RewardButton;
    public void _btnMenu()
    {
        /* Make rigid not sliding */
        Rigidbody rgPlayer = GameManager.instance.player.GetComponent<Rigidbody>();
        rgPlayer.velocity = Vector3.zero;
            /* ------ */
        Button btnMenu = TOPRIGHTUI.transform.GetChild(0).GetComponent<Button>();
        btnMenu.interactable = false;
        /* Stop script Player and Enemy */
        GameManager.instance.player.enabled = false;
        GameManager.instance.player.GetComponent<Animator>().enabled = false;
        foreach (var value in GameManager.instance.enemyList)
        {
            value.GetComponent<Animator>().enabled = value.GetComponent<Enemy>().enabled = false;
        }
        /* Something active here */
        PanelMenu.SetActive(true);
    }
    public void _btnMenu_Shop()
    {
        shop.active = true;
        Button btnMenu = TOPRIGHTUI.transform.GetChild(0).GetComponent<Button>();
        btnMenu.gameObject.SetActive(false);
        PanelMenu.active = false;
    }
    public void _btnMenu_Quit()
    {
        Application.Quit();
    }
    public void _btnMenu_Resume()
    {
        Button btnMenu = TOPRIGHTUI.transform.GetChild(0).GetComponent<Button>();
        btnMenu.interactable = true;
        PanelMenu.SetActive(false);
        /* Stop script Player and Enemy */
        GameManager.instance.player.enabled = true;
        GameManager.instance.player.GetComponent<Animator>().enabled = true;
        foreach (var value in GameManager.instance.enemyList)
        {
            value.GetComponent<Animator>().enabled = value.GetComponent<Enemy>().enabled = true;
        }
    }
    public void _btnShop_Close()
    {
        Button btnMenu = TOPRIGHTUI.transform.GetChild(0).GetComponent<Button>();
        btnMenu.gameObject.SetActive(true);
        btnMenu.interactable = false;
        PanelMenu.SetActive(true);
        shop.SetActive(false);
    }
    void BuyItem(ShopItem value,Button btn)
    {
        Text yourCoin = gameObject.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>();
        Text txt = btn.gameObject.transform.GetChild(0).GetComponent<Text>();
        /* Checking if thisOne enough Coin */
        int total = PlayerPrefs.GetInt("coin") - value.Price;
        if(total >= 0&&!value.isPurchased)
        {
            PlayerPrefs.SetInt("coin", total);
            yourCoin.text = PlayerPrefs.GetInt("coin").ToString() + "$";
            value.isPurchased = true;
            txt.text = "Equip";
            PlayerPrefs.SetInt(value.name, 1);
        }
        if(total <= 0 && !value.isPurchased)
        {
            anim_notEnough.SetTrigger("isAppear");
        }
        if(value.isPurchased)
        {
            btn.onClick.AddListener(delegate { EquipItem(value,btn); });
        }
    }
    void EquipItem(ShopItem value,Button btn)
    {
        Text txtBuy = btn.transform.GetChild(0).GetComponent<Text>();
        /* Last thing I need to Disable All the equip */
        for(int i =0;i<content.transform.childCount;i++)
        {
            if (listItems[i].isPurchased)
            {
                GameObject obj = content.transform.GetChild(i).gameObject;
                Text txtObj = obj.GetComponent<Item>().txtBuy;
                Button btnObj = obj.GetComponent<Item>().btnBuy;
                txtObj.text = "Equip";
                btnObj.interactable = true;
            }
        }
            /* Set new equip; */
        txtBuy.text = "Equipped";
        btn.interactable = false;
        value.isEquip = true;
        PlayerPrefs.GetInt("Equip", value.ID);
        /* Change mesh */
        MeshFilter playerLeft  = GameManager.instance.player.weaponLeft.GetComponent<MeshFilter>();
        MeshFilter playerRight = GameManager.instance.player.weaponRight.GetComponent<MeshFilter>();
        playerLeft.sharedMesh = playerRight.sharedMesh = value.mesh;
    }
    public void InitShopItem()
    {
        MeshFilter playerMesh = GameManager.instance.player.weaponLeft.GetComponent<MeshFilter>();
        Text yourCoin = gameObject.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>();
        /* PlayerPrefs YourCoin */
        yourCoin.text = PlayerPrefs.GetInt("coin").ToString() + "$";
        /* Init listItem */
        foreach (var value in listItems)
        {
            GameObject obj = Instantiate(prefabItem, content.transform);
            Button btn  = obj.GetComponent<Item>().btnBuy;
            Text txtBuy = obj.GetComponent<Item>().txtBuy;
            Text price  = obj.GetComponent<Item>().Price;
            Image img   = obj.GetComponent<Item>().imageIcon;
            /* Value condition  */
            /* Playerprefs Int Value name này sẽ xét isPurchased  0 - la chua mua - 1 là mua r */
            if (PlayerPrefs.GetInt(value.name) != 0) value.isPurchased = true;
            else value.isPurchased = false;
            if (PlayerPrefs.GetInt("Equip") == value.ID) value.isEquip = true;
                /* End value condition */
            img.sprite = value.sprite;
            price.text = value.Price.ToString();
            /* PlayerPrefs To save value for weapon 0 is not Purchased - 1 is already Purchased */
            if (value.isPurchased) // Equip Item
            {
                btn.interactable = true;
                txtBuy.text = "Equip";
                btn.onClick.AddListener(delegate { EquipItem(value, btn); });
            }
            else // Buy Item
            {
                txtBuy.text = "Buy";
                btn.onClick.AddListener(delegate { BuyItem(value, btn); });
            }
            /* Checking which Weapon Player equipped */
            if(value.mesh.name == playerMesh.sharedMesh.name)
            {
                btn.interactable = false;
                value.isEquip = true;
                txtBuy.text = "Equipped";
            }
        }
        listItems[0].isPurchased = true; // Default Dagger
    }
    public void _PanelGameOver()
    {
            /* Disable UI */
            PanelMenu.active = shop.active = TOPRIGHTUI.active = false;
        GameObject panelGameOver = gameObject.transform.GetChild(1).gameObject;
        GameObject TOPUI = panelGameOver.transform.GetChild(0).gameObject;
        GameObject panelEffect = TOPUI.transform.GetChild(2).gameObject;
        panelGameOver.active = panelEffect.active = true;
    }
    public void _PanelFinish()
    {
        /* Disable UI */
        TOPRIGHTUI.active = false;
        /* PS_panelFinish */
        StartCoroutine(PS_PanelFinish());
        /* cash Earn */
        StartCoroutine(C_cashEarn());
    }
    private IEnumerator PS_PanelFinish()
    {

        GameObject panelFinish = gameObject.transform.GetChild(0).gameObject;
        GameObject panelEffect = panelFinish.transform.GetChild(2).gameObject;
        panelEffect.active = panelFinish.active = true;
        yield return new WaitForSeconds(2f);
        panelEffect.SetActive(false);
        yield return PS_PanelFinish();
    }
    public IEnumerator C_cashEarn()
    {
        bool Loop = true;
        int currentCoin = PlayerPrefs.GetInt("coin");
        int targetCoin = currentCoin + 30;
        /* Init coinText */
        Text coinText = gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
        coinText.text = currentCoin.ToString();
        /* Sound Effect */
        AudioSource adiSrc = coinText.gameObject.AddComponent<AudioSource>();
        AudioClip cashEarn = SoundManager.instance.cashEarn;
        adiSrc.clip = cashEarn;
        adiSrc.Play();
        /* Cash Earn */
        RectTransform coinPos = gameObject.transform.GetChild(0).transform.GetChild(3).GetComponent<RectTransform>();
        RectTransform middleCoin = gameObject.transform.GetChild(0).transform.GetChild(4).GetComponent<RectTransform>();
        float Distance = 0f;
        while (Loop)
        {
            /* Transform Position Coin */
            if (middleCoin.position != coinPos.position)
            {
                Distance = Vector3.Distance(coinPos.position,middleCoin.position);
                gameObject.transform.GetChild(0).transform.GetChild(4).GetComponent<RectTransform>().position
                    = Vector3.Lerp(middleCoin.position, coinPos.position, 3f * Time.deltaTime);
                yield return null;
            }
            if (Distance < 1f)
            {
                middleCoin.gameObject.SetActive(false);
                middleCoin.position = coinPos.position;
                float _time = 0f;
                while(_time < 1f)
                {
                    /* Increase Coin in real-Time */
                    _time += Time.deltaTime / 1f;
                    int Coin = (int)Mathf.Lerp(currentCoin, targetCoin, _time);
                    coinText.text = Coin.ToString();
                    yield return null;
                }
                if(_time>1f)
                {
                    /* Set Coin Prefs */
                    PlayerPrefs.SetInt("coin",targetCoin);
                    Loop = false;
                }
            }
        }
    }
    public void _btnNextLevel()
    {
        StartCoroutine(C_btnNextLevel());
    }
    IEnumerator C_btnNextLevel()
    {
        buttonNextLevel.interactable = false;
        /* Transition Effect */
        Transition();
        yield return new WaitForSeconds(5f);
        PlayerPrefs.SetInt("Scene", PlayerPrefs.GetInt("Scene") + 1);
        int Scene = PlayerPrefs.GetInt("Scene");
        if(Scene == 25)
        {
            PlayerPrefs.SetInt("Scene", 1);
        }
        SceneManager.LoadSceneAsync(0);
    }
    public void TryAgain()
    {
        StartCoroutine(C_btnTryAgain());
    }
    public void _btnTryAgain()
    {
        gameObject.GetComponent<AdsController>().ShowAds();
        buttonTryAgain.interactable = false;
    }
    IEnumerator C_btnTryAgain()
    {
        Transition();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync(0);
    }
    public void _btnRewardVideo()
    {
        GetComponent<AdsController>().ShowRewardVideo();
    }
    void Transition()
    {
        foreach (var value in animList)
        {
            value.SetTrigger("isFinish");
        }
    }
}
