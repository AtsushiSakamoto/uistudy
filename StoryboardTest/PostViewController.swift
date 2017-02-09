//
//  PostViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/03.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit

class PostViewController: UIViewController , UITableViewDelegate, UITableViewDataSource, UITextFieldDelegate {
    
    
    @IBOutlet weak var closeButton: UIBarButtonItem!
    @IBOutlet weak var postTable: UITableView!
    
    //    @IBOutlet weak var postCell: UITableViewCell!
    
    
    var roomidTextField: UITextField!
    var readerTextField: UITextField!
    var commentTextField: UITextField!
    var continyuityTextField: UITextField!
    var dungeonidTextField: UITextField!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        print("postview")
    
        //Viewにタップジェスチャーリコグナイザーを配置
        let tapGesture:UITapGestureRecognizer = UITapGestureRecognizer(target: self, action: #selector(self.tapscreen(_:)))
        self.view.addGestureRecognizer(tapGesture)
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
        
        //仮のセルを作る
        let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
        //ビューの幅を取得
        let width = self.view.bounds.width / 2
        //セルの高さを取得
        let height = cell.bounds.height
        
        if(indexPath.section == 0){
            switch indexPath.row {
            case 0 :
                
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                
                print(cell.bounds)
                print(cell.frame)
                print(cell.center)
                roomidTextField = UITextField(frame: CGRect(x: width,y: 6,width: width - 10,height: height - 12))
                cell.textLabel?.text = "ルームID"
                roomidTextField.placeholder = "例：1024"
                roomidTextField.delegate = self;
                roomidTextField.borderStyle = UITextBorderStyle.roundedRect
                cell.contentView.addSubview(roomidTextField)
                return cell
                
            case 1 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "リーダー"
                
                readerTextField = UITextField(frame: CGRect(x: width,y: 6,width: width - 10,height: height - 12))
                readerTextField.placeholder = "例：クリシュナ"
                readerTextField.delegate = self;
                readerTextField.borderStyle = UITextBorderStyle.roundedRect
                cell.contentView.addSubview(readerTextField)
                return cell
                
            case 2 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "コメント"
                commentTextField = UITextField(frame: CGRect(x: width ,y: 6,width: width - 10,height: height - 12))
                commentTextField.placeholder = "例：よろしく！"
                commentTextField.delegate = self;
                commentTextField.borderStyle = UITextBorderStyle.roundedRect
                cell.contentView.addSubview(commentTextField)
                return cell
                
            case 3 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "コンテニュー"
                continyuityTextField = UITextField(frame: CGRect(x: width ,y: 6,width: width - 10,height: height - 12))
                continyuityTextField.placeholder = "例：する...1,しない...0"
                continyuityTextField.delegate = self;
                continyuityTextField.borderStyle = UITextBorderStyle.roundedRect
                cell.contentView.addSubview(continyuityTextField)
                return cell
                
            default :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "ダンジョンID"
                dungeonidTextField = UITextField(frame: CGRect(x: width ,y: 6,width: width - 10,height: height - 12))
                dungeonidTextField.placeholder = "例：2024"
                dungeonidTextField.delegate = self;
                dungeonidTextField.borderStyle = UITextBorderStyle.roundedRect
                cell.contentView.addSubview(dungeonidTextField)
                return cell
                
            }
            
            /*
             switch indexPath.row {
             
             case 0:
             let postRoomIdCell = table.dequeueReusableCell(withIdentifier: "postRoomIdCell", for: indexPath) as! PostRoomIdCell
             postRoomIdCell.roomIdLabel.text = "ルームID"
             postRoomIdCell.roomIdTextField.placeholder = "例：1024"
             return postRoomIdCell
             case 1:
             let postReaderCell = table.dequeueReusableCell(withIdentifier: "postReaderCell", for: indexPath)as! PostReaderCell
             postReaderCell.readerLabel.text = "リーダー"
             postReaderCell.readerTexeField.placeholder = "例：クリシュナ"
             return postReaderCell
             case 2:
             let postCommentCell = table.dequeueReusableCell(withIdentifier: "postCommentCell", for: indexPath)as! PostCommentCell
             postCommentCell.commentLabel.text = "コメント"
             postCommentCell.commentTextField.placeholder = "例：よろしく！"
             return postCommentCell
             case 3:
             let postContinyuity = table.dequeueReusableCell(withIdentifier: "postContinyuityCell", for: indexPath)as! PostContinyuityCell
             postContinyuity.continyuityLabel.text = "コンテニュー"
             postContinyuity.continyuityTextField.placeholder = "例：する...1,しない...2"
             return postContinyuity
             default:
             let postDungeonIdCell = table.dequeueReusableCell(withIdentifier: "postDungeonIdCell", for: indexPath)as! PostDungeonIdCell
             postDungeonIdCell.dungeonIdLabel.text = "ダンジョンID"
             postDungeonIdCell.dungeonIdTxetField.placeholder = "例：2024"
             return postDungeonIdCell
             }
             */
            
        }else{
            
            
            let postViewButtonCell = table.dequeueReusableCell(withIdentifier: "postButtonCell", for: indexPath) as! PostViewButtonCell
            postViewButtonCell.postButton.setTitle("投稿", for: UIControlState.normal)
            postViewButtonCell.postButton .addTarget(self, action: #selector(PostViewController.pushPostButton(_:)), for: UIControlEvents.touchUpInside)
            
            return postViewButtonCell
        }
    }
    
    //tap
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        //        print(indexPath.row)
        tableView.deselectRow(at: indexPath, animated: true)
    }
    
    
    @IBAction func pushPostButton(_ sender: UIBarButtonItem) {
        
        let roomid = roomidTextField.text
        
        if(roomid?.isEmpty)!{
            print("ルームIDを入力して下さい。")
        }else{
            print("投稿されたルームIDは"+roomid!+"です。")
        }
        
        print(readerTextField.text!+commentTextField.text!+continyuityTextField.text!+dungeonidTextField.text!)
    }
    
    @IBAction func pushCloseButton(_ sender: UIBarButtonItem) {
        self.dismiss(animated: true, completion: nil)
    }
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool{
        // 改行でキーボードを閉じる
        textField.resignFirstResponder()
        return true
    }
    
    func tapscreen(_ sender: UITapGestureRecognizer){
        //タップでキーボードを閉じる
        self.view.endEditing(true)
    }

    
    
   //閉じるボタンで自分でキーボード落とす
    //タップジェスチャーをビューが変わる前に落とす
    //
}



