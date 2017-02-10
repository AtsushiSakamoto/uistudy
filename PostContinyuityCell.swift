//
//  PostContinyuityCell.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/03.
//  Copyright © 2017年 com.imple. All rights reserved.
//


import UIKit
class PostContinyuityCell: UITableViewCell {
    

    @IBOutlet weak var continyuityLabel: UILabel!
    
    @IBOutlet weak var continyuityTextField: UITextField!

    
    
    override func awakeFromNib() {
        
        super.awakeFromNib()
        // Initialization code
    }
    
    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)
        
        // Configure the view for the selected state
    }
    
}
