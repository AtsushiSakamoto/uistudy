//
//  PostViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/03.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit

class PostViewController: UIViewController , UITableViewDelegate, UITableViewDataSource, UITextFieldDelegate, UITextViewDelegate/*,UIPickerViewDelegate,UIPickerViewDataSource*/{
    
    
    @IBOutlet weak var postTable: UITableView!
    var parameter: String = ""
    
    var tapGesture: UITapGestureRecognizer! = nil
    var roomidTextField: UITextField!
    var readerTextField: UITextField!
    var commentTextView: UITextView!
//    var continyuityTextField: UITextField!
    var continyuitySegment: UISegmentedControl = UISegmentedControl()
    var dungeonLabel: UILabel = UILabel()
//    var dungeonSelectButton: UIButton = UIButton()
//    var pickerDungeon: String! = nil
//    var dungeonPicker: UIPickerView! = UIPickerView()

    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        print("postview viewDidLoad")
        //タイトルを取得して再設定する。
        self.title = self.title! + ""
        
     
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
        self.view.endEditing(true)
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
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat
    {
        if(indexPath.row == 3){
            return 88.0
        }else{
            return 44.0
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
                cell.textLabel?.text = "ダンジョン"
                
                dungeonLabel = UILabel(frame: CGRect(x: width ,y: 6,width: width - 10,height: 32))
                dungeonLabel.text = self.parameter
                cell.contentView.addSubview(dungeonLabel)
                
                return cell
                
            case 1 :
                
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                roomidTextField = UITextField(frame: CGRect(x: width,y: 6,width: width - 10,height: 32))
                cell.textLabel?.text = "ルームID"
                //テキストフィールドの設定とセルへの追加
                roomidTextField.placeholder = "例：1024"
                roomidTextField.keyboardType = UIKeyboardType.numberPad
                roomidTextField.delegate = self;
                roomidTextField.borderStyle = UITextBorderStyle.roundedRect
                
                cell.contentView.addSubview(roomidTextField)
                return cell
                
            case 2 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "リーダー"
                
                readerTextField = UITextField(frame: CGRect(x: width,y: 6,width: width - 10,height: 32))
                readerTextField.placeholder = "例：クリシュナ"
                readerTextField.delegate = self;
                readerTextField.borderStyle = UITextBorderStyle.roundedRect
                
                cell.contentView.addSubview(readerTextField)
                return cell
                
            case 3 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "コメント"
                
                commentTextView = UITextView(frame: CGRect(x: width ,y: 6,width: width - 10,height: 76))
                commentTextView.delegate = self
                commentTextView.layer.masksToBounds = true
                commentTextView.layer.cornerRadius = 5.0
                commentTextView.layer.borderColor = UIColor.lightGray.cgColor
                commentTextView.textAlignment = NSTextAlignment.left
                commentTextView.font = UIFont.systemFont(ofSize: 16)
                
                commentTextView.layer.borderWidth = 1
                
                cell.contentView.addSubview(commentTextView)
                
                return cell
                
            default :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "コンテニュー"
                
                let segmentArray: NSArray = ["する","しない"]
                continyuitySegment = UISegmentedControl(items: segmentArray as [AnyObject])
                continyuitySegment.layer.position = CGPoint(x: width + 54.5, y: 22)
                // continyuitySegment.layer.position = CGPoint(x: width*2 - 64.5, y: 22)
                continyuitySegment.backgroundColor = UIColor.white
                continyuitySegment.tintColor = UIColor.init(red: 0.0, green: 0.0, blue: 0.0, alpha: 0.4)
                continyuitySegment.selectedSegmentIndex = 0
                continyuitySegment.addTarget(self, action: #selector(PostViewController.segconChanged(segcon:)), for: UIControlEvents.valueChanged)
                
                cell.contentView.addSubview(continyuitySegment)
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
        //投稿ボタンが押された時の処理
        let roomid = roomidTextField.text
        
        if(roomid?.isEmpty)!{
            print("ルームIDを入力して下さい。")
        }else{
            print("投稿されたルームIDは"+roomid!+"です。")
        }
        
        print(readerTextField.text!)
        
        //スウィッチがtrueで有,falseで無
        if(continyuitySegment.selectedSegmentIndex == 0){
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
    
    internal func segconChanged(segcon: UISegmentedControl){
        //コンテニューセグメントの選択が切り替わった時の処理
        switch segcon.selectedSegmentIndex {
        case 0:
            print("コンテニュー有")
        case 1:
            print("コンテニュー無")
        default:
            print("Error")
        }
        
    }
/*
    internal func pushSelectButton(sender: UIButton){
    
    
        let title = "ダンジョンを選択して下さい。"
        let message = "\n\n\n\n\n\n\n\n" //改行入れないとOKCancelがかぶる
        let alert = UIAlertController(title: title, message: message, preferredStyle: .alert)
        let okAction = UIAlertAction(title: "OK", style: UIAlertActionStyle.default, handler:{
            (action: UIAlertAction!) -> Void in
            
            self.dungeonLabel.text = self.pickerDungeon as String
            print(self.pickerDungeon)
            
            
        })
        let cancelAction = UIAlertAction(title: "Cancel", style: .cancel) { action in
        }
        
        // PickerView
        dungeonPicker.selectRow(0, inComponent: 0, animated: true) // 初期値
        dungeonPicker.frame = CGRect(x:0, y:54, width:270,height: 180)
        dungeonPicker.dataSource = self
        dungeonPicker.delegate = self
        alert.view.addSubview(dungeonPicker)
        
        alert.addAction(okAction)
        alert.addAction(cancelAction)
        present(alert, animated: true, completion: nil)
    
    }
 */
/*
    func numberOfComponents(in pickerView: UIPickerView) -> Int {
        return 1
    }
    
    func pickerView(_ pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        return dungeonValue.count
    }

    func pickerView(_ pickerView: UIPickerView, titleForRow row: Int, forComponent component: Int) -> String? {
        return dungeonValue[row] as? String
    }
    
    func pickerView(_ pickerView: UIPickerView, didSelectRow row: Int, inComponent component: Int) {
        
        pickerDungeon = self.dungeonValue[row] as! String
    }
 */
    
    
    
    //閉じるボタンで自分でキーボード落とすOK
    //タップジェスチャーをビューが変わる前に落とすOK
    //キーボードをテキストフィールドに合わせて数字に変える。OK
    //コンテニューをスウィッチボタンで実装OK
    
    
    //セグメントでコンテニューの有無OK
    //ダンジョン選択させるようにするok
    
    //受け取ったデータをラベルに代入OK
    //画面をバックした時にキーボードとタップジェスチャーを閉じる。OK
}



