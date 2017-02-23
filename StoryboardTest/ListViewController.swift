//
//  ListViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/03.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit
import Alamofire
import SwiftyJSON

class ListViewController: UIViewController, UITableViewDelegate, UITableViewDataSource {
    
    @IBOutlet weak var listTable: UITableView!
    var myDataSource : [Multidata] = []
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        print("listview　viewDidLoad")
        //タイトルを取得して再設定する。
        self.title = self.title! + ""
        
        
        //Webサーバに対してHTTP通信のリクエストを出してデータを取得
        let listUrl = "http://52.199.28.109/pazd_multi_api.php"
        Alamofire.request(listUrl).responseJSON{ response in
            
            let json = JSON(response.result.value ?? 0)
            var jsonarray = json.arrayValue
            
            //Multidataのインスタンスを作りmyDataSourseに挿入
            for i in (0..<jsonarray.count){
                let _m = Multidata()
                _m.getlist(data:jsonarray[i])
                self.myDataSource.insert(_m,at:0)
            }
            //テーブルの更新
            self.listTable.reloadData()
        }
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewDidDisappear(animated)
        print("listview WillAppear")
    }
    
   
    
    func tableView(_ table: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        return self.myDataSource.count
        
    }
    
    
    
    //各セルの要素を設定する
    func tableView(_ table: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = UITableViewCell(style: UITableViewCellStyle .default, reuseIdentifier: "Cell")
        let _m = self.myDataSource[indexPath.row]
        cell.textLabel?.text = "ダンジョンID : " + _m.dungeon_id
        return cell
        
    }
    
    //tap
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {

        performSegue(withIdentifier: "toDetail",sender: nil)
        
        
    }
   
}

//listviewにテーブルを置き、セルを表示
//Alamofireで投稿データを取ってくる
//セルをタップで詳細を表示する画面にpush

