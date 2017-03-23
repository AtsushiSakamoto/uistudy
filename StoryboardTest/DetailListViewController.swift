//
//  DetailListViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/03/01.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit
import GoogleMobileAds

class DetailListViewController: UIViewController , UITableViewDelegate, UITableViewDataSource, GADBannerViewDelegate {
    
    
    @IBOutlet weak var detailListTable: UITableView!
    @IBOutlet weak var bannerView: GADBannerView!
    
    var selectPostId: String = ""
    var selectRoomId: String = ""
    var selectReader: String = ""
    var selectComment: String = ""
    var selectContinyuity: String = ""
    var selectDungeonName: String = ""
    var continyuityString: String = ""
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        print("DetailListView viewDidLoad")
        //タイトルを取得して再設定する。
        self.title = self.title! + ""
        
        //高さを動的に変更
        self.detailListTable.estimatedRowHeight = 24
        self.detailListTable.rowHeight = UITableViewAutomaticDimension
        

        bannerView.delegate = self
        bannerView.adUnitID = "ca-app-pub-3607945421999798/5225503666"
        bannerView.rootViewController = self
        
        let request = GADRequest()
        request.testDevices = ["c1e9edc86b2dda4d4142510cdaee48b1"]
/*
        request.gender = .male
        var components = DateComponents()
        components.month = 3
        components.day = 13
        components.year = 1976
        request.birthday = Calendar.current.date(from: components)
        request.tag(forChildDirectedTreatment: true)
*/
        bannerView.load(request)
        
        
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
        
        if(self.selectContinyuity == "1"){
            continyuityString = "有"
        }else{
            continyuityString = "無"
        }
        
        if(indexPath.section == 0){
            switch indexPath.row {
                
            case 0 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "ダンジョン : " + self.selectDungeonName
                return cell
                
            case 1 :
                
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "ルームID : " + self.selectRoomId
//                cell.textLabel?.text = "ルームID : " + self.selectRoomId.substring(to: self.selectRoomId.index(self.selectRoomId.startIndex, offsetBy: 4)) + "****"
                
                return cell
                
            case 2 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "リーダー : " + self.selectReader
                
                
                return cell
                
            case 3 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "コンテニュー : " + self.continyuityString
                
                
                
                return cell
                
            default :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "コメント : " + self.selectComment
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
    
    func adViewDidReceiveAd(_ bannerView: GADBannerView) {
        bannerView.alpha = 0
        UIView.animate(withDuration: 0.5, animations: {
            bannerView.alpha = 1
        })
    }

}


//ListViewから値受けもらいok
//groupedでセクションを二つにし、マルチの情報とパズドラ起動ボタンの設置ok
//セルの高さを動的に変更ok
//ボタンを押した時、IDをコピーしてパズドラを起動するかどうかのアラート設置ok
//アラートでYESを押すとコピーしてパズドラ起動ok

//ルームID後半をアスタリスクにok



//チャットワークで送られた広告をここにはる！ok


