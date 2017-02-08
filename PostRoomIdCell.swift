//
//  PostRoomIdCell.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/03.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit
protocol PostRoomIdCellDelegate: class {
    func textFieldDidEndEditing(cell: PostRoomIdCell, value: NSString)
}

class PostRoomIdCell: UITableViewCell, UITextFieldDelegate {

    
    @IBOutlet weak var roomIdLabel: UILabel!
    @IBOutlet weak var roomIdTextField: UITextField!
    weak var delegate: PostRoomIdCellDelegate! = nil

    
    override func awakeFromNib() {
        
        super.awakeFromNib()
        // Initialization code
        roomIdTextField.delegate = self
    }
    
    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)
        
        // Configure the view for the selected state
    }

    internal func textFieldDidEndEditing(_ textField: UITextField) {
        print("testedirt")
//        self.delegate.textFieldDidEndEditing(cell: self, value: roomIdTextField.text! as NSString)
    }

}
