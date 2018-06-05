//
//  ViewController.swift
//  MacOS
//
//  Created by KonH on 4/29/18.
//  Copyright © 2018 KonH. All rights reserved.
//

import Cocoa

class ViewController: NSViewController {
    @IBOutlet weak var textView: NSTextField!
    @IBOutlet weak var textInput: NSTextField!
    @IBOutlet weak var submitButton: NSButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        let nc = NotificationCenter.default
        nc.addObserver(forName:TextEngine.writeNotification, object:nil, queue:nil, using:onWriteNotification)
    }

    override var representedObject: Any? {
        didSet {
        // Update the view, if already loaded.
        }
    }
    
    @IBAction func onSubmitButtonClick(_ sender: NSButton) {
        let text = textInput.stringValue
        TextEngine.sharedInstance.onRead(msg: text)
        textInput.stringValue = ""
    }
    
    func onWriteNotification(notification:Notification) {
        guard
            let userInfo = notification.userInfo,
            let message = userInfo["message"] as? String else {
                return
        }
        textView.stringValue += message
    }
}

