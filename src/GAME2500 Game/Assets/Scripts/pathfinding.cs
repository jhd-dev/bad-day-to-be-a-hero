


//TODO: Find/make move(enum goDir) and checkEnemy(x, y) function
/*
public class AI {

    public enum MinionMove {wait, attack, move};
    public enum goDir {up, down, left, right};

    public static MinionMove excecuteMinionMove(int currGridX, int currGridY, int sightRad, int attackRad, int moveCount) {
        
        bool triggerAction = false; //If minion is attacking/moving, false if waiting
        MinionMove thisMinionMove = MinionMove.wait;

        int enemyLocationX, enemyLocationY; //X and Y locations of seen enemies
        
        //Check if enemy in sight
        bool inSight;
        for(int i = 1; i <= sightRad; i++) { //Cycle through close radius before outer radii
            for(int x = currGridX-i; x <= currGridX+i; x++) { //Cycle through left to right
                for(int y = currGridY-i; y <= currGridY+i; y++) { //Bottom to top
                    if(checkEnemy(x, y)) {
                        triggerAction = true; //'Turn on' minion if found enemy within sight radius
                        enemyLocationX = x;
                        enemyLocationY = y;
                    }
                }
            }
        }

        //Only enables minion when triggered
        if (triggerAction) {
        
            //TODO: Maybe: If enemy in sight, lock on and attack or move towards them OR attack closest enemy        
            //Attack if enemy is in range
            
            if( ((currGridX + attackRad) >= enemyLocationX) && ((currGridX + attackRad) >= enemyLocationX) ) {
                attack(enemyLocationX, enemyLocationY);
                if(!checkEnemy(enemyLocationX, enemyLocationY)) {
                    triggerAction = false; //Disable minion if enemy is killed until the next move
                }
            }
            else { //If enemy is not in attack range, move towards them
                int xDistFromEnemy = currGridX - enemyLocationX;
                int yDistFromEnemy = currGridY - enemyLocationY;

                if(Math.Abs(xDistFromEnemy) >= Math.Abs(yDistFromEnemy)) { //If x difference is less than or same as y difference, move left or right
                    if(xDistFromEnemy > 0) {
                        move(left);
                    }
                    if(xDistFromEnemy < 0) {
                        move(right);
                    }
                }
                else { //Otherwise move either up or down
                    if(yDistFromEnemy > 0) {
                        move(down);
                    }
                    if(yDistFromEnemy < 0) {
                        move(up);
                    }
                }
            } 
            

        }
    }
}*/