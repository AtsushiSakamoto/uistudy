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
    
    

    @IBOutlet weak var supportPageView: UIWebView!
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
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
}
