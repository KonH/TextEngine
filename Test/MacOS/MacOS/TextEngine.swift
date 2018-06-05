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
    
    private init() {
		TextEngine_Init({ (arg) in TextEngine.testWrite(arg: arg) }, { (arg) in TextEngine.testWrite(arg: arg) })
	}
	
	static func testWrite(arg:Optional<UnsafePointer<Int8>>) {
		// todo
	}
	
    func write(msg:String) {
        NSLog("TextEngine.write: '%@'", msg)
        let nc = NotificationCenter.default
        nc.post(name:TextEngine.writeNotification,
                object: nil,
                userInfo:["message":msg])
    }
	
	func debug(msg:String) {
		NSLog("TextEngine.debug: '%@'", msg)
	}
        
    func onStart() {
        NSLog("TextEngine.onStart")
		TextEngine_OnStart()
    }
        
    func onRead(msg:String) {
        NSLog("TextEngine.onRead: '%@'", msg)
        //write(msg: "You entered: '" + msg + "'\n")
		TextEngine_OnRead(msg)
    }
}
