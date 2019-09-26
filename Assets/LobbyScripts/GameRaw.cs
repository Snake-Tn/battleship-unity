using UnityEngine;
using UnityEngine.UI;

public class GameRaw : MonoBehaviour
{
    public Text TitleTxt;
    public Text HostTxt;
    public Text GuestTxt;
    public Text VSTxt;
    public Button JoinBtn;    
    public int Id;

    public RectTransform NewGamePanel;
    public RectTransform JoinedGamePanel;

    public void SetTitle(string title) {
        TitleTxt.text = title;
    }
    public void SetHost(string host) {
        HostTxt.text = host;
    }
    public void SetGuest(string guest) {
        VSTxt.text = "VS";
        GuestTxt.text = guest;
        JoinBtn.gameObject.SetActive(false);
    }    
    public void SetId(int id) {
        this.Id = id;
    }

    public void SetNewGoomPanel(RectTransform NewGamePanel) {
        this.NewGamePanel = NewGamePanel;
    }
    public void SetJoinedGamePanel(RectTransform JoinedGamePanel) {
        this.JoinedGamePanel = JoinedGamePanel;
    }
}

