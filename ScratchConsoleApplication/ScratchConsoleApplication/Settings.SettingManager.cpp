#include "stdafx.h"

#include "Settings.h"

#include <unordered_map>
#include <string>
#include <memory>
#include <mutex>



struct SettingsManager::Implementation
{
    std::unordered_map<std::wstring, std::unique_ptr<ISettings>> Settings;
};

SettingsManager::SettingsManager() : m_Impl(new Implementation)
{

}
SettingsManager::~SettingsManager()
{
    delete m_Impl;
}

bool SettingsManager::GetSetting(const std::wstring &space, const std::wstring &name, std::wstring &value) const noexcept
{
    // TODO: lock
    auto &allSettings = m_Impl->Settings;
    
    auto existing = allSettings.find(space);
    if(existing != allSettings.end())
    {
        return (*existing).second->GetSetting(name, value);
    }

    return false;
}

wchar_t SettingsManager::NamespaceSeparator() const noexcept
{
    return L':';
}

bool SettingsManager::CrackQualifiedName(const std::wstring &qualifiedName, std::wstring &name, std::wstring &setting) const noexcept
{
    auto pivot = qualifiedName.find(NamespaceSeparator());

    if(pivot = std::wstring::npos)
    {
        return false;
    }

    name = qualifiedName.substr(0, pivot);
    setting = qualifiedName.substr(pivot + 1);

    return true;
}


void SettingsManager::DoRegister(const std::wstring &space, ISettings *settings)
{
    // TODO: lock

    auto &allSettings = m_Impl->Settings;
    
    auto existing = allSettings.find(space);
    if(existing == allSettings.end())
    {
        // It's new
        allSettings.emplace(space, std::unique_ptr<ISettings>(settings));
    }
    else
    {
        // We've already got a setting so aggregate them
        auto existingSetting = (*existing).second.release();
        auto aggregate = std::make_unique<SettingsNode>(settings, existingSetting);

        allSettings.erase(existing);
        allSettings.emplace(space, std::move(aggregate));
    }

}