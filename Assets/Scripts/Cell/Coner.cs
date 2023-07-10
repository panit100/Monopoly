using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coner : Grid
{
   public TEAM team;

   void Start() 
   {
        cellType = CELLTYPE.CONER;
   }

   public void HealPlayer(PlayerController player)
   {
      if(player.Team == team)
         player.Heal(3);
      else
         player.Heal(1);

   }
}
