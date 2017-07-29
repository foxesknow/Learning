// ScratchConsoleApplication.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "StackTrace.h"

#include <iostream>

int main()
{
    auto stackTrace = StackTrace::Capture();
    for(auto &frame : stackTrace)
    {
        std::cout << frame << std::endl;
    }

    return 0;
}

