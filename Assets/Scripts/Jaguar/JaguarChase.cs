using UnityEngine;

public class JaguarChase : JaguarBehavior
{
    private void OnDisable()
    {
        this.jaguar.scatter.Enable();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled)// to ma hindus ale tu nie widzi idk  if(... && !this.jaguar.frightened.enable)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach(Vector2 availableDirection in node.availableDirection)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.jaguar.target.position - newPosition).sqrMagnitude;

                if(distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            this.jaguar.movement.SetDirection(direction);
        }
    }

}
