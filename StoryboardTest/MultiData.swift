//
//  MultiData.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/23.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit
import SwiftyJSON

class Multidata{
    var post_id : String = ""
    var user_id : String = ""
    var room_id : String = ""
    var my_reader : String = ""
    var comment : String = ""
    var continuity : String = ""
    var dungeon_id : String = ""
    var post_date : String = ""
    
    func getlist(data:JSON){
        self.post_id = data["post_id"].stringValue
        self.user_id = data["user_id"].stringValue
        self.room_id = data["room_id"].stringValue
        self.my_reader = data["my_reader"].stringValue
        self.comment = data["comment"].stringValue
        self.continuity = data["continuity"].stringValue
        self.dungeon_id = data["dungeon_id"].stringValue
        self.post_date = data["post_date"].stringValue
    }
}
