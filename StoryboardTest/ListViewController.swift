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
    var selectPostId : String = ""
    var selectRoomId: String = ""
    var selectReader: String = ""
    var selectComment: String = ""
    var selectContinyuity: String = ""
    var selectDungeonName: String = ""
    var searchDungeonId: Int = 0
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        print("listview　viewDidLoad")
        //タイトルを取得して再設定する。
        self.title = self.title! + ""
        
        loadData()
        
        self.listTable.estimatedRowHeight = 60
        self.listTable.rowHeight = UITableViewAutomaticDimension
        
        }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewDidDisappear(animated)
        print("listview WillAppear")

        
        
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?)  {
        
        if(segue.identifier == "toDetail"){
            
            let next = segue.destination as! DetailListViewController
            next.selectPostId = selectPostId
            next.selectRoomId = selectRoomId
            next.selectReader = selectReader
            next.selectDungeonName = selectDungeonName
            next.selectContinyuity = selectContinyuity
            next.selectComment = selectComment
        }
        
        if(segue.identifier == "search"){
            
            let next = segue.destination as! SearchViewController
            next.returnAction = { self.updateTableView() }
            
        }
    }
    
   
    func loadData(){
        
        //Webサーバに対してHTTP通信のリクエストを出してデータを取得
        let listUrl = "http://52.199.28.109/puzd_api.php"
        let parameters: Parameters = ["dungeon_id": searchDungeonId]
        Alamofire.request(listUrl, method: .post, parameters: parameters, encoding: JSONEncoding.default).responseJSON{ response in
            
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
            print("テーブルをロードしました")
        }

    }
    
    
    
    func tableView(_ table: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        return self.myDataSource.count

    }

      
    //各セルの要素を設定する
    func tableView(_ table: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let _m = self.myDataSource[indexPath.row]
        
        let customCell = table.dequeueReusableCell(withIdentifier: "customCell", for: indexPath) as! CustomCell
        
        customCell.dungeonLabel.text = _m.dungeon_name
        
        customCell.readerLabel.text = _m.my_reader
        
        customCell.commentLabel.text = _m.comment
        
        customCell.postDateLabel.text = _m.post_date
        
        
        return customCell
    }


    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {

        //タップして詳細画面に遷移
        let row = self.myDataSource[indexPath.row]
        self.selectPostId = row.post_id
        self.selectRoomId = row.room_id
        self.selectReader = row.my_reader
        self.selectDungeonName = row.dungeon_name
        self.selectContinyuity = row.continuity
        self.selectComment = row.comment
        
        performSegue(withIdentifier: "toDetail",sender: nil)
        
        
    }
    
    func updateTableView() {
        
        self.myDataSource.removeAll()
        loadData()
    }
    
}

//listviewにテーブルを置き、セルを表示ok
//Alamofireで投稿データを取ってくるok
//セルをタップで詳細を表示する画面にpush

//ナビゲーションバーにビューがかからないようにするok
//タブバーにも同じようにok


//使うタブを左にok
//検索はとりあえずダンジョンを選ばせて表示ok
//ダンジョンをデータベースに加える。ok
//投稿で記号などを入力した場合の処理の確認

