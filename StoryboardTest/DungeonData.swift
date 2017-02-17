//
//  DungeonData.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/17.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit
import SwiftyJSON

class DungeonData{
    var dungeon_id : String = ""
    var dungeon_name : String = ""
    
    func getlist(data:JSON){
        self.dungeon_id = data["dungeon_id"].stringValue
        self.dungeon_name = data["dungeon_name"].stringValue
            }
}

