#pragma once

#include <type_traits>
#include <string>
#include <unordered_map>
#include <vector>
#include <sstream>
#include <exception>

#define BEGIN_ENUM_CONVERTER(NAME, ENUM) class NAME : public EnumConverter<ENUM, NAME> \
{ \
public: \
    static const std::vector<EnumEntry> &GetEnumTable() noexcept \
    { \
        static std::vector<EnumEntry> table \
        {


#define END_ENUM_CONVERTER }; \
        return table; \
        } \
    };

#define MAKE_ENUM_TABLE_ENTRY(E) EnumEntry{enum_type::E, static_cast<underlying_type>(enum_type::E), #E}

template<class T, class Derived>
class EnumConverter
{
private:
    static_assert(std::is_enum_v<T>, "T is not an enum");

protected:
    using underlying_type = std::underlying_type_t<T>;
    using enum_type = T;

    struct EnumEntry
    {
        T Value;
        underlying_type Underlying;
        std::string AsString;
    };

public:
    static bool TryToString(T value, std::string &result) noexcept
    {
        const auto &table = Derived::GetEnumTable();
        for(const EnumEntry &entry : table)
        {
            if(entry.Value == value)
            {
                result = entry.AsString;
                return true;
            }
        }

        result = std::to_string(static_cast<underlying_type>(value));
        return false;
    }
    
    static std::string ToString(T value)
    {
        std::string result;
        if(!TryToString(value, result))
        {
            throw std::exception("could not convert enum");
        }

        return result;
    }
    
    static bool TryToEnum(const std::string &value, T &result) noexcept
    {
        const auto &table = Derived::GetEnumTable();
        for(const EnumEntry &entry : table)
        {
            if(entry.AsString == value)
            {
                result = entry.Value;
                return true;
            }
        }

        result = T{};
        return false;
    }

    static T ToEnum(const std::string &value)
    {
        T result;
        if(!TryToEnum(value, result))
        {
            throw std::exception("could not convert string");
        }

        return result;
    }
};




#define BEGIN_BITFIELD_ENUM_CONVERTER(NAME, ENUM) class NAME : public BitfieldEnumConverter<ENUM, NAME> \
{ \
public: \
    static const std::vector<EnumEntry> &GetEnumTable() noexcept \
    { \
        static std::vector<EnumEntry> table \
        {


#define END_BITFIELD_ENUM_CONVERTER }; \
        return table; \
        } \
        static const std::unordered_map<std::string, EnumEntry> &GetStringMap() noexcept \
        { \
            static auto map = ByString(); \
            return map; \
        } \
        static const std::unordered_map<underlying_type, EnumEntry> &GetUnderlyingMap() noexcept \
        { \
            static auto map = ByUnderlying(); \
            return map; \
        } \
    };

#define MAKE_ENUM_TABLE_ENTRY(E) EnumEntry{enum_type::E, static_cast<underlying_type>(enum_type::E), #E}


template<class T, class Derived>
class BitfieldEnumConverter
{
private:
    static_assert(std::is_enum_v<T>, "T is not an enum");

protected:
    using underlying_type = std::underlying_type_t<T>;
    using enum_type = T;

    using underlying_type = std::underlying_type_t<T>;
    using enum_type = T;

    struct EnumEntry
    {
        T Value;
        underlying_type Underlying;
        std::string AsString;
    };

protected:
    static std::unordered_map<std::string, EnumEntry> ByString() noexcept
    {
        std::unordered_map<std::string, EnumEntry> map;

        const auto &table = Derived::GetEnumTable();
        for(const EnumEntry &entry : table)
        {
            map.emplace(entry.AsString, entry);
        }

        return map;
    }

    static std::unordered_map<underlying_type, EnumEntry> ByUnderlying() noexcept
    {
        std::unordered_map<underlying_type, EnumEntry> map;

        const auto &table = Derived::GetEnumTable();
        for(const EnumEntry &entry : table)
        {
            map.emplace(entry.Underlying, entry);
        }

        return map;
    }

public:
    static bool TryToString(T value, std::string &result) noexcept
    {
        const auto &map= Derived::GetUnderlyingMap();

        result.clear();
        underlying_type underlying = static_cast<underlying_type>(value);

        // Handle zero as a special case as we can't OR it out
        if(underlying == 0)
        {
            const auto it = map.find(static_cast<underlying_type>(value));
            if(it != map.end())
            {
                result = (*it).second.AsString;
                return true;
            }
            else
            {                
                return false;
            }
        }

        bool first = true;
        for(const auto &pair : map)
        {
            const auto mask = pair.first;
            if(mask == 0) continue;

            if((underlying & mask) == mask)
            {
                if(!first)
                {
                    result += " | ";
                }
                    
                result += pair.second.AsString;
                first = false;

                // Now remove the bits...
                underlying &= ~mask;

                // ...and if the value is now zero we're done
                if(underlying == 0) break;
            }
        }

        if(underlying != 0)
        {
            // We failed to match something
            result.clear();
            return false;
        }

        return true;
    }

    static std::string ToString(T value)
    {
        std::string result;
        if(!TryToString(value, result))
        {
            throw std::exception("could not convert enum");
        }

        return result;
    }
    
    
    static bool TryToEnum(const std::string &value, T &result) noexcept
    {
        bool first = true;
        bool matched = false;

        std::stringstream stream(value);

        const auto &map= Derived::GetStringMap();
        
        std::string v;
        while(stream >> v)
        {
            const auto it = map.find(v);
            if(it != map.end())
            {
                if(first)
                {
                    result = (*it).second.Value;
                    first = false;
                }
                else
                {
                    result = static_cast<enum_type>((*it).second.Underlying | static_cast<underlying_type>(result));
                }

                matched = true;
            }
            else
            {
                // It's an unknown value, so fail
                result = T{};
                return false;
            }
        }

        if(matched)
        {
            return true;
        }

        result = T{};
        return false;
    }

    static T ToEnum(const std::string &value)
    {
        T result;
        if(!TryToEnum(value, result))
        {
            throw std::exception("could not convert string");
        }

        return result;
    }
};





void TestEnumToString();

