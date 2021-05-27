using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/*
 * Clase contenedora de constantes, para el uso de nombrs de tags o nombres de los gameobjects
 */
public class Constants 
{
    #region UI GameObjects
    
    public const string GO_TXT_POINTS = "Points";
    public const string GO_TXT_TIMER = "Time";
    public const string GO_TXT_WINDINFO = "WindInfo";
    public const string GO_SLIDER_FORCE = "Force";
    public const string GO_SLIDER_ANGLE = "Angle";
    
    #endregion

    #region MyRegion

    public const string GO_CASTLE = "Castle";
    public const string GO_ENTRANCE_CASTLE = "Entrance";
    

    #endregion

    #region GameObjects Tags

    public const string TAG_GROUND = "Ground";
    public const string TAG_ENEMY= "Enemy";
    public const string TAG_ENTRANCE= "Entrance";
  
    #endregion


    #region PrefabsName
    public const string nameScore = "score";
    public const string Ball = "Sphere";
    #endregion
}
