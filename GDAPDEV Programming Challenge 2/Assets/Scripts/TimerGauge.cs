using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerGauge : MonoBehaviour
{
    [SerializeField] Color unreadyColor;
    [SerializeField] Color readyColor;
    private RectTransform rectTf;
    private Image gaugeImage;
    private float gaugeMaxWidth;

    private void Awake()
    {
        rectTf = GetComponent<RectTransform>();
        gaugeImage = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gaugeMaxWidth = rectTf.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        float progress = GameHandler.Instance.CurrentTime / GameHandler.Instance.MaxTime;
        float prog_width = Mathf.Lerp(0f, gaugeMaxWidth, progress);
        rectTf.sizeDelta = new Vector2(prog_width, rectTf.sizeDelta.y);

        if (progress > 0.25)
        {
            gaugeImage.color = unreadyColor;
        }
        else
        {
            gaugeImage.color = readyColor;
        }
    }
}
