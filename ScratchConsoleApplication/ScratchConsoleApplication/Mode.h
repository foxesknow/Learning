#pragma once

#include <type_traits>

#include "EnumConverter.h"

enum class Mode
{
    None,
    Read = 1,
    Write = 2,
    Execute = 4
};

constexpr Mode operator|(Mode lhs, Mode rhs) noexcept
{
    using underlying = std::underlying_type_t<Mode>;
    return static_cast<Mode>(static_cast<underlying>(lhs) | static_cast<underlying>(rhs));
}

BEGIN_BITFIELD_ENUM_CONVERTER(ModeConverter, Mode)
    MAKE_ENUM_TABLE_ENTRY(None),
    MAKE_ENUM_TABLE_ENTRY(Read),
    MAKE_ENUM_TABLE_ENTRY(Write),
    MAKE_ENUM_TABLE_ENTRY(Execute),
END_BITFIELD_ENUM_CONVERTER;