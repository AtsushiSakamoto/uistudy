//
//  DungeonViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/16.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit
import Alamofire
import SwiftyJSON


class DungeonViewController: UIViewController, UITableViewDelegate, UITableViewDataSource {
    
    
    @IBOutlet weak var dungeon_table: UITableView!
    @IBOutlet weak var closeButton: UIButton!
    
    var dungeonList: [Dungeon] = []
    
    var selectDungeonName: String! = ""
    var selectDungeonId: Int!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        
        //タイトルを取得して再設定する。
//        self.title = self.title! + ""
        
        let listUrl = "http://52.199.28.109/v1/dungeons.php"
        Alamofire.request(listUrl).responseJSON{ response in
            
            let json = JSON(response.result.value ?? 0)
            var jsonarray = json.arrayValue
            
            for i in (0..<jsonarray.count){
                let row = Dungeon()
                row.getlist(data:jsonarray[i])
                self.dungeonList.append(row)
            }
            self.dungeon_table.reloadData()
        }
        
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?)  {
        
        let next = segue.destination as! PostViewController
        next.dungeonName = selectDungeonName
        next.dungeonId = selectDungeonId
        
    }
    
    
    
    func tableView(_ table: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        return dungeonList.count
        
    }
    
    
    
    //各セルの要素を設定する
    func tableView(_ table: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = UITableViewCell(style: UITableViewCellStyle .default, reuseIdentifier: "Cell")
        let row = self.dungeonList[indexPath.row]
        cell.textLabel?.text = row.dungeon_name
        return cell
        
    }
    
    //tap
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        //セルをタップした時セグウェイで遷移
        tableView.deselectRow(at: indexPath, animated: true)
        let row = self.dungeonList[indexPath.row]
        self.selectDungeonName = row.dungeon_name
        self.selectDungeonId = row.dungeon_id
        performSegue(withIdentifier: "toPost",sender: nil)
        
    }
    
    
    @IBAction func pushCloseButton(_ sender: UIBarButtonItem) {
        
        //閉じるボタンで前の画面(ListView)に戻る
        self.view.endEditing(true)
        self.dismiss(animated: true, completion: nil)
        
    }
    
    //セグウェイで投稿フォームに移動、選択したダンジョンのデータも送る。OK
    
    //タイトルに"ダンジョン選択"などを入れる。OK
    //コメントはテキストフィールドにする。OK
    
    //ダンジョンはjsonでサーバーから取ってくる。OK
    
    //ダンジョンのデータのクラス名OK
    //文字数制限コメントOK
    //ヘッダー消すOK
    
}
