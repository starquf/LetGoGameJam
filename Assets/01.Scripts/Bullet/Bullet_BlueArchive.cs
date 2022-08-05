using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_BlueArchive : Bullet
{

    [SerializeField]
    protected LineRenderer m_LineRenderer;
    [SerializeField]
    protected bool m_AddCollider = false;
    [SerializeField]
    protected PolygonCollider2D m_PolygonCollider2D;
    [SerializeField]
    protected Camera m_Camera;
    protected List<Vector2> m_Points;

    protected override void Awake()
    {
        base.Awake();
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }
        m_Points = new List<Vector2>();
    }

    public override void SetOwner(bool isEnemy)
    {
        isEnemyBullet = isEnemy;
        curSpeed = isEnemyBullet ? curSpeed * 0.7f : curSpeed;
        ChangeState(BulletState.MoveForward);
    }

    public override void Despawned()
    {
        ChangeState(BulletState.MoveForward);
    }

    public override void Spawned()
    {
        curSpeed = bulletSpeed;
        transform.position = Vector3.zero;
        m_LineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(BulletLifetime());
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public void SetRenderer(Vector3 position)
    {
        m_Points.Clear();
        m_LineRenderer.positionCount = 2;

        position.Set(position.x, position.y, 0);
        m_Points.Add(position);
        m_LineRenderer.SetPosition(0, position);
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_Points.Add(position);
        position.Set(position.x, position.y, 0);
        m_LineRenderer.SetPosition(1, position);
        


        if (m_PolygonCollider2D != null && m_AddCollider && m_Points.Count > 1)
        {
            List<Vector2> edges1 = new List<Vector2>();
            List<Vector2> edges2 = new List<Vector2>();
           
            Vector2 pos = m_LineRenderer.GetPosition(0); 
            Vector2 pos1 = new Vector3(pos.x + m_LineRenderer.startWidth, pos.y + m_LineRenderer.startWidth);
            Vector2 pos2 = new Vector3(pos.x - m_LineRenderer.startWidth, pos.y - m_LineRenderer.startWidth);
            edges1.Add(pos1);
            edges2.Add(pos2);
            pos = m_LineRenderer.GetPosition(1);
            pos1 = new Vector3(pos.x + m_LineRenderer.startWidth, pos.y + m_LineRenderer.startWidth);
            pos2 = new Vector3(pos.x - m_LineRenderer.startWidth, pos.y - m_LineRenderer.startWidth);
            edges1.Add(pos2);
            edges2.Add(pos1);


            for (int i = edges2.Count- 1; i > 0 ; i--)
            {
                edges1.Add(edges2[i]);

            }
           

            m_PolygonCollider2D.SetPath(0, edges1);
        }

      


    }

   

}
