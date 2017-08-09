#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

#include "mscorlib_System_Object2689449295.h"

// System.Byte[][]
struct ByteU5BU5DU5BU5D_t717853552;
// Kyokumenn
struct Kyokumenn_t1278207725;
// System.String
struct String_t;




#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// Joseki
struct  Joseki_t3207443615  : public Il2CppObject
{
public:
	// System.Byte[][] Joseki::josekiData
	ByteU5BU5DU5BU5D_t717853552* ___josekiData_0;
	// System.Int32 Joseki::numJoseki
	int32_t ___numJoseki_1;
	// Kyokumenn Joseki::josekiKyokumenn
	Kyokumenn_t1278207725 * ___josekiKyokumenn_2;
	// System.String Joseki::path
	String_t* ___path_3;

public:
	inline static int32_t get_offset_of_josekiData_0() { return static_cast<int32_t>(offsetof(Joseki_t3207443615, ___josekiData_0)); }
	inline ByteU5BU5DU5BU5D_t717853552* get_josekiData_0() const { return ___josekiData_0; }
	inline ByteU5BU5DU5BU5D_t717853552** get_address_of_josekiData_0() { return &___josekiData_0; }
	inline void set_josekiData_0(ByteU5BU5DU5BU5D_t717853552* value)
	{
		___josekiData_0 = value;
		Il2CppCodeGenWriteBarrier(&___josekiData_0, value);
	}

	inline static int32_t get_offset_of_numJoseki_1() { return static_cast<int32_t>(offsetof(Joseki_t3207443615, ___numJoseki_1)); }
	inline int32_t get_numJoseki_1() const { return ___numJoseki_1; }
	inline int32_t* get_address_of_numJoseki_1() { return &___numJoseki_1; }
	inline void set_numJoseki_1(int32_t value)
	{
		___numJoseki_1 = value;
	}

	inline static int32_t get_offset_of_josekiKyokumenn_2() { return static_cast<int32_t>(offsetof(Joseki_t3207443615, ___josekiKyokumenn_2)); }
	inline Kyokumenn_t1278207725 * get_josekiKyokumenn_2() const { return ___josekiKyokumenn_2; }
	inline Kyokumenn_t1278207725 ** get_address_of_josekiKyokumenn_2() { return &___josekiKyokumenn_2; }
	inline void set_josekiKyokumenn_2(Kyokumenn_t1278207725 * value)
	{
		___josekiKyokumenn_2 = value;
		Il2CppCodeGenWriteBarrier(&___josekiKyokumenn_2, value);
	}

	inline static int32_t get_offset_of_path_3() { return static_cast<int32_t>(offsetof(Joseki_t3207443615, ___path_3)); }
	inline String_t* get_path_3() const { return ___path_3; }
	inline String_t** get_address_of_path_3() { return &___path_3; }
	inline void set_path_3(String_t* value)
	{
		___path_3 = value;
		Il2CppCodeGenWriteBarrier(&___path_3, value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
