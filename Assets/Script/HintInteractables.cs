using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HintInteractables : MonoBehaviour
{
    private float duration;
    private float effectMaxDuration=5f;
    private ImageShake[] hintImages;
    private Sprite hintImageFiller;

    private void Awake()
    {
        hintImageFiller = Resources.Load<Sprite>("HintImageFiller");
    }

    private void Start()
    {
        ItemContainer[] itemContainers = GetComponentsInChildren<ItemContainer>(true);
        hintImages = new ImageShake[itemContainers.Length];
        
        for (int i = 0; i < itemContainers.Length; i++)
        {
            if (!itemContainers[i].UsedItem)
            {
                hintImages[i] = NewHintImage(itemContainers[i]);
            }
        }
    }

    public void Hint()
    {
        duration = effectMaxDuration;
        for (int i = 0; i < hintImages.Length; i++)
        {
            if (hintImages[i].ShowHint())
            {
                hintImages[i].Start();
            }
        }
    }

    public void Update()
    {
        if (duration > 0)
        {
            for (int i = 0; i < hintImages.Length; i++)
            {
                if (hintImages[i].ShowHint())
                    hintImages[i].Update();
            }
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                for (int i = 0; i < hintImages.Length; i++)
                {
                    if (hintImages[i].ShowHint())
                        hintImages[i].End();
                }
            }

        }
    }


    private ImageShake NewHintImage(ItemContainer itemContainer)
    {
        GameObject hintImageGO = new GameObject();
        Image baseImage = itemContainer.GetComponent<Image>();

        Image hintImage=hintImageGO.AddComponent<Image>(baseImage);
        hintImage.rectTransform.CloneRectTransform(baseImage.rectTransform);
        hintImage.transform.SetParent(baseImage.transform);
        hintImage.raycastTarget = false;
        hintImageGO.gameObject.SetActive(false);
        hintImageGO.name = "Hint " + itemContainer.name;
        if (hintImage.sprite == null)
        {
            hintImage.sprite = hintImageFiller;
        }

        ImageShake imageShake = new ImageShake(hintImage, itemContainer, effectMaxDuration);
        return imageShake;
    }

    private class ImageShake {
        private ItemContainer itemContainer;          
        private Image image;
        private float t = 0;
        private Color startColor;
        private Vector3 startScale;
        private Vector3 targetScale;
        private Quaternion startingRotation;
        private Color32 targetColor;
        private float shakeRange = 20;
        private float duration;

        public void Start()
        {
                t = 0;
                image.color = startColor;
                image.transform.localScale = startScale;
                image.transform.rotation = startingRotation;
                image.gameObject.SetActive(true);           
        }
        public void Update()
        {            
                image.color = Color.Lerp(image.color, targetColor, t);
                image.transform.localScale = Vector3.Lerp(image.transform.localScale, targetScale, t);
                Shake();
                t += Time.deltaTime / duration;           
        }

        public void End()
        {
            image.gameObject.SetActive(false);
        }


        public bool ShowHint()
        {
            if (!itemContainer.gameObject.activeSelf)
            {
                return false;
            }

            if (itemContainer.PlayerAction == Action.TryToOpen)
            {
                return true;
            }

            if (!itemContainer.UsedItem)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Shake()
        {
            float z = Random.value * shakeRange - (shakeRange / 2);
            image.transform.eulerAngles = new Vector3(image.transform.rotation.x, image.transform.rotation.y, image.transform.rotation.z + z);
        }
     
        public ImageShake(Image image,ItemContainer itemContainer,float duration)
        {
            this.itemContainer = itemContainer;
            this.duration = duration;
            this.image = image;
            this.startColor = image.color;
            this.startScale = image.transform.localScale;
            this.startingRotation = image.transform.rotation;
            this.targetScale = new Vector3(startScale.x*1.5f, startScale.y * 1.5f, startScale.z * 1.5f);
            this.targetColor = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }




}


