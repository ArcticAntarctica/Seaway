using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyObserver : MonoBehaviour
{
    public static int MoneyAdditionFactor;
    public Text MoneyText, MoneyAddCanvas;
    public AudioSource Cash1, Cash2;

    private void OnEnable()
    {
        // Attaches observer
        DeliveryObserver.MoneyPopUp += KickstartMoneyPopUp;
        FishingObserver.FishMoneyPopUp += KickstartMoneyPopUp;
        LootClaim.LootMoneyPopUp += KickstartMoneyPopUp;
    }

    private void OnDisable()
    {
        // Detaches observer
        DeliveryObserver.MoneyPopUp -= KickstartMoneyPopUp;
        FishingObserver.FishMoneyPopUp -= KickstartMoneyPopUp;
        LootClaim.LootMoneyPopUp -= KickstartMoneyPopUp;
    }

    public void KickstartMoneyPopUp()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        Cash2.Play();
        MoneyAddCanvas.text = MoneyAdditionFactor.ToString();

        // fade from transparent to opaque
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            MoneyAddCanvas.GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }
        //Ensure that UI is fully on
        MoneyAddCanvas.GetComponent<CanvasGroup>().alpha = 1;

        //Add the sum to the total
        MoneyText.text = (int.Parse(MoneyText.text) + MoneyAdditionFactor).ToString();
        yield return new WaitForSeconds(1f);

        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            MoneyAddCanvas.GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }
        //Ensure that the UI is fully off
        MoneyAddCanvas.GetComponent<CanvasGroup>().alpha = 0;
        yield return null;
    }
}
