#pragma once

#include "EnumConverter.h"

enum class People
{
    Nobody,
    Jack,
    Kate,
    Sawyer,
    Hurley,
    Ben
};

BEGIN_ENUM_CONVERTER(PeopleConverter, People)
    MAKE_ENUM_TABLE_ENTRY(Nobody),
    MAKE_ENUM_TABLE_ENTRY(Jack),
    MAKE_ENUM_TABLE_ENTRY(Kate),
    MAKE_ENUM_TABLE_ENTRY(Sawyer),
    MAKE_ENUM_TABLE_ENTRY(Hurley),
    MAKE_ENUM_TABLE_ENTRY(Ben),
END_ENUM_CONVERTER;