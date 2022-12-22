using UnityEngine;

public class JaguarScatter : JaguarBehavior
{
    private void OnDisable()
    {
        this.jaguar.chase.Enable();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if(node != null && this.enabled)// to ma hindus ale tu nie widzi idk  if(... && !this.jaguar.frightened.enable)
        {
            int index = Random.Range(0, node.availableDirection.Count);
            //zeby ziomek se nie chodzi³ lewo prawo bo to dziwne lol
            if(node.availableDirection[index] == -this.jaguar.movement.direction && node.availableDirection.Count > 1)
            {
                index++;
                if(index >= node.availableDirection.Count)
                {
                    index = 0;
                }
            }

            this.jaguar.movement.SetDirection(node.availableDirection[index]);

        }
    }
}
