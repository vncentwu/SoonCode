	

    static var curHp : float = 300.0;
    static var maxHp : float = 300.0;
    static var curMana : float = 50.0;
    static var maxMana : float = 50.0;
    var HpBarTexture : Texture2D;
    var ManaBarTexture : Texture2D;
    var hpBarLength : float;
    var percentOfHp : float;
    var manaBarLength : float;
    var percentOfMana :float;
     
     
    function OnGUI () {
     
            if (curHp > 0) {
                    GUI.DrawTexture(Rect((Screen.width/2) - 100, 10, hpBarLength, 10), HpBarTexture);
            }
            if (curMana > 0) {
                    GUI.DrawTexture(Rect((Screen.width/2) - 100, 20, manaBarLength, 10), ManaBarTexture);
            }
    }
     
    function Update () {
     
            percentOfHP = curHp/maxHp;
            hpBarLength = percentOfHP*100;
     
            percentOfMana = curMana/maxMana;
            manaBarLength = percentOfMana*100;
     
            if(Input.GetKeyDown("h")) {
                    curHp -= 10.0;
            }
     
            if(Input.GetKeyDown("m")) {
                    curMana -= 10.0;
            }
    }

