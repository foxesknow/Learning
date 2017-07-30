#include "stdafx.h"

#include "Settings.h"

#include <stdlib.h>

bool EnvironmentSettings::GetSetting(const std::wstring &name, std::wstring &value) const noexcept
{
    wchar_t *envValue = nullptr;
    size_t length = 0;

    auto error = ::_wdupenv_s(&envValue, &length, name.c_str());

    if(error || envValue == nullptr)
    {
        return false;
    }

    value = envValue;
    ::free(envValue);

    return true;
}