//
//  AppDelegate.swift
//  MacOS
//
//  Created by KonH on 4/29/18.
//  Copyright © 2018 KonH. All rights reserved.
//

import Cocoa

@NSApplicationMain
class AppDelegate: NSObject, NSApplicationDelegate {



    func applicationDidFinishLaunching(_ aNotification: Notification) {
        // Insert code here to initialize your application
        TextEngine.sharedInstance.onStart()
    }

    func applicationWillTerminate(_ aNotification: Notification) {
        // Insert code here to tear down your application
    }


}

