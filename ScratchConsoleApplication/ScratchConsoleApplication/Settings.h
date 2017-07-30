#pragma once

#include <string>

class ISettings
{
public:
    ISettings();
    virtual ~ISettings();

    virtual bool GetSetting(const std::wstring &name, std::wstring &value) const noexcept = 0;
};

class SettingsNode : public ISettings
{
private:
    struct Implementation;
    Implementation *m_Impl;

public:
    SettingsNode(ISettings *settings, ISettings *next) noexcept;
    ~SettingsNode();

    virtual bool GetSetting(const std::wstring &name, std::wstring &value) const noexcept override;
};


class EnvironmentSettings : public ISettings
{
public:
    virtual bool GetSetting(const std::wstring &name, std::wstring &value) const noexcept override;
};

class ProcessSettings : public ISettings
{
public:
    virtual bool GetSetting(const std::wstring &name, std::wstring &value) const noexcept override;
};

class FilesystemSettings : public ISettings
{
public:
    virtual bool GetSetting(const std::wstring &name, std::wstring &value) const noexcept override;
};


class SettingsManager
{
private:
    struct Implementation;
    Implementation *m_Impl;

    void DoRegister(const std::wstring &space, ISettings *settings);

public:
    SettingsManager();
    ~SettingsManager();

    bool GetSetting(const std::wstring &space, const std::wstring &name, std::wstring &value) const noexcept;
    
    wchar_t NamespaceSeparator() const noexcept;
    bool CrackQualifiedName(const std::wstring &qualifiedName, std::wstring &name, std::wstring &setting) const noexcept;
};

