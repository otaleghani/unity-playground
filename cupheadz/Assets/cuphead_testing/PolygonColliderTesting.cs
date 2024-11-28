using UnityEngine;
using System.Collections.Generic;

public class PolygonColliderTesting : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      var sprite = GetComponent<SpriteRenderer>().sprite;
      var polygonCollider2D = GetComponent<PolygonCollider2D>();
      var pointsList = new List<Vector2>();
      
      sprite.GetPhysicsShape(0, pointsList);
      
      polygonCollider2D.points = pointsList.ToArray();
        
    }
}
