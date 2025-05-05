using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeObserver : MonoBehaviour
{
    public Text NoticeText;
    private void OnEnable()
    {
        // Attaches observer
        FishingObserver.LootFishMaxNotice += KickstartFishMaxNotice;
        FishCapture.FishMaxNotice += KickstartFishMaxNotice;
        DeliveryObserver.InventoryMaxNotice += KickstartInventoryMaxNotice;
        DeliveryObserver.IslandTooFarNotice += KickstartIslandTooFarNotice;
    }

    private void OnDisable()
    {
        // Detaches observer
        FishingObserver.LootFishMaxNotice -= KickstartFishMaxNotice;
        FishCapture.FishMaxNotice -= KickstartFishMaxNotice;
        DeliveryObserver.InventoryMaxNotice -= KickstartInventoryMaxNotice;
        DeliveryObserver.IslandTooFarNotice -= KickstartIslandTooFarNotice;
    }

    public void KickstartFishMaxNotice()
    {
        StartCoroutine(FishMaxCapacity());
    }

    public void KickstartInventoryMaxNotice()
    {
        StartCoroutine(InventoryFullText());
    }

    public void KickstartIslandTooFarNotice()
    {
        StartCoroutine(IslandTooFarText());
    }

    private IEnumerator FishMaxCapacity()
    {
        NoticeText.text = "Fish maximum capacity reached";

        for (float i = 0; i <= 1; i += Time.deltaTime * 2)
        {
            NoticeText.color = new Color(0, 0, 0, i);
            yield return null;
        }
        // Ensure that UI is fully on
        NoticeText.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(1f);

        for (float i = 1; i >= 0; i -= Time.deltaTime * 2)
        {
            NoticeText.color = new Color(0, 0, 0, i);
            yield return null;
        }
        // Ensure that the UI is fully off
        NoticeText.color = new Color(0, 0, 0, 0);

        yield return null;
    }

    private IEnumerator InventoryFullText()
    {
        NoticeText.text = "Cargo hold full";

        for (float i = 0; i <= 1; i += Time.deltaTime * 2)
        {
            NoticeText.color = new Color(0, 0, 0, i);
            yield return null;
        }
        // Ensure that UI is fully on
        NoticeText.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(1f);

        for (float i = 1; i >= 0; i -= Time.deltaTime * 2)
        {
            NoticeText.color = new Color(0, 0, 0, i);
            yield return null;
        }
        // Ensure that the UI is fully off
        NoticeText.color = new Color(0, 0, 0, 0);

        yield return null;
    }

    private IEnumerator IslandTooFarText()
    {
        NoticeText.text = "The island is too far away";

        for (float i = 0; i <= 1; i += Time.deltaTime * 2)
        {
            NoticeText.color = new Color(0, 0, 0, i);
            yield return null;
        }
        // Ensure that UI is fully on
        NoticeText.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(1f);

        for (float i = 1; i >= 0; i -= Time.deltaTime * 2)
        {
            NoticeText.color = new Color(0, 0, 0, i);
            yield return null;
        }
        // Ensure that the UI is fully off
        NoticeText.color = new Color(0, 0, 0, 0);

        yield return null;
    }
}
