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
    var dungeonList: [Dungeon] = []
    let all = Dungeon()
    var searchId: Int!
    var returnAction: (() -> Void)?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        //タイトルを取得して再設定する。
        self.title = self.title! + ""
        all.dungeon_id = 0
        all.dungeon_name = "全て"
        dungeonList.append(all)
        
        //ダンジョン一覧をサーバーに問いかけ取得
        let listUrl = "http://52.199.28.109/pazd_dungeon_api.php"
        Alamofire.request(listUrl).responseJSON{ response in
            
            let json = JSON(response.result.value ?? 0)
            var jsonarray = json.arrayValue
            
            for i in (0..<jsonarray.count){
                let row = Dungeon()
                row.getlist(data:jsonarray[i])
                self.dungeonList.append(row)
            }
            self.searchTable.reloadData()
        }
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }


    
    func tableView(_ table: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        return self.dungeonList.count
    }
    

    func tableView(_ table: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        //セルの作成
        let cell = UITableViewCell(style: UITableViewCellStyle .default, reuseIdentifier: "Cell")
        let row = self.dungeonList[indexPath.row]
        cell.textLabel?.text = row.dungeon_name
        return cell
        
 
    }


    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        tableView.deselectRow(at: indexPath, animated: true)
        self.searchId = self.dungeonList[indexPath.row].dungeon_id
        let nav = self.navigationController!
        
        //呼び出し元のView Controllerを遷移履歴から取得しパラメータを渡す
        let next = nav.viewControllers[nav.viewControllers.count-2] as! ListViewController
        next.searchDungeonId = self.searchId
        
        //ListViewのテーブルを更新
        self.returnAction!()

        self.navigationController!.popViewController(animated: true)
    }


    
    
    
    
    
    
    
    
    
    
    


}
