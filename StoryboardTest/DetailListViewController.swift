//
//  DetailListViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/03/01.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit

class DetailListViewController: UIViewController , UITableViewDelegate, UITableViewDataSource {
    
    
      @IBOutlet weak var detailListTable: UITableView!
    var selectPostId: String = ""
    var selectRoomId: String = ""
    var selectReader: String = ""
    var selectComment: String = ""
    var selectContinyuity: String = ""
    var selectDungeonName: String = ""

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        print("DetailListView viewDidLoad")
        //タイトルを取得して再設定する。
        self.title = self.title! + ""
        
        //高さを動的に変更
        self.detailListTable.estimatedRowHeight = 24
        self.detailListTable.rowHeight = UITableViewAutomaticDimension
        
    }
    
    override func viewDidAppear(_ animated: Bool){
        //Viewにタップジェスチャーリコグナイザーを配置
        
        print("detaillistview viewDidApper")
    }
    
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 2
    }
    
    func tableView(_ table: UITableView, numberOfRowsInSection section: Int) -> Int {
        if(section == 0){
            return 5
        }else{
            return 1
        }
    }
    
   
    
    //各セルの要素を設定する
    func tableView(_ table: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        
        if(indexPath.section == 0){
            switch indexPath.row {
                
            case 0 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "ダンジョン:" + self.selectDungeonName
                return cell
                
            case 1 :
                
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                
                cell.textLabel?.text = "ルームID:" + self.selectRoomId
               
                return cell
                
            case 2 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "リーダー:" + self.selectReader
                
                
                return cell
                
            case 3 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "コンテニュー:" + self.selectContinyuity
                
      
                
                return cell
                
            default :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "コメント:" + self.selectComment
                cell.textLabel?.numberOfLines = 0
                return cell
                
            }
            
        }else{
            
            let puzdStartCell = table.dequeueReusableCell(withIdentifier: "puzdStartCell", for: indexPath) as! PuzdStartCell
            puzdStartCell.puzdStartButton .addTarget(self, action: #selector(DetailListViewController.pushPuzdStartButton(_:)), for: UIControlEvents.touchUpInside)
            
            return puzdStartCell
        }
    }
    
    //tap
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        //        print(indexPath.row)
        tableView.deselectRow(at: indexPath, animated: true)
    }

    @IBAction func pushPuzdStartButton(_ sender: UIButton){
        
        //ボタンを押したらIDコピーとパズドラ起動するかどうかのアラート表示
        let alert: UIAlertController = UIAlertController(title: "", message: "ルームIDをコピーして、パズドラを起動しますか？", preferredStyle: .alert)
        let okAction = UIAlertAction(title: "YES", style: .default) { action in
            
            //クリップボードにコピー
            let board = UIPasteboard.general
            board.string = self.selectRoomId
            
            // パズドラ起動
            if #available(iOS 10.0, *) {
                UIApplication.shared.open(URL(string: "PUZZLEANDDRAGONS://")!)
            } else {
                // Fallback on earlier versions
                UIApplication.shared.openURL(URL(string: "PUZZLEANDDRAGONS://")!)
            }
            
        }
        
        let noAction = UIAlertAction(title: "NO", style: .default)
        alert.addAction(okAction)
        alert.addAction(noAction)
        self.present(alert, animated: true, completion: nil)
        
    
    }

}


//ListViewから値受けもらい
//groupedでセクションを二つにし、マルチの情報とパズドラ起動ボタンの設置
//セルの高さを動的に変更
//ボタンを押した時、IDをコピーしてパズドラを起動するかどうかのアラート設置
//アラートでYESを押すとコピーしてパズドラ起動







