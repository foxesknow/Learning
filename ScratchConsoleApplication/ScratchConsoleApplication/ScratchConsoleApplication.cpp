// ScratchConsoleApplication.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "StackTrace.h"
#include "TokenExpander.h"
#include "Settings.h"

#include <iostream>

int main()
{    
    EnvironmentSettings settings;

    std::wstring value;
    auto found = settings.GetSetting(L"USERDOMAIN", value);

    return 0;
}

