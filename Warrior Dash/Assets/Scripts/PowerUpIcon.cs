using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpIcon : MonoBehaviour
{
    //cached references
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI powerUpText;

    [SerializeField] GameObject powerUp;
    [SerializeField] Sprite defaultSprite;
    string defaultText = "inf";

    private void Start()
    {
        ResetIcon();
    }
        
    void Update()
    {
        if (powerUp != null)
        {
            SetText(powerUp.GetComponent<Pickup>().GetAmmoCount());
        }
    }

    //makes Powerup icon in HUD the same as the current powerUp
    public void SetIcon(Sprite newSprite)
    {
        GetComponent<Image>().sprite = newSprite;
    }

    public void ResetIcon(){ GetComponent<Image>().sprite = defaultSprite; }
    public void Reset()
    {
        powerUpText.text = defaultText;
        GetComponent<Image>().sprite = defaultSprite;
    }

    public void SetText(int text){ powerUpText.text = text.ToString(); }
    public string GetText() { return powerUpText.text; }

    public void SetPowerUp(GameObject newPower) { powerUp = newPower; }
    public GameObject GetPowerUp() { return powerUp; }
}
