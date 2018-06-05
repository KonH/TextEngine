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
	
	let wrapper = TextEngineWrapper()
	
    func performInit() {
		wrapper.performInit(
			writeCallback:    { (arg) in TextEngine.write(arg: arg) },
			andDebugCallback: { (arg) in TextEngine.debug(arg: arg) }
		)
	}
	
	static func write(arg:Optional<UnsafePointer<Int8>>) {
		if let arg = arg {
			let str = String(cString: arg)
			sharedInstance.write(msg: str)
		} else {
			NSLog("Invalid write argument")
		}
	}
	
	static func debug(arg:Optional<UnsafePointer<Int8>>) {
		if let arg = arg {
			let str = String(cString: arg)
			sharedInstance.debug(msg: str)
		} else {
			NSLog("Invalid debug argument")
		}
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
		wrapper.onStart()
    }
        
    func onRead(msg:String) {
        NSLog("TextEngine.onRead: '%@'", msg)
		wrapper.onRead(msg)
    }
}
