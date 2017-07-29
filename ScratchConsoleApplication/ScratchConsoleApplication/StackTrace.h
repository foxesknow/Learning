#pragma once

#include <string>
#include <vector>
#include <ostream>

class StackFrame
{
private:
    std::string m_Filename;
    std::string m_Name;
    size_t m_LineNumber;
    DWORD64 m_Address;

public:
    StackFrame(std::string filename, std::string name, size_t lineNumber, DWORD64 address) 
        : m_Filename(std::move(filename)), m_Name(std::move(name)), m_LineNumber(lineNumber), m_Address(address)
    {
    }

    const std::string &Filename() const noexcept
    {
        return m_Filename;
    }

    const std::string &Name() const noexcept
    {
        return m_Name;
    }

    size_t LineNumber() const noexcept
    {
        return m_LineNumber;
    }

    DWORD64 Address() const noexcept
    {
        return m_Address;
    }
};

std::ostream &operator<<(std::ostream &stream, const StackFrame &stackFrame);


class StackTrace
{
private:
    std::vector<StackFrame> m_StackFrames;

public:
    size_t Size() const noexcept
    {
        return m_StackFrames.size();
    }

    bool IsEmpty() const noexcept
    {
        return m_StackFrames.empty();
    }

    auto begin() noexcept -> decltype(m_StackFrames.begin())
    {
        return m_StackFrames.begin();
    }

    auto end() noexcept -> decltype(m_StackFrames.end())
    {
        return m_StackFrames.end();
    }

    auto begin() const noexcept -> decltype(m_StackFrames.begin())
    {
        return m_StackFrames.begin();
    }

    auto end() const noexcept -> decltype(m_StackFrames.end())
    {
        return m_StackFrames.end();
    }

    const StackFrame &operator[](size_t index) const noexcept
    {
        return m_StackFrames[index];
    }

    static StackTrace Capture() noexcept;
};
