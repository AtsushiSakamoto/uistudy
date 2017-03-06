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

class ListViewController: UIViewController, UITableViewDelegate, UITableViewDataSource{
    
    @IBOutlet weak var listTable: UITableView!
    @IBOutlet weak var reloadButton: UIButton!
    
    var myDataSource : [Multidata] = []
    var selectPostId : String = ""
    var selectRoomId: String = ""
    var selectReader: String = ""
    var selectComment: String = ""
    var selectContinyuity: String = ""
    var selectDungeonName: String = ""
    var searchDungeonId: Int = 0
    var load_position = 0
    var On_removeAll = 0
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        print("listview　viewDidLoad")
        //タイトルを取得して再設定する。
        self.title = self.title! + ""
        
        loadTable()
        
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
            next.returnAction = { self.searchDungeon() }
            
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
        tableView.deselectRow(at: indexPath, animated: true)
        let row = self.myDataSource[indexPath.row]
        self.selectPostId = row.post_id
        self.selectRoomId = row.room_id
        self.selectReader = row.my_reader
        self.selectDungeonName = row.dungeon_name
        self.selectContinyuity = row.continuity
        self.selectComment = row.comment
        
        performSegue(withIdentifier: "toDetail",sender: nil)
 
        
    }

    func searchDungeon(){
        
        
        self.load_position = 0
        self.On_removeAll = 1
        self.loadTable()
    }
    
    func loadTable(){
        
        //Webサーバに対してHTTP通信のリクエストを出してデータを取得
        let listUrl = "http://52.199.28.109/puzd_reload.php"
        let parameters: Parameters = ["dungeon_id": searchDungeonId,"load_position": load_position]
        Alamofire.request(listUrl, parameters: parameters).responseJSON{ response in
            
            let json = JSON(response.result.value ?? 0)
            if(json["msg"] == "on"){
                
                if(self.On_removeAll == 1){
                    self.myDataSource.removeAll()
                    self.On_removeAll = 0
                }
                let item = json["items"]
                var jsonarray = item.arrayValue
                
                //Multidataのインスタンスを作りmyDataSourseに挿入
                for i in (0..<jsonarray.count){
                    let _m = Multidata()
                    _m.getlist(data:jsonarray[i])
                    self.myDataSource.append(_m)
                }
                
                //テーブルの更新
                self.load_position = self.load_position + 15
                print("テーブルをロードしました")
                self.listTable.reloadData()
            }else if(json["msg"] == "off"){
                
                print("検索するdungeon_idが文字列")
                
            }else{
                
                let alert: UIAlertController = UIAlertController(title: "通信失敗しました", message: "", preferredStyle: .alert)
                let okAction = UIAlertAction(title: "OK", style: .default)
                alert.addAction(okAction)
                self.present(alert, animated: true, completion: nil)
                
            }
 
        }
 
    }
    
    func scrollViewDidEndDragging(_ scrollView: UIScrollView, willDecelerate decelerate: Bool) {
        
        let statusHeight = UIApplication.shared.statusBarFrame.height
        let navigationBarHeight = self.navigationController?.navigationBar.frame.size.height
        
        if(self.listTable.contentOffset.y + navigationBarHeight! + statusHeight < -60){
           
            self.load_position = 0
            self.On_removeAll = 1
            loadTable()
            
            
        }else if(self.listTable.contentOffset.y >= self.listTable.contentSize.height - self.listTable.bounds.size.height
            && self.listTable.contentOffset.y + navigationBarHeight! + statusHeight > 60){
            
            loadTable()
        }
    }

    @IBAction func pushReloadButton(_ sender: UIButton) {
        //更新ボタンを押して更新
        self.listTable.contentOffset = CGPoint(x: 0,y: -self.listTable.contentInset.top)
        self.load_position = 0
        self.On_removeAll = 1
        loadTable()
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
//投稿で記号などを入力した場合の処理の確認ok


//phpの返却値を値が０なのか通信失敗したのか判別できるようにok
//postをgetにok


//更新ボタンの設置ok
//ボタンで更新後トップに移動ok
//引っ張って更新ok
//タップで選択解除ok




