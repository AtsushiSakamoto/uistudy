//
//  SupportPageViewController.swift
//  StoryboardTest
//
//  Created by 坂本篤志 on 2017/03/13.
//  Copyright © 2017年 com.imple. All rights reserved.
//

import UIKit

class SupportPageViewController: UIViewController ,UIWebViewDelegate {
    
    var targetURL = ""
    var pageTitle = ""
    

    @IBOutlet weak var webView: UIWebView!
    @IBOutlet weak var supportPageView: UIWebView!
    
    @IBOutlet weak var returnButton: UIBarButtonItem!
    
    @IBOutlet weak var reloadButton: UIBarButtonItem!
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.title = pageTitle
        loadAddressURL()
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func loadAddressURL() {
        let requestURL = NSURL(string: targetURL)
        let req = NSURLRequest(url: requestURL as! URL)
        supportPageView.loadRequest(req as URLRequest)
    }
   
    @IBAction func touchReloadButton(_ sender: UIBarButtonItem) {
        self.webView.reload()
    }
    @IBAction func touchReturnButton(_ sender: UIBarButtonItem) {
        self.webView.goBack()
    }
}
