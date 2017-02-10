//
//  PostViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/03.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit

class PostViewController: UIViewController , UITableViewDelegate, UITableViewDataSource, UITextFieldDelegate{
    
    
    
    @IBOutlet weak var closeButton: UIBarButtonItem!
    @IBOutlet weak var postTable: UITableView!
    
    
    var tapGesture: UITapGestureRecognizer! = nil
    var roomidTextField: UITextField!
    var readerTextField: UITextField!
    var commentTextField: UITextField!
    var continyuityTextField: UITextField!
    var dungeonidTextField: UITextField!
    let mySwicth: UISwitch = UISwitch()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        print("postview viewDidLoad")
        
    }
    
    override func viewDidAppear(_ animated: Bool){
        //Viewにタップジェスチャーリコグナイザーを配置
        tapGesture = UITapGestureRecognizer(target: self, action: #selector(self.tapscreen(_:)))
        self.view.addGestureRecognizer(tapGesture)
        print(tapGesture)
        print("viewDidApper")
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        print("viewWillDisappear")
        //タップジェスチャーを消す。
        if(tapGesture != nil){
            self.view.removeGestureRecognizer(tapGesture)
            print("close tapgesture")
        }
        print(tapGesture)
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
        
        //ビューの幅を取得
        let width = self.view.bounds.width / 2
        
        if(indexPath.section == 0){
            switch indexPath.row {
                
            case 0 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                print(cell.bounds)
                roomidTextField = UITextField(frame: CGRect(x: width,y: 6,width: width - 10,height: 32))
                cell.textLabel?.text = "ルームID"
                //テキストフィールドの設定とセルへの追加
                roomidTextField.placeholder = "例：1024"
                roomidTextField.keyboardType = UIKeyboardType.numberPad
                roomidTextField.delegate = self;
                roomidTextField.borderStyle = UITextBorderStyle.roundedRect
                
                cell.contentView.addSubview(roomidTextField)
                return cell
                
            case 1 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "リーダー"
                
                readerTextField = UITextField(frame: CGRect(x: width,y: 6,width: width - 10,height: 32))
                readerTextField.placeholder = "例：クリシュナ"
                readerTextField.delegate = self;
                readerTextField.borderStyle = UITextBorderStyle.roundedRect
                
                cell.contentView.addSubview(readerTextField)
                return cell
                
            case 2 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "コメント"
                
                commentTextField = UITextField(frame: CGRect(x: width ,y: 6,width: width - 10,height: 32))
                commentTextField.placeholder = "例：よろしく！"
                commentTextField.delegate = self;
                commentTextField.borderStyle = UITextBorderStyle.roundedRect
                
                cell.contentView.addSubview(commentTextField)
                return cell
                
            case 3 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "ダンジョンID"
                
                dungeonidTextField = UITextField(frame: CGRect(x: width ,y: 6,width: width - 10,height: 32))
                dungeonidTextField.placeholder = "例：2024"
                dungeonidTextField.keyboardType = UIKeyboardType.numberPad
                dungeonidTextField.delegate = self;
                dungeonidTextField.borderStyle = UITextBorderStyle.roundedRect
                
                cell.contentView.addSubview(dungeonidTextField)
                return cell
                
            default :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "コンテニュー"
                //スウィッチの設定とセルへの追加
                mySwicth.layer.position = CGPoint(x: width*2 - 38, y: 22)
                mySwicth.tintColor = UIColor.black
                mySwicth.isOn = true
                mySwicth.addTarget(self, action: #selector(self.onClickMySwicth(sender:)), for: UIControlEvents.valueChanged)
                
                cell.contentView.addSubview(mySwicth)
                
                return cell
                
                
                /*
                 
                 let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                 cell.textLabel?.text = "コンテニュー"
                 continyuityTextField = UITextField(frame: CGRect(x: width ,y: 6,width: width - 10,height: height - 12))
                 continyuityTextField.placeholder = "する:1,しない:0"
                 continyuityTextField.keyboardType = UIKeyboardType.numberPad
                 continyuityTextField.delegate = self;
                 continyuityTextField.borderStyle = UITextBorderStyle.roundedRect
                 cell.contentView.addSubview(continyuityTextField)
                 return cell
                 
                 */
                
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
        
        print(readerTextField.text!+commentTextField.text!+dungeonidTextField.text!)
        
        //スウィッチがtrueで有,falseで無
        if(mySwicth.isOn){
            print("コンテニュー有")
        }else{
            print("コンテニュー無")
        }
        
    }
    
    @IBAction func pushCloseButton(_ sender: UIBarButtonItem) {
        //閉じるボタンでキーボードを閉じ、前の画面(ListView)に戻る
        print("fall keyboard")
        self.view.endEditing(true)
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
    
    internal func onClickMySwicth(sender: UISwitch){
        //Switchの状態を表示
        print(sender.isOn)
        
    }
    
    
    
    //閉じるボタンで自分でキーボード落とすOK
    //タップジェスチャーをビューが変わる前に落とすOK
    //キーボードをテキストフィールドに合わせて数字に変える。OK
    //コンテニューをスウィッチボタンで実装OK
}



