//
//  PostViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/02/03.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit
import Alamofire
import SwiftyJSON
import GoogleMobileAds

class PostViewController: UIViewController , UITableViewDelegate, UITableViewDataSource, UITextFieldDelegate, UITextViewDelegate, GADBannerViewDelegate{
    
    
    @IBOutlet weak var bannerView: GADBannerView!
    @IBOutlet weak var postTable: UITableView!
    var dungeonName: String = ""
    var dungeonId: Int = 0
    
    var tapGesture: UITapGestureRecognizer! = nil
    var roomidTextField: UITextField!
    var readerTextField: UITextField!
    var commentTextView: UITextView!
    var continyuitySegment: UISegmentedControl! = UISegmentedControl()
    var continyuityValue: Int! = 1
    var dungeonLabel: UILabel = UILabel()
    let maxLength = 200
    var previousText = ""
    var lastReplaceRange: NSRange!
    var lastReplacementString = ""
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        print("postview viewDidLoad")
        //タイトルを取得して再設定する。
        self.title = self.title! + ""

        bannerView.delegate = self
        bannerView.adUnitID = "ca-app-pub-3607945421999798/4255696064"
        bannerView.rootViewController = self
        
        let request = GADRequest()
        request.testDevices = ["c1e9edc86b2dda4d4142510cdaee48b1"]
        
        bannerView.load(request)
    }
    
    override func viewDidAppear(_ animated: Bool){
        //Viewにタップジェスチャーリコグナイザーを配置
        tapGesture = UITapGestureRecognizer(target: self, action: #selector(self.tapscreen(_:)))
        self.view.addGestureRecognizer(tapGesture)
        print("postview viewDidApper")
        
        let pasteboard = UIPasteboard.general
        if let string = pasteboard.string{
            let id = string.components(separatedBy: CharacterSet.whitespaces).joined()
            if id.characters.count <= 8{
                self.roomidTextField.text = id
            }
        }
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        print("postview viewDisappear")
        //タップジェスチャーを消す。
        if(tapGesture != nil){
            self.view.removeGestureRecognizer(tapGesture)
        }
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
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat{
        //テーブルの高さを取得
        if(indexPath.row == 3){
            return 88.0
        }else{
            return 44.0
        }
    }
    
    //各セルの要素を設定する
    func tableView(_ table: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        //ビューの幅を取得
        let width = self.postTable.bounds.width / 2
        
        if(indexPath.section == 0){
            switch indexPath.row {
                
            case 0 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "ダンジョン"
                
                dungeonLabel = UILabel(frame: CGRect(x: width ,y: 6,width: width - 10,height: 32))
                dungeonLabel.text = self.dungeonName
                cell.contentView.addSubview(dungeonLabel)
                
                return cell
                
            case 1 :
                
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                roomidTextField = UITextField(frame: CGRect(x: width,y: 6,width: width - 10,height: 32))
                cell.textLabel?.text = "ルームID"
                //テキストフィールドの設定とセルへの追加
                roomidTextField.placeholder = "例：10245048"
                roomidTextField.keyboardType = UIKeyboardType.numberPad
                roomidTextField.delegate = self;
                roomidTextField.borderStyle = UITextBorderStyle.roundedRect
                
                cell.contentView.addSubview(roomidTextField)
                return cell
                
            case 2 :
                let cell = UITableViewCell(style: UITableViewCellStyle.value1, reuseIdentifier: "Cell")
                cell.textLabel?.text = "リーダー"
                
                readerTextField = UITextField(frame: CGRect(x: width,y: 6,width: width - 10,height: 32))
                readerTextField.placeholder = "省略可"
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
        
        //転送データの生成
        let postData:Parameters? = [
            
            "room_id" : self.roomidTextField.text!,
            "my_reader" : self.readerTextField.text!,
            "comment" : self.commentTextView.text!,
            "continuity" : self.continyuityValue,
            "dungeon_id" : self.dungeonId
        ]
        
        print(postData!)
        
        //PHPにAlamofireを用いてPOSTを投げ、レスポンスを貰う
        Alamofire.request("http://52.199.28.109/v1/entry.php", method: .post, parameters: postData!, encoding: JSONEncoding.default).responseJSON { response in
            
            //受け取ったAny?クラスのデータをJson?→Dictionaty?と変える
            let json = JSON(response.result.value ?? 0)
            let jsondictionary = json.dictionaryValue
            let re: Int? = (jsondictionary["post"]?.intValue)
            
            if (re != nil) {
                
                switch (re!) {
                    
                case 0  :
                    let alert: UIAlertController = UIAlertController(title: "投稿しました", message: "パズドラを起動しますか？", preferredStyle: .alert)
                    let okAction = UIAlertAction(title: "YES", style: .default) { action in
                        
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
                    
                case 92 :
                    let alert: UIAlertController = UIAlertController(title: "投稿出来ませんでした", message: "ユーザーIDが正しくありません", preferredStyle: .alert)
                    let okAction = UIAlertAction(title: "OK", style: .default)
                    alert.addAction(okAction)
                    self.present(alert, animated: true, completion: nil)
                    
                case 93 :
                    let alert: UIAlertController = UIAlertController(title: "投稿出来ませんでした", message: "ルームIDが正しくありません", preferredStyle: .alert)
                    let okAction = UIAlertAction(title: "OK", style: .default)
                    alert.addAction(okAction)
                    self.present(alert, animated: true, completion: nil)
/*
                case 94 :
                    let alert: UIAlertController = UIAlertController(title: "投稿出来ませんでした", message: "リーダーが正しくありません", preferredStyle: .alert)
                    let okAction = UIAlertAction(title: "OK", style: .default)
                    alert.addAction(okAction)
                    self.present(alert, animated: true, completion: nil)

                case 95 :
                    let alert: UIAlertController = UIAlertController(title: "投稿出来ませんでした", message: "コメントが正しくありません", preferredStyle: .alert)
                    let okAction = UIAlertAction(title: "OK", style: .default)
                    alert.addAction(okAction)
                    self.present(alert, animated: true, completion: nil)
 
 */
                case 96 :
                    let alert: UIAlertController = UIAlertController(title: "投稿出来ませんでした", message: "コンテニューの有無を選択して下さい", preferredStyle: .alert)
                    let okAction = UIAlertAction(title: "OK", style: .default)
                    alert.addAction(okAction)
                    self.present(alert, animated: true, completion: nil)
                    
                case 97 :
                    let alert: UIAlertController = UIAlertController(title: "投稿出来ませんでした", message: "ダンジョンを選択して下さい", preferredStyle: .alert)
                    let okAction = UIAlertAction(title: "OK", style: .default)
                    alert.addAction(okAction)
                    self.present(alert, animated: true, completion: nil)
                    
                case 98 :
                    let alert: UIAlertController = UIAlertController(title: "投稿出来ませんでした", message: "データベース接続失敗しました", preferredStyle: .alert)
                    let okAction = UIAlertAction(title: "OK", style: .default)
                    alert.addAction(okAction)
                    self.present(alert, animated: true, completion: nil)
                    
                case 99 :
                    let alert: UIAlertController = UIAlertController(title: "投稿出来ませんでした", message: "クエリー失敗しました", preferredStyle: .alert)
                    let okAction = UIAlertAction(title: "OK", style: .default)
                    alert.addAction(okAction)
                    self.present(alert, animated: true, completion: nil)
                    
                default :
                    let alert: UIAlertController = UIAlertController(title: "投稿出来ませんでした", message: "予期せぬエラーにより投稿できませんでした", preferredStyle: .alert)
                    let okAction = UIAlertAction(title: "OK", style: .default)
                    alert.addAction(okAction)
                    self.present(alert, animated: true, completion: nil)
                    
                }
            }else{
                print(response.error!)
                let alert: UIAlertController = UIAlertController(title: "投稿出来ませんでした", message: "ネットワーク通信が不安定です", preferredStyle: .alert)
                let okAction = UIAlertAction(title: "OK", style: .default)
                alert.addAction(okAction)
                self.present(alert, animated: true, completion: nil)
            }
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
        if (segcon.selectedSegmentIndex == 0) {
            self.continyuityValue = 1
        }else if (segcon.selectedSegmentIndex == 1){
            self.continyuityValue = 0
        }else{
            print("error")
        }
        
    }
    
    
    func textField(_ textField: UITextField, shouldChangeCharactersIn range: NSRange, replacementString text: String) -> Bool {
        //入力文字数の制限
        if(textField == roomidTextField){
            
            // 入力済みの文字と入力された文字を合わせて取得.
            let str = textField.text! + text
            if str.characters.count <= 8 {
                return true
            }
        }
        
        if (textField == readerTextField){
            let str = textField.text! + text
            if str.characters.count <= 30 {
                return true
            }
        }
        
        return false
    }
    
    func textView(_ textView: UITextView, shouldChangeTextIn range: NSRange, replacementText text: String) -> Bool {
        
        //確定している文字列、終端点と追加する文字列を取得する
        self.previousText = textView.text
        self.lastReplaceRange = range
        self.lastReplacementString = text
        
        return true
    }
    
    
    func textViewDidChange(_ textView: UITextView) {
        //変換中は入力を続ける
        if textView.markedTextRange != nil {
            return
        }
        //最大文字数より変換終了後の文字数が多い時
        if (textView.text?.characters.count)! > maxLength {
            
            let offset = maxLength - (textView.text?.characters.count)!
            
            //変換し追加する文字列を余りの文字数まで削る
            let replacementString = (lastReplacementString as NSString).substring(to: lastReplacementString.characters.count + offset)
            
            //確定している文字列の終端から文字数調整した文字列を追加
            let text = (previousText as NSString).replacingCharacters(in: lastReplaceRange, with: replacementString)
            textView.text = text
            
            //カーソル位置を元の場所から削った分だけずらす
            let position = textView.position(from: textView.selectedTextRange!.start, offset: offset)
            let selectedTextRange = textView.textRange(from: position!, to: position!)
            textView.selectedTextRange = selectedTextRange
            
        }
    }
    
    
    
    
    //閉じるボタンで自分でキーボード落とすOK
    //タップジェスチャーをビューが変わる前に落とすOK
    //キーボードをテキストフィールドに合わせて数字に変える。OK
    //コンテニューをスウィッチボタンで実装OK
    
    
    //セグメントでコンテニューの有無OK
    //ダンジョン選択させるようにするok
    
    //受け取ったデータをラベルに代入OK
    //画面をバックした時にキーボードとタップジェスチャーを閉じる。OK
    //コメントとルームIDの文字数制限OK
    
    
    //リーダーの文字数制限OK
    //jsonpost でサーバーに送る投稿OK
    
    //投稿後アラート　成功しました。　パズドラ起動どうか　はいorいいえ
    //失敗　の時は　ケースをアラート　ボタン一個OK
    
    
    //リーダーok
    //クリップボードの文字列をルームIDに代入ok
}


