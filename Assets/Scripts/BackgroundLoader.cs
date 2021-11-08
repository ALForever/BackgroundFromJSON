using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackgroundLoader : MonoBehaviour
{
    public TextAsset json;
    public BackgroundTextureArray background;
    private SpriteRenderer backgroundSprite;
    private GameObject spriteObject;

    void Start()
    {
        background = JsonUtility.FromJson<BackgroundTextureArray>(json.text);
        spriteObject = new GameObject("Sprite Object", typeof(SpriteRenderer)/*, typeof(RectTransform)*/);
        backgroundSprite = spriteObject.GetComponent<SpriteRenderer>();
        BackgroundLoading();
        Destroy(spriteObject);        
    }

    private void BackgroundLoading()
    {
        for (int i = 0; i < background.List.Length; i++)
        {
            backgroundSprite.sprite = BackgroundSpriteCreate(background.List[i]);
            Instantiate(spriteObject, new Vector2(background.List[i].X, background.List[i].Y), Quaternion.identity, transform);

        }
    }
    private Sprite BackgroundSpriteCreate(BackgroundTexture backgroundTexture)
    {
        Sprite sprite = Resources.Load<Sprite>(backgroundTexture.Id);
        string path = AssetDatabase.GetAssetPath(sprite);
        TextureImporter importSettings = AssetImporter.GetAtPath(path) as TextureImporter;
        importSettings.spritePivot = Vector2.up;
        AssetDatabase.Refresh();
        Sprite sprite1 = Resources.Load<Sprite>(backgroundTexture.Id);


        return sprite1;
    }
}
