using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

[System.Serializable]

public class ParallaxLayer
{
    //一个背景层
    [SerializeField] private Transform background;//背景层
    [SerializeField] private float parallaxMultiplier;//背景的移动速度
    [SerializeField] private float imageWidthOffset = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()//计算图像宽度
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;//一张背景图片的完整宽度
        imageHalfWidth = imageFullWidth / 2;//图片半宽
    }

    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)//背景循环播放
    {
        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;

        if(imageRightEdge < cameraLeftEdge)
        {
            background.position += Vector3.right * imageFullWidth;
        }
        else if(imageLeftEdge > cameraRightEdge)
        {
            background.position += Vector3.right * -imageFullWidth;
        }
        
    }
    
}
