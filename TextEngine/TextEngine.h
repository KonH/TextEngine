extern "C" {
	
	// Called from frontend code to initialize all required callbacks
	void TextEngine_Init();
	
	// Called from frontend code when it is ready
	void TextEngine_OnStart();
	
	// Called from frontend code on user input
	void TextEngine_OnRead(const char* msg);
}

// Called by user library, forwarded to frontend
void TextEngine_Write();

