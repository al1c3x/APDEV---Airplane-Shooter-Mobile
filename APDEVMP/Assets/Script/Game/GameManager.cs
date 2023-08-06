using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playbutton;
    public GameObject playerShip;
    public GameObject EnemySpawner;
    public GameObject GameOverGo;
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;
    public GameObject TitleGO;
    public GameObject GoldGO;
    public GameObject HeartGO;
    public GameObject AdsGO;
    public GameObject DebugMenuGO;
    public GameObject Canvas;

    public GameObject NotifButtonGO;

    public GameObject ShopMenu;

    public GameObject Shop;
    public GameObject Debug; 
    public GameObject Quit;

    public GameObject PlanetLevel1;
    public GameObject PlanetLevel2;
    public GameObject PlanetLevel3;
    public Text GoldUIText;
    public int sGold = 0;
    int levelhp = 1;
    int levelspeed = 1;
    int leveldmg = 1;

    public TMP_Text speedtxt;
    public TMP_Text hptxt;
    public TMP_Text dmgtxt;

    int randomstage;

    public int gameover = 1;


    public enum GameManagerState
    {
        Opening,
        GamePlay,
        GameOver
    }

    GameManagerState GMState;
    // Start is called before the first frame update
    void Start()
    {
        GoldUIText.text = "Gold:" + sGold;
        GMState = GameManagerState.Opening;
        DebugMenuGO.SetActive(false);
        ShopMenu.SetActive(false);
        PlanetLevel2.SetActive(false);
        PlanetLevel3.SetActive(false);
    }

    

    // Update is called once per frame
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                PlanetLevel1.SetActive(true);
                AdsGO.SetActive(true);
                HeartGO.SetActive(true);
                GoldGO.SetActive(true);
                GameOverGo.SetActive(false);
                NotifButtonGO.SetActive(true);
                playbutton.SetActive(true);
                TitleGO.SetActive(true);
                Shop.SetActive(true);
                Debug.SetActive(true);
                Quit.SetActive(true);
                break;
            case GameManagerState.GamePlay:
                randomstage = Random.Range(0, 2);
                if(randomstage == 0)
                {
                    PlanetLevel1.SetActive(true);
                    PlanetLevel2.SetActive(false);
                    PlanetLevel3.SetActive(false);
}
                else if(randomstage == 1)
                {
                    PlanetLevel1.SetActive(false);
                    PlanetLevel2.SetActive(true);
                    PlanetLevel3.SetActive(false);
                }

        
                else
                {
                    PlanetLevel1.SetActive(false);
                    PlanetLevel2.SetActive(false);
                    PlanetLevel3.SetActive(true);
                }
        
                Shop.SetActive(false);
                Debug.SetActive(false);
                Quit.SetActive(false);
                AdsGO.SetActive(false);
                HeartGO.SetActive(false);
                GoldGO.SetActive(false);
                TitleGO.SetActive(false);
                NotifButtonGO.SetActive(false);
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                playbutton.SetActive(false);
                playerShip.GetComponent<PlayerControl>().Init();
                EnemySpawner.GetComponent<EnemySpawner>().StartSpawner();
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
                break;
            case GameManagerState.GameOver:
               
                GoldGO.SetActive(false);
                NotifButtonGO.SetActive(false);
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();
                EnemySpawner.GetComponent<EnemySpawner>().StopSpawner();
                GameOverGo.SetActive(true);
                Invoke("OpeningState", 8.0f);
                sGold += Random.Range(8, 20);
                GoldUIText.text = "Gold:" + sGold;
                break;
        }
    }

    public void AddGold()
    {
        sGold += Random.Range(8, 50);
        GoldUIText.text = "Gold:" + sGold;
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.GamePlay;
        UpdateGameManagerState();
    }

    public void OpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    public void GenerateNotif()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();


        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notifications Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "Test Notification";
        notification.Text = "Sample";
        //notification.FireTime = System.DateTime.Now.AddMinutes(1);
        notification.FireTime = System.DateTime.Now.AddSeconds(10);

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

    public void UpgradeHp()
    {
        if(sGold >= 25 * levelhp && levelhp < 5)
        {
            levelhp++;
            sGold -= 25;
            playerShip.GetComponent<PlayerControl>().IncreaseHp();
            GoldUIText.text = "Gold:" + sGold;
        }
        if (levelspeed == 5)
        {
           hptxt.text = "Max Upgrade";
        }
    }
    public void UpgradeSpeed()
    {
        if (sGold >= 50 * levelspeed && levelspeed < 5)
        {
            levelspeed++;
            sGold -= 50;
            playerShip.GetComponent<PlayerControl>().IncreaseSpeed();
            GoldUIText.text = "Gold:" + sGold;
        }
        if(levelspeed == 5)
        {
            speedtxt.text = "Max Upgrade";
        }
    }
    public void UpgradeDamage()
    {
        if (sGold >= 100 * leveldmg && leveldmg < 5)
        {
            leveldmg++;
            sGold -= 100;
            playerShip.GetComponent<PlayerControl>().IncreaseDamage();
            GoldUIText.text = "Gold:" + sGold;
        }
        if (levelspeed == 5)
        {
            dmgtxt.text = "Max Upgrade";
        }
    }
    public void DebugMenuStart()
    {
        Canvas.SetActive(false);
        DebugMenuGO.SetActive(true);
    }
    public void BackMenu()
    {
        Canvas.SetActive(true);
        DebugMenuGO.SetActive(false);
        ShopMenu.SetActive(false);
    }
    public void ShopMenuStart()
    {
        Canvas.SetActive(false);
        ShopMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
