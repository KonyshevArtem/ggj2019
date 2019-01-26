using UnityEngine;

public class FillBar : MonoBehaviour
{
    public GameObject Fill;
    public GameObject Highlight;
    private RectTransform _fillRectTransform;
    private RectTransform _highlightRectTransform;
    public int Value = 0;
    private void UpdateFill()
    {
        if (Value <= 0)
        {
            _fillRectTransform.sizeDelta = new Vector2(0, _fillRectTransform.sizeDelta.y);
        }
        else if (Value >= 100)
        {
            _fillRectTransform.sizeDelta =
                new Vector2(_highlightRectTransform.rect.width, _fillRectTransform.sizeDelta.y);
        }
        else
        {
            var part = (float)Value / 100;
            _fillRectTransform.sizeDelta =
                new Vector2(part * _highlightRectTransform.rect.width, _fillRectTransform.sizeDelta.y);
        }
    }

    // Use this for initialization
    private void Start()
    {
        _fillRectTransform = Fill.GetComponent<RectTransform>();
        UpdateFill();
        _highlightRectTransform = Highlight.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateFill();
    }
}
