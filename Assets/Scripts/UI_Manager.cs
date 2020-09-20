using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text livesText;
    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript ps = GameObject.FindGameObjectsWithTag("Player")?[0].GetComponent<PlayerScript>();
        if (ps == null) Debug.LogError("UI_Manager::Start() -> ps is null.");
        if (livesText == null) Debug.LogError("UI_Manager::Start() -> livesText is null.");
        if (coinsText == null) Debug.LogError("UI_Manager::Start() -> coinsText is null");
        UpdateIntegerText(livesText, ps.GetLives());
        UpdateIntegerText(coinsText, ps.GetCoins());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerScript.coinAmountEvent += UpdateCoinText;
        PlayerScript.lifeAmountEvent += UpdateLivesText;
    }


    private void OnDisable()
    {
        PlayerScript.coinAmountEvent -= UpdateCoinText;
        PlayerScript.lifeAmountEvent -= UpdateLivesText;
    }

    void UpdateLivesText(int amount)
    {
        UpdateIntegerText(livesText, amount);
    }

    void UpdateCoinText(int amount)
    {
        UpdateIntegerText(coinsText, amount);
    }

    void UpdateIntegerText(Text t, int amount)
    {
        t.text = string.Format("{0, 4}", amount);
    }

}
