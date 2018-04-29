//
//  ViewController.swift
//  iOS
//
//  Created by KonH on 4/29/18.
//  Copyright © 2018 KonH. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    @IBOutlet weak var textView: UILabel!
    @IBOutlet weak var textInput: UITextField!
    @IBOutlet weak var submitButton: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        let nc = NotificationCenter.default
        nc.addObserver(forName:TextEngine.writeNotification, object:nil, queue:nil, using:onWriteNotification)
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBAction func onSubmitButtonClick() {
        let text = textInput.text
        if text != nil {
            TextEngine.sharedInstance.onRead(msg: text!)
        }
        textInput.text = ""
    }
    
    func onWriteNotification(notification:Notification) {
        guard
            let userInfo = notification.userInfo,
            let message = userInfo["message"] as? String else {
                return
        }
        textView.text! += message
        textView.sizeToFit()
    }
}

