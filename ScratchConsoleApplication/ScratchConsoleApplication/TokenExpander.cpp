#include "stdafx.h"

#include "TokenExpander.h"
#include "Settings.h"

static std::wstring DefaultBeginToken = L"${";
static std::wstring DefaultEndToken = L"}";

TokenExpander::TokenExpander()
{
}


TokenExpander::~TokenExpander()
{
}

std::wstring TokenExpander::ExpandText(const std::wstring &value, const SettingsManager &settingsManager)
{
    return ExpandText(value, DefaultBeginToken, DefaultEndToken, settingsManager);
}

std::wstring TokenExpander::ExpandText(const std::wstring &value, const std::wstring &beginToken, const std::wstring &endToken, const SettingsManager &settingsManager)
{
    auto expandedValue = value;

    auto beginMarkLength = beginToken.size();
	auto endMarkLength = endToken.size();
			
	size_t index = std::wstring::npos;
	size_t startIndex = 0;
			
	while((index = expandedValue.find(beginToken,startIndex)) != -1)
	{
		int end = expandedValue.find(endToken,index+1);
		if(end == std::wstring::npos)
		{
			break;
		}
		else
		{
			auto token = expandedValue.substr(index + beginMarkLength, (end - index) - beginMarkLength);
			auto tokenValue = ExpandToken(token, settingsManager);
					
			auto leftPart = expandedValue.substr(0, index);
			auto rightPart = expandedValue.substr(end + endMarkLength);
					
			expandedValue = leftPart + tokenValue + rightPart;
			startIndex = leftPart.size() + tokenValue.size();
		}
	}
			
	return expandedValue;
}

std::wstring TokenExpander::ExpandToken(const std::wstring &token, const SettingsManager &settingsManager)
{
    std::wstring space;
    std::wstring name;
    std::wstring action;

    // TODO: split the token apart

    if(space.empty())
    {
        // TODO: throw
    }

    if(name.empty())
    {
        // TODO: throw
    }

    std::wstring value;
    if(!settingsManager.GetSetting(space, name, value))
    {
        // TODO: throw
    }

    if(!action.empty())
    {
        // TODO: handle action
    }

    return value;
}