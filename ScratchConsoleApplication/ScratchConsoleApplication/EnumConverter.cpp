#include "stdafx.h"
#include "EnumConverter.h"

#include "People.h"
#include "Mode.h"

void TestEnum()
{
    std::string result;
    bool success = PeopleConverter::TryToString(People::Jack, result);

    People people;
    success = PeopleConverter::TryToEnum("Ben", people);
}

void TestBitfieldEnum()
{
    Mode mode;
    bool success = ModeConverter::TryToEnum("Read Write", mode);

    std::string asString;
    success = ModeConverter::TryToString(Mode::Read | Mode::Write | Mode::Execute, asString);
}

void TestEnumToString()
{
    TestBitfieldEnum();
}