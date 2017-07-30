#pragma once

#include <string>

class SettingsManager;

class TokenExpander
{
private:
    static std::wstring ExpandToken(const std::wstring &token, const SettingsManager &settingsManager);

public:
    TokenExpander();
    ~TokenExpander();

    static std::wstring ExpandText(const std::wstring &value, const SettingsManager &settingsManager);
    static std::wstring ExpandText(const std::wstring &value, const std::wstring &beginToken, const std::wstring &endToken, const SettingsManager &settingsManager);    
};

