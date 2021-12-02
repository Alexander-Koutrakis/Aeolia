using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NotificationController : MonoBehaviour
{
    private PingPong itemNotification;
    private PingPong containerNotification;
    private PingPong gPsNotification;
    private PingPong recordingsNotification;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image containerImage;
    [SerializeField] private Image gPsImage;
    [SerializeField] private Image recordingsImage;
    private bool itemNotificationON;
    private bool containerNotificationON;
    private bool gPsNotificationON;
    private bool recordingsNotificationON;

    public static NotificationController Instance;
    private void Awake()
    {
        Instance = this;
        itemNotification = new PingPong(itemImage);
        containerNotification = new PingPong(containerImage);
        gPsNotification = new PingPong(gPsImage);
        recordingsNotification = new PingPong(recordingsImage);
    }

    private void Update()
    {
        if (itemNotificationON)
        {
            itemNotification.Update();
        }
        if (containerNotificationON)
        {
            containerNotification.Update();
        }
        if (gPsNotificationON)
        {
            gPsNotification.Update();
        }
        if (recordingsNotificationON)
        {
            recordingsNotification.Update();
        }
    }


    public void Notify(Notification notification)
    {
        switch (notification)
        {
            case Notification.NewItem:
                itemNotificationON = true;
                itemNotification.Start();
                break;
            case Notification.NewContainer:
                containerNotificationON = true;
                containerNotification.Start();
                break;
            case Notification.NewLocation:
                gPsNotificationON = true;
                gPsNotification.Start();
                break;
            case Notification.NewRecording:
                recordingsNotificationON = true;
                recordingsNotification.Start();
                break;
            default:
                break;
        }
    }

    public void StopNotifying(string notification)
    {
        switch (notification)
        {
            case "NewItem":
                itemNotificationON = false;
                itemNotification.End();
                break;
            case "NewContainer":
                containerNotificationON = false;
                containerNotification.End();
                break;
            case "NewLocation":
                gPsNotificationON = false;
                gPsNotification.End();
                break;
            case "NewRecording":
                recordingsNotificationON = false;
                recordingsNotification.End();
                break;
            default:
                break;
        }
    }

    public void StopNotifying(Notification notification)
    {
        switch (notification)
        {
            case Notification.NewItem:
                itemNotificationON = false;
                itemNotification.End();
                break;
            case Notification.NewContainer:
                containerNotificationON = false;
                containerNotification.End();
                break;
            case Notification.NewLocation:
                gPsNotificationON = false;
                gPsNotification.End();
                break;
            case Notification.NewRecording:
                recordingsNotificationON = false;
                recordingsNotification.End();
                break;
            default:
                break;
        }
    }

    private class PingPong
    {
        private Image image;
        private float t;

        public void Start()
        {
            image.gameObject.SetActive(true);
        }

        public void Update()
        {
            t = Time.time;
            image.transform.localScale = new Vector3(Mathf.PingPong(t, 1 - 0.5f) + 0.5f, Mathf.PingPong(t, 1 - 0.5f) + 0.5f, 0);
        }

        public void End()
        {
            image.gameObject.SetActive(false);
        }

        public PingPong(Image image)
        {
            this.image = image;
            this.image.gameObject.SetActive(false);
        }
    }
}

public enum Notification { NewItem,NewContainer,NewLocation,NewRecording}

