#include "stdafx.h"

#include "StackTrace.h"

#include <DbgHelp.h>

#pragma comment(lib, "dbghelp.lib")

static HANDLE s_Process = ::GetCurrentProcess();

// TODO: Set the path to the process directory
static BOOL s_SymSystemInitialized = ::SymInitialize(s_Process, nullptr, TRUE);

std::ostream &operator<<(std::ostream &stream, const StackFrame &stackFrame)
{
    std::ios::fmtflags flags(stream.flags());

    stream  << "[Name: " << stackFrame.Name() 
            << "] [Filename: " << stackFrame.Filename() 
            << "] [Line: " << stackFrame.LineNumber() 
            << "] [Address: " << std::hex << stackFrame.Address()
            << "]";

    stream.flags(flags);

    return stream;
}

StackTrace StackTrace::Capture() noexcept
{
    StackTrace stackTrace;

    if(!s_SymSystemInitialized)
    {
        // For some reason we couldn't initialize the symbol system
        return stackTrace;
    }

    constexpr DWORD MaxStackFrames = 1024;
    constexpr DWORD MaxFunctionNameLength = 1024;

    // The 1 to CaptureStackBackTrace will remove this function from the capture
    void *stack[MaxStackFrames];
    auto numberOfFrames = ::CaptureStackBackTrace(1, MaxStackFrames, stack, nullptr);
    

    auto *symbol = static_cast<SYMBOL_INFO*>(::malloc(sizeof(SYMBOL_INFO) + ((MaxFunctionNameLength -1) * sizeof(CHAR))));
    symbol->MaxNameLen = MaxFunctionNameLength;
    symbol->SizeOfStruct = sizeof(SYMBOL_INFO);

    IMAGEHLP_LINE64 line;
    line.SizeOfStruct = sizeof(IMAGEHLP_LINE64);

    // TODO: The SymXXX functions are not thread safe, so we need to lock here

    for(int i = 0; i < numberOfFrames; i++)
    {
        auto address = reinterpret_cast<DWORD64>(stack[i]);
        DWORD displacement = 0;
        if(::SymFromAddr(s_Process, address, nullptr, symbol))
        {
            if(::SymGetLineFromAddr64(s_Process, address, &displacement, &line))
            {
                stackTrace.m_StackFrames.emplace_back(line.FileName,  symbol->Name, line.LineNumber, address);
            }
        }
    }

    ::free(symbol);

    return stackTrace;
}