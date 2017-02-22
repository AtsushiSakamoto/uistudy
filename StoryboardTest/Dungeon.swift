//
//  Dungeon.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/20.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit
import SwiftyJSON

class Dungeon{
    var dungeon_id : Int! = nil
    var dungeon_name : String = ""
    
    func getlist(data:JSON){
        self.dungeon_id = data["dungeon_id"].intValue
        self.dungeon_name = data["dungeon_name"].stringValue
    }
}
