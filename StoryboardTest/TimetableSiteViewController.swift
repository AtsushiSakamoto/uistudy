//
//  TimetableSiteViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/03/13.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit

class TimetableSiteViewController: UIViewController ,UIWebViewDelegate {
    
    var targetURL = "http://pad.zap.jp.net"
    
    @IBOutlet weak var timetableSiteView: UIWebView!
    
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        //タイトルを取得して再設定する。
        self.title = self.title! + ""
        loadAddressURL()
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func loadAddressURL() {
        let requestURL = NSURL(string: targetURL)
        let req = NSURLRequest(url: requestURL as! URL)
        timetableSiteView.loadRequest(req as URLRequest)
    }
}