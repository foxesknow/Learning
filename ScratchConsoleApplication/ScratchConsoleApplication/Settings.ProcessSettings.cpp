#include "stdafx.h"

#include "Settings.h"

bool ProcessSettings::GetSetting(const std::wstring &name, std::wstring &value) const noexcept
{
	auto lower = name;

    if(lower == L"pid")
    {
        auto id = ::GetCurrentProcessId();
        // TODO: to string
    }
    else
    {
        return false;
    }

    return true;
}