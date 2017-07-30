#include "stdafx.h"

#include "Settings.h"

#include <memory>
#include <vector>

struct SettingsNode::Implementation
{
    Implementation(ISettings *settings, ISettings *next) : Settings(settings), Next(next)
    {
    }

    std::unique_ptr<ISettings> Settings;
    std::unique_ptr<ISettings> Next;
};

SettingsNode::SettingsNode(ISettings *settings, ISettings *next) noexcept : m_Impl(new Implementation(settings, next))
{

}


SettingsNode::~SettingsNode()
{
    delete m_Impl;
}

bool SettingsNode::GetSetting(const std::wstring &name, std::wstring &value) const noexcept
{
    bool success = m_Impl->Settings->GetSetting(name, value);
    if(!success)
    {
        success = m_Impl->Next->GetSetting(name, value);
    }

    return success;
}