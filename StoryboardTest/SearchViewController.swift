//
//  SearchViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/03/01.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit
import SwiftyJSON
import Alamofire

class SearchViewController: UIViewController,UITableViewDelegate,UITableViewDataSource{



    @IBOutlet weak var searchTable: UITableView!


    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }




    func tableView(_ table: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 5
    }
    



    func tableView(_ table: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
        cell.textLabel?.text = "セル"
        return cell
 
    }






}
