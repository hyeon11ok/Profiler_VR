using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템의 이름, 정보를 표시해줄 이미지 정보를 담고 있는 객체
/// </summary>
public class Item : MonoBehaviour
{
    private Sprite itemInfo;
    private string itemName;

    public Sprite ItemInfo {  get { return itemInfo; } }
    public string ItemName { get {  return itemName; } }

    // Start is called before the first frame update
    void Start() {
        itemName = transform.name;
        itemInfo = Resources.Load<Sprite>("Sprites/Item/" + transform.name);
    }
}
