using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Data;

public class PieceStats : MonoBehaviour
{
    public GameObject statsCanvas;

    public TextMeshProUGUI characterName;
    public Image healthGreen;
    public Image healthRed;
    public TextMeshProUGUI healthTxt;
    public TextMeshProUGUI powerTxt;

    private float updateSpeed = 0.75f;

    public void Toggle(bool isActive)
    {
        statsCanvas.SetActive(isActive);
    }

    public void UpdateUI(Piece piece)
    {
        healthGreen.fillAmount = piece.health / piece.maxHealth;
        healthRed.fillAmount = piece.health / piece.maxHealth;
        healthTxt.text = $"{piece.health} / {piece.maxHealth}";
        characterName.text = piece.label.ToString();
        powerTxt.text = ((piece.power - 1) * 10).ToString();
    }

    public void UpdateHealthUI(Piece piece, float previousHealth)
    {
        statsCanvas.gameObject.SetActive(true);
        float preDamageHealthPercent = previousHealth / piece.maxHealth;
        float postDamageHealthPercent = piece.health / piece.maxHealth;

        // if damaged
        if (preDamageHealthPercent > postDamageHealthPercent)
        {
            healthGreen.fillAmount = postDamageHealthPercent;
            healthRed.fillAmount = preDamageHealthPercent;
            StartCoroutine(UpdateBar(healthRed, preDamageHealthPercent, postDamageHealthPercent));

        } // if heal
        else if (preDamageHealthPercent < postDamageHealthPercent)
        {
            healthRed.fillAmount = postDamageHealthPercent;
            healthGreen.fillAmount = preDamageHealthPercent;
            StartCoroutine(UpdateBar(healthGreen, preDamageHealthPercent, postDamageHealthPercent));
        }

        healthTxt.text = $"{piece.health} / {piece.maxHealth}";
    }

    IEnumerator UpdateBar(Image targetBar, float startPercent, float endPercent)
    {
        float preChangePercent = startPercent;
        float elapsed = 0f;

        yield return new WaitForSeconds(1);

        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            targetBar.fillAmount = Mathf.Lerp(preChangePercent, endPercent, elapsed / updateSpeed);
            yield return null;
        }

        targetBar.fillAmount = endPercent;
        yield return new WaitForSeconds(1);
        statsCanvas.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        statsCanvas.transform.rotation = Camera.main.transform.rotation;
    }
}
