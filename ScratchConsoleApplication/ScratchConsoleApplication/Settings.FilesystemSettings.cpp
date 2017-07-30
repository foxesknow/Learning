#include "stdafx.h"

#include "Settings.h"

bool FilesystemSettings::GetSetting(const std::wstring &name, std::wstring &value) const noexcept
{
	// TODO
    auto lower = name;

    if(lower == L"cwd")
    {
        constexpr DWORD BufferLength = 1024;
        wchar_t buffer[BufferLength];
        auto length = ::GetCurrentDirectory(BufferLength, buffer);

        if(length)
        {
            value = std::wstring(buffer, length);
        }
        else
        {
            return false;
        }
    }
    else if(lower == L"tempdir")
    {
        constexpr DWORD BufferLength = 1024;
        wchar_t buffer[BufferLength];
        auto length = ::GetCurrentDirectory(BufferLength, buffer);

        if(length)
        {
            value = std::wstring(buffer, length);
        }
        else
        {
            return false;
        }
    }
    else
    {
        return false;
    }

    return true;
}