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
    let scrollIndicator : UIActivityIndicatorView! = UIActivityIndicatorView(activityIndicatorStyle: UIActivityIndicatorViewStyle.whiteLarge)
    let reloadButtonIndicator : UIActivityIndicatorView! = UIActivityIndicatorView(activityIndicatorStyle: UIActivityIndicatorViewStyle.whiteLarge)
    
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
        
        //テーブルのデータを取ってきて更新
        loadTable()

        
        //インディケータの設定
        setIndicator()
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
            next.returnAction = { self.loadTable() }
            
        }
    }
    
    
    func tableView(_ table: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        return self.myDataSource.count
        
    }
    
    
    //各セルの要素を設定する
    func tableView(_ table: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let _m = self.myDataSource[indexPath.row]
        
        //日付を「月/日　時:分」に変更
        let datetimeFormatter = DateFormatter()
        datetimeFormatter.dateFormat = "yyyy-MM-dd HH:mm:ss"
        let date: NSDate? = datetimeFormatter.date(from: _m.post_date) as NSDate?
        
        let outputFormatter = DateFormatter()
        outputFormatter.dateFormat = "MM/dd HH:mm"
        let outputDate = outputFormatter.string(from: date! as Date)
        
        //セルを作る
        let customCell = table.dequeueReusableCell(withIdentifier: "customCell", for: indexPath) as! CustomCell
        
        customCell.dungeonLabel.text = _m.dungeon_name
        customCell.readerLabel.text = _m.my_reader
        customCell.commentLabel.text = _m.comment
        customCell.postDateLabel.text = outputDate
        
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

    func setIndicator(){
        
        //上までスクロールして更新する時のインディケータの設定
        scrollIndicator.frame = CGRect(x: self.view.bounds.width / 2 - 25, y: (self.navigationController?.navigationBar.frame.size.height)! + UIApplication.shared.statusBarFrame.height, width: 50, height:50)
        scrollIndicator.color = UIColor.green
        self.view.addSubview(scrollIndicator)
        self.view.bringSubview(toFront: scrollIndicator)
        
        //更新ボタンを押した時のインディケータの設定
        reloadButtonIndicator.center = self.view.center
        reloadButtonIndicator.color = UIColor.green
        reloadButtonIndicator.backgroundColor = UIColor.lightGray
        reloadButtonIndicator.layer.masksToBounds = true
        reloadButtonIndicator.layer.cornerRadius = 5.0
        reloadButtonIndicator.layer.opacity = 0.8
        self.view.addSubview(reloadButtonIndicator)
        self.view.bringSubview(toFront: reloadButtonIndicator)

        
    }

    
    func loadTable(){
        
        //Webサーバに対してHTTP通信のリクエストを出してデータを取得
        let listUrl = "http://52.199.28.109/puzd_api.php"
        let parameters: Parameters = ["dungeon_id": searchDungeonId]
        Alamofire.request(listUrl, parameters: parameters).responseJSON{ response in
            
            let json = JSON(response.result.value ?? 0)
            if(json["msg"] == "on"){
                
                    self.myDataSource.removeAll()
                
                let item = json["items"]
                var jsonarray = item.arrayValue
                
                //Multidataのインスタンスを作りmyDataSourseに挿入
                for i in (0..<jsonarray.count){
                    let _m = Multidata()
                    _m.getlist(data:jsonarray[i])
                    self.myDataSource.append(_m)
                }
                
                //テーブルの更新
                print("テーブルをロードしました")
                self.listTable.reloadData()
                // アニメーションを停止
                self.scrollIndicator.stopAnimating()
                self.reloadButtonIndicator.stopAnimating()
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
    
    func scrollViewDidScroll(_ scrollView: UIScrollView) {
        
        let statusHeight = UIApplication.shared.statusBarFrame.height
        let navigationBarHeight = self.navigationController?.navigationBar.frame.size.height
        if(self.listTable.contentOffset.y + navigationBarHeight! + statusHeight < -60){
            
            self.scrollIndicator.startAnimating()
            
        }else{
            
            self.scrollIndicator.stopAnimating()
            
        }
        
    }
    
    func scrollViewDidEndDragging(_ scrollView: UIScrollView, willDecelerate decelerate: Bool) {
        
        let statusHeight = UIApplication.shared.statusBarFrame.height
        let navigationBarHeight = self.navigationController?.navigationBar.frame.size.height
        
        if(self.listTable.contentOffset.y + navigationBarHeight! + statusHeight < -60){
            
            loadTable()
            
        }else if(self.listTable.contentOffset.y >= self.listTable.contentSize.height - self.listTable.bounds.size.height
            && self.listTable.contentOffset.y + navigationBarHeight! + statusHeight > 60){
            
//            loadTable()
        }
    }

    @IBAction func pushReloadButton(_ sender: UIButton) {
        
        //更新ボタンを押して更新
        self.reloadButtonIndicator.startAnimating()
        self.listTable.contentOffset = CGPoint(x: 0,y: -self.listTable.contentInset.top)
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

//コメントは必須にしないok
//下スクロールの追加の更新は使わない。ok
//更新でエフェクトつけるok

//セルのレイアウト
//日付を「月/日　時：分」にするok


