//
//  TextEngine.swift
//  MacOS
//
//  Created by KonH on 4/29/18.
//  Copyright © 2018 KonH. All rights reserved.
//

import Foundation

class TextEngine {
    static let sharedInstance:TextEngine = TextEngine()
    
    static let writeNotification = Notification.Name(rawValue:"TextEngine.WriteNotification")
    
    private init() {}
    
    func write(msg:String) {
        NSLog("TextEngine.write: '%@'", msg)
        let nc = NotificationCenter.default
        nc.post(name:TextEngine.writeNotification,
                object: nil,
                userInfo:["message":msg])
    }
        
    func onStart() {
        NSLog("TextEngine.onStart")
    }
        
    func onRead(msg:String) {
        NSLog("TextEngine.onRead: '%@'", msg)
        write(msg: "You entered: '" + msg + "'\n")
    }
}
