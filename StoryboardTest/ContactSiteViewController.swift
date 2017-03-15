//
//  ContactSiteViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/03/13.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit

class ContactSiteViewController: UIViewController ,UIWebViewDelegate {
    
    var targetURL = "http://padmulti.com/padMultis/supportForm/"
    
    
    
    @IBOutlet weak var contactSiteView: UIWebView!
    
    
    
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
        contactSiteView.loadRequest(req as URLRequest)
    }
}
