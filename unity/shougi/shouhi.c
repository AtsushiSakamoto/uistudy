//
//  shouhi.c
//  
//
//  Created by 坂本篤志 on 2017/05/18.
//
//

#include <stdio.h>

int main(void){
    
    int i,j,k;
    
    for(i = 1; i <= 9; i++){
        
        for(j = 1;j <= 9; j++){
            
            printf("//マス%d−%dをタップ\npublic void PushButtonMasu%d%d(){\nSelectMasu (%d, %d);\n}\n",i,j,i,j,i,j);
        }
    }
    return 0;
}
