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
    
    var dungeonList: [DungeonData] = []
    
    var selectDungeon: String! = ""
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        //タイトルを取得して再設定する。
        self.title = self.title! + ""
        
        let listUrl = "http://52.199.28.109/pazd_dungeon_api.php"
        Alamofire.request(listUrl).responseJSON{ response in
            
            let json = JSON(response.result.value ?? 0)
            var jsonarray = json.arrayValue
            
            for i in (0..<jsonarray.count){
                let row = DungeonData()
                row.getlist(data:jsonarray[i])
                self.dungeonList.insert(row,at:0)
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
        next.parameter = selectDungeon
        
    }
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }
    
    func tableView(_ table: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        return dungeonList.count
        
    }
    
    func tableView(_ tableView: UITableView, heightForHeaderInSection section: Int) -> CGFloat {
        return 20
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
        
        let row = self.dungeonList[indexPath.row]
        self.selectDungeon = row.dungeon_name
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
    
    //ダンジョンはjsonでサーバーから取ってくる。
    
    //ダンジョンのデータのクラス名
    //文字数制限コメント
    
}
