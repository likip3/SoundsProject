using UnityEngine;
using UnityEngine.UI;

public class UserUI : MonoBehaviour
{
    private bool fadeUp;
    [SerializeField] private Text subTitle;
    [SerializeField] private Text title;
    private bool TitleOnly;

    private void FixedUpdate()
    {
        if (fadeUp)
        {
            var tempTitleColor = title.color;
            tempTitleColor.a += 0.01f;
            title.color = tempTitleColor;

            var tempSubTitleColor = subTitle.color;
            tempSubTitleColor.a += 0.01f;
            subTitle.color = tempSubTitleColor;

            if (title.color.a > 1.5) fadeUp = false;
        }
        else if (title.color.a > 0)
        {
            var tempTitleColor = title.color;
            tempTitleColor.a -= 0.01f;
            title.color = tempTitleColor;

            var tempSubTitleColor = subTitle.color;
            tempSubTitleColor.a -= 0.01f;
            subTitle.color = tempSubTitleColor;
        }
    }

    public void ShowMessage()
    {
        ShowMessage("Test Title", "Test SubTitle");
    }

    public void ShowMessage(string Title)
    {
        ShowMessage(Title, "");
    }

    public void ShowMessage(string Title, string SubTitle, bool artifact)
    {
        if (artifact) GetComponent<AudioSource>().Play();
        ShowMessage(Title, SubTitle);
    }

    public void ShowMessage(string Title, string SubTitle)
    {
        title.text = Title;
        subTitle.text = SubTitle;
        fadeUp = true;
    }
}