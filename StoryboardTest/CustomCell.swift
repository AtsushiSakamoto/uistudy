//
//  CustomCell.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/24.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit

class CustomCell: UITableViewCell {

    @IBOutlet weak var dungeonLabel: UILabel!
    @IBOutlet weak var readerLabel: UILabel!
    @IBOutlet weak var commentLabel: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        
    }
    
    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)
        
        // Configure the view for the selected state
    }
    
}
