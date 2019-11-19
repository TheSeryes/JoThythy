using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField]
    private int m_PotionHealth = 10;

    private void GiveHealth()
    {
        //To DO  Changer le chemin  pour passer vers le InventoryManager
        //GameManager.Instance.Player.ReceiveHealth(m_PotionHealth);
        
    }
    /* 
        InventoryManager() 
            {

                List<CollectibleObj> Collectibles = new list<CollectibleObj>();

                public void AddObjInInventory(CollectibleObj aObj)
                {
                    Collectibles.Add(aObj);
                }

                public void Clear()
                {
                    Collectibles.Clear();
                }

                public RemoveItems(CollectibleObj aObj)
                {
                    for(int i = 0; i < Collectibles.Count; i++)
                    {
                        if(aObj == Collectibles[i])
                        {
                            Collectibles.Remove(Collectibles[i]);
                        }
                    }
                    
                }

                UseObj(CollectibleObj aObj)
                {
                    for(int i = 0; i < Collectibles.Count; i++)
                    {
                        if(aObj == Collectibles[i])
                        {
                            CollectibleObj obj = Instantiate(aObj).GetComponent<CollectibleObj>();
                            obj.Use();
                            
                            return;
                        }
                    }
                    
                }
            }
    
    
    */
    

    public void Use()
    {
        //GameManager.Instance.Player.Heal();
        //Destroy(this);
        //GameManager.Instance.Player.SwordAvailable();
    }
}
