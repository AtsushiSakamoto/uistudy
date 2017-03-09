//
//  SupportViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/03/09.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit

class SupportViewController: UIViewController, UITableViewDelegate, UITableViewDataSource{
    
    @IBOutlet weak var supportTable: UITableView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        
        //タイトルを取得して再設定する。
        self.title = self.title! + ""
        
        
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 3
    }
    
    func tableView(_ table: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        switch section {
        case 0:
            return 2
        case 1:
            return 4
        default:
            return 1
        }
        
    }
    
    func tableView(_ table: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        switch indexPath.section {
        case 0:
            switch indexPath.row {
            case 0:
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "問い合わせ・不具合連絡"
                return cell
            default:
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "利用規約(EULA)"
                return cell
            }
        case 1:
            switch indexPath.row {
            case 0:
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "公式サイト"
                return cell
            case 1:
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "攻略"
                return cell
            case 3:
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "ゲリラ時間割"
                return cell
            default:
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "パズドラ最新情報"
                return cell
            }
        default:
            let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
            cell.textLabel?.text = "レビューのお願い"
            return cell
        }
        
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        tableView.deselectRow(at: indexPath, animated: true)
        
        switch indexPath.section {
        case 0:
            switch indexPath.row {
            case 0:
                print("問い合わせ")
            default:
                print("利用規約(EULA)")
            }
        case 1:
            switch indexPath.row {
            case 0:
                let url = NSURL(string: "https://pad.gungho.jp/member/")
                if UIApplication.shared.canOpenURL(url! as URL){
                    UIApplication.shared.openURL(url! as URL)
                }
            case 1:
                let url = NSURL(string: "https://game8.jp/games/5")
                if UIApplication.shared.canOpenURL(url! as URL){
                    UIApplication.shared.openURL(url! as URL)
                }
            case 3:
                let url = NSURL(string: "http://pad.zap.jp.net")
                if UIApplication.shared.canOpenURL(url! as URL){
                    UIApplication.shared.openURL(url! as URL)
                }

            default:
                print("パズドラ最新情報")
            }
        default:
            let alert: UIAlertController = UIAlertController(title: "レビューのお願い", message: "今後も無料でサービスを提供したいのでレビューお願いいたします。", preferredStyle: .alert)
            let okAction = UIAlertAction(title: "レビューする", style: .default) { action in
                
                print("レビュー画面に移動")
                
            }
            
            let noAction = UIAlertAction(title: "また今度", style: .default)
            alert.addAction(okAction)
            alert.addAction(noAction)
            self.present(alert, animated: true, completion: nil)
        }
        
        
        
        
    }
    
}


//ウェブビューを作るアプリ内で写す
